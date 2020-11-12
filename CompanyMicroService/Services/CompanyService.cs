using CompanyMicroService.Models;
using CompanyMicroService.Repositories;
using Microsoft.EntityFrameworkCore;
using StockMarketWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace CompanyMicroService.Services
{
    public class CompanyService : ICompanyRepository
    {
        private readonly CompanyAppDBContext _db;
        public CompanyService(CompanyAppDBContext db)
        {
            _db = db;
        }

        public async Task<bool> AddCompany(AddCompanyModel model)
        {
            var sectorInfo = _db.Sectors.Where(s => s.SectorID == model.SectorID).FirstOrDefault();
            
            Company company = new Company
            {
                CompanyName = model.CompanyName,
                TurnOver = model.TurnOver,
                CEO = model.CEO,
                BoardOfDirectors = model.BoardOfDirectors,
                SectorID = model.SectorID,
                Sector = sectorInfo,
                WriteUp = model.WriteUp,
                stockCodes = null,
                Active = model.Active
            };

            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                _db.Companies.Add(company);
                List<CompanyStockExchange> iecse = new List<CompanyStockExchange>();
                foreach (string exchangeShortName in model.Exchanges)
                {
                    var ex = _db.StockExchanges.Where(e => e.shortName == exchangeShortName).FirstOrDefault();
                    //var cid = _db.Companies.Where(c => c.CompanyName == model.CompanyName).FirstOrDefault().CompanyID;
                    CompanyStockExchange cse = new CompanyStockExchange
                    {
                        Company = company,
                        StockExchangeID = ex.StockExchangeID,
                        StockExchange = ex
                    };
                    iecse.Add(cse);
                }
                _db.companyStockExchanges.AddRange(iecse);
                _db.SaveChanges();
                await transaction.CommitAsync();
                return true;
            }
            catch(Exception err)
            {
                Console.WriteLine(err);
                await transaction.RollbackAsync();
                return false;
            }           
                   
        }

        public bool addIPO(AddIPOModel model)
        {
            var company = _db.Companies.Where(c => c.CompanyID == model.CompanyID).FirstOrDefault();
            IPODetails ipo = new IPODetails
            {
                CompanyID = model.CompanyID,
                Company = company,
                stockExchanges = model.stockExchanges,
                PricePerShare = model.PricePerShare,
                TotalAvailableShares = model.TotalAvailableShares,
                OpeningDate = model.OpeningDate,
                Remarks = model.Remarks
            };
            var result = _db.IPODetails.Add(ipo);
            _db.SaveChanges();
            if (result != null)
                return true;
            else return false;
        }

        public bool addStockPrice(AddStockPriceModel sp)
        {
            var cmpany = _db.Companies.Where(c => c.CompanyID == sp.CompanyCode).FirstOrDefault();
            StockPrice stockPrice = new StockPrice
            {
                CompanyCode = sp.CompanyCode,
                Company = cmpany,
                Exchange = sp.Exchange,
                price = sp.price,
                Date = sp.Date,
                Time = sp.Time
            };
            var result = _db.StockPrices.Add(stockPrice);
            _db.SaveChanges();
            if (result != null)
                return true;
            else return false;
        }

        public IEnumerable<Company> getCompanyDetails()
        {
            return _db.Companies.Include(c => c.Sector).Include(co => co.stockCodes).Include(com => com.CompaniesListed).ToList();     
        }

        public IEnumerable<IPODetails> getCompanyIPODetails(int C_ID)
        {
            return _db.IPODetails.Where(s => s.CompanyID == C_ID).Include(c => c.Company).ToList();            
        }

        public IEnumerable<StockPrice> getCompanyStockPrice(int C_ID)
        {
            return _db.StockPrices.Where(c => c.Company.CompanyID == C_ID).ToList();            
        }

        public IEnumerable<Company> getMatchingCompanies(string nOfCompany)
        {
            return _db.Companies.Where(c => c.CompanyName.Contains(nOfCompany)).Include(c => c.Sector).Include(co => co.stockCodes).Include(com => com.CompaniesListed).ToList();            
        }

        public bool updateCOmpanyDetails(int CompanyID, UpdateCompanyDetailsModel companyToBeUpdated)
        {
            var actualCompnay = _db.Companies.Where(c => c.CompanyID == CompanyID).FirstOrDefault();
            actualCompnay.CompanyName = companyToBeUpdated.CompanyName;
            actualCompnay.TurnOver = companyToBeUpdated.TurnOver;
            actualCompnay.CEO = companyToBeUpdated.CEO;
            actualCompnay.BoardOfDirectors = companyToBeUpdated.BoardOfDirectors;
            actualCompnay.WriteUp = companyToBeUpdated.WriteUp;
            actualCompnay.Active = companyToBeUpdated.Active;
            _db.Companies.Update(actualCompnay);
            var res = _db.SaveChanges();
            return (res == 1) ? true : false;
        }

        public bool updateIPODetails(int id, UpdateIPODetailsModel ipod)
        {
            var actualIPO = _db.IPODetails.Where(ipo => ipo.ipoID == id).FirstOrDefault();
            actualIPO.PricePerShare = ipod.PricePerShare;
            actualIPO.OpeningDate = ipod.OpeningDate;
            actualIPO.TotalAvailableShares = ipod.TotalAvailableShares;
            actualIPO.Remarks = ipod.Remarks;
            _db.IPODetails.Update(actualIPO);
            var res = _db.SaveChanges();
            return (res == 1) ? true : false;
        }
    }
}

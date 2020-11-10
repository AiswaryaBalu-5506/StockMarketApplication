using CompanyMicroService.Models;
using CompanyMicroService.Repositories;
using Microsoft.EntityFrameworkCore;
using StockMarketWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroService.Services
{
    public class CompanyService : ICompanyRepository
    {
        private readonly CompanyAppDBContext _db;
        public CompanyService(CompanyAppDBContext db)
        {
            _db = db;
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
    }
}

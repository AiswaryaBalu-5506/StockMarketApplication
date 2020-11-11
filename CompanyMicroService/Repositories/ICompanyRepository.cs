using CompanyMicroService.Models;
using StockMarketWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroService.Repositories
{
    public interface ICompanyRepository
    {
        public IEnumerable<Company> getCompanyDetails();
        public IEnumerable<IPODetails> getCompanyIPODetails(int C_ID);
        public IEnumerable<StockPrice> getCompanyStockPrice(int C_ID);
        public IEnumerable<Company> getMatchingCompanies(string nOfCompany);
        public Task<bool> AddCompany(AddCompanyModel company);
        public bool addIPO(AddIPOModel ipo);
        public bool addStockPrice(AddStockPriceModel sp);
    }
}

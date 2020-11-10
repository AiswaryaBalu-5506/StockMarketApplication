using Microsoft.EntityFrameworkCore;
using StockExchangeMicroService.Models;
using StockExchangeMicroService.Repositories;
using StockMarketWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMicroService.Services
{
    public class ExchangeService : IExchangeRepository
    {
        private readonly ExchangeAppDBContext _db;
        public ExchangeService(ExchangeAppDBContext db)
        {
            _db = db;
        }

        public IEnumerable<Company> getCompaniesInAExchange(int exchangeID)
        {
            return _db.Companies.Include(c => c.Sector).Include(co => co.stockCodes).Include(com => com.CompaniesListed)
                .Where(c => c.CompaniesListed.Any(e => e.StockExchangeID == exchangeID)).ToList();
        }

        public IEnumerable<StockExchange> getStockEchanges()
        {
            return _db.StockExchanges.ToList();            
        }
    }
}

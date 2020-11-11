using StockMarketWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMicroService.Repositories
{
    public interface IExchangeRepository
    {
        public IEnumerable<StockExchange> getStockEchanges();
        public IEnumerable<Company> getCompaniesInAExchange(int exchangeID);
        public bool addNewExchange(StockExchange exchange);
    }
}

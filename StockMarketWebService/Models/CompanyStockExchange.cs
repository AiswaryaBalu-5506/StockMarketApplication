using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketWebService.Models
{
    public class CompanyStockExchange
    {
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        public int StockExchangeID { get; set; }
        public StockExchange StockExchange { get; set; }
    
    }
}

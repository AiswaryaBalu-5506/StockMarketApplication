using StockMarketWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroService.Models
{
    public class AddIPOModel
    {
        public int CompanyID { get; set; }        
        public string stockExchanges { get; set; }      
        public float PricePerShare { get; set; }      
        public int TotalAvailableShares { get; set; }        
        public DateTime OpeningDate { get; set; }
        public string Remarks { get; set; }
    }
}

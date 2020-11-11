using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroService.Models
{
    public class AddStockPriceModel
    {
        public int CompanyCode { get; set; }

        
        public string Exchange { get; set; }

      
        public float price { get; set; }

     
        public DateTime Date { get; set; }

      
        public DateTime Time { get; set; }
    }
}

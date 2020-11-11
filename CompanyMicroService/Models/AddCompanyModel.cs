using StockMarketWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroService.Models
{
    public class AddCompanyModel
    {
        public string CompanyName { get; set; }
        public float TurnOver { get; set; }       
        public string CEO { get; set; }
        public string BoardOfDirectors { get; set; }
        public IEnumerable<string> Exchanges { get; set; }        
        public int SectorID { get; set; }
        public string WriteUp { get; set; }
    }
}

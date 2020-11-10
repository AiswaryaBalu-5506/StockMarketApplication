using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketWebService.Models
{
    public class StockExchange
    {
        [Key]
        public int StockExchangeID { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Cannot exceed 30 characters")]
        public string ExchangeName { get; set; }
        [Required]
        [MaxLength(5, ErrorMessage = "Cannot exceed 30 characters")]
        public string shortName { get; set; }

        public string BriefWriteUp { get; set; }

        public string Address { get; set; }

        public string Remarks { get; set; }

        public IEnumerable<CompanyStockExchange> CompaniesListed { get; set; }
        
    }
}

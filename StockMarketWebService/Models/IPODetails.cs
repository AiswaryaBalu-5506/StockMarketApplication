using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketWebService.Models
{
    public class IPODetails
    {
        [Key]
        public int ipoID { get; set; }

        [Required]        
        public int CompanyID { get; set; }
        [Required]
        public Company Company { get; set; } 

        [Required]
        public string stockExchanges { get; set; } 

        [Required]
        public float PricePerShare { get; set; }

        [Required]
        public int TotalAvailableShares { get; set; }

        [Required]
        public DateTime OpeningDate { get; set; }

        public string Remarks { get; set; }
    }
}

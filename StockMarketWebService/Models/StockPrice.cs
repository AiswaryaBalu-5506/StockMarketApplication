using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketWebService.Models
{
    public class StockPrice
    {
        [Key]        
        public int SerialNumber { get; set; }

        [Required]
        public int CompanyCode { get; set; } 

        public Company Company { get; set; }

        [Required]
        public string Exchange { get; set; }

        [Required]
        public float price { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }
    }
}

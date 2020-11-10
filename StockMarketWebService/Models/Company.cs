using StockMarketWebService.DataEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketWebService.Models
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Cannot exceed 30 characters")]
        public string CompanyName { get; set; }

        public float TurnOver { get; set; }

        [Required]
        public string CEO { get; set; }

        public string BoardOfDirectors { get; set; }

        public IEnumerable<CompanyStockExchange> CompaniesListed { get; set; }

        [Required]
        public Sectors Sector { get; set; }
        public int SectorID { get; set; }

        public string WriteUp { get; set; }

        [Required]
        public IEnumerable<StockCodes> stockCodes { get; set; }

    }
}

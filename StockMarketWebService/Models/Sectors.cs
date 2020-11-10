using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketWebService.Models
{
    public class Sectors
    {
        [Key]
        public int SectorID { get; set; }

        [Required]
        [MaxLength(30)]
        public string SectorName { get; set; }

        public string WriteUp { get; set; }

        public IEnumerable<Company> CompaniesInThisSector { get; set; }
    }
}

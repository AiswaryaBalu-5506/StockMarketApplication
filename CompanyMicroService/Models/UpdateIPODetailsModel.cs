using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroService.Models
{
    public class UpdateIPODetailsModel
    {
        public float PricePerShare { get; set; }        
        public int TotalAvailableShares { get; set; }        
        public DateTime OpeningDate { get; set; }
        public string Remarks { get; set; }
    }
}

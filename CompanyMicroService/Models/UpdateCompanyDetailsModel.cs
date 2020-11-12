using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroService.Models
{
    public class UpdateCompanyDetailsModel
    {
        public string CompanyName { get; set; }
        public float TurnOver { get; set; }
        public string CEO { get; set; }
        public string BoardOfDirectors { get; set; }          
        public string WriteUp { get; set; }
        public bool Active { get; set; }
    }
}

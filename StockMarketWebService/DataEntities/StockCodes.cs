using StockMarketWebService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketWebService.DataEntities
{
    public class StockCodes
    {
        [Key]
        public int SID { get; set; }
        public int StockCodeInExchange { get; set; }
        public string NameOfExchange { get; set; }
    }
}

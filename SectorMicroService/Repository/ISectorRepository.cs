using StockMarketWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectorMicroService.Repository
{
    public interface ISectorRepository
    {
        public IEnumerable<Sectors> getSectors();
        public IEnumerable<Company> getCompaniesInSector(int sectorID);
    }
}

using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SectorMicroService.Models;
using SectorMicroService.Repository;
using StockMarketWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectorMicroService.Services
{
    public class SectorService : ISectorRepository
    {
        private readonly SectorAppDbContext _db;
        public SectorService(SectorAppDbContext db)
        {
            _db = db;
        }

        public bool addSector(Sectors sector)
        {
            _db.Sectors.Add(sector);
            var res = _db.SaveChanges();
            return (res==1) ? true : false;            
        }

        public IEnumerable<Company> getCompaniesInSector(int sectorID)
        {
            return _db.Companies.Where(c => c.SectorID == sectorID).Include(c => c.Sector).Include(co => co.stockCodes).Include(com => com.CompaniesListed).ToList();      
        }

        public IEnumerable<Sectors> getSectors()
        {
            return _db.Sectors.Include(c => c.CompaniesInThisSector).ToList();
        }
    }
}

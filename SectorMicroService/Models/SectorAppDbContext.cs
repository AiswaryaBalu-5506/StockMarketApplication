using Microsoft.EntityFrameworkCore;
using StockMarketWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectorMicroService.Models
{
    public class SectorAppDbContext: DbContext 
    {
        public SectorAppDbContext(DbContextOptions<SectorAppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyStockExchange>()
                .HasKey(bc => new { bc.CompanyID, bc.StockExchangeID });
            modelBuilder.Entity<CompanyStockExchange>()
                .HasOne(bc => bc.Company)
                .WithMany(b => b.CompaniesListed)
                .HasForeignKey(bc => bc.CompanyID);
            modelBuilder.Entity<CompanyStockExchange>()
                .HasOne(bc => bc.StockExchange)
                .WithMany(c => c.CompaniesListed)
                .HasForeignKey(bc => bc.StockExchangeID);
        }


        public DbSet<Company> Companies { get; set; }
        public DbSet<IPODetails> IPODetails { get; set; }
        public DbSet<Sectors> Sectors { get; set; }
        public DbSet<StockExchange> StockExchanges { get; set; }
        public DbSet<StockPrice> StockPrices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CompanyStockExchange> companyStockExchanges { get; set; }
    }
}

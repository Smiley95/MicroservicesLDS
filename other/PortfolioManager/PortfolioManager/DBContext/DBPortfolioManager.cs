using Microsoft.EntityFrameworkCore;
using PortfolioManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioManager.DBContext
{
    public class DBPortfolioManager : DbContext
    {
        public DBPortfolioManager(DbContextOptions<DBPortfolioManager> options): base(options) { }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Investor> Investors { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

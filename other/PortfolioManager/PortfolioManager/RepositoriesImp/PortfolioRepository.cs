using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioManager.Models;
using PortfolioManager.Repositories;
using PortfolioManager.DBContext;
using Microsoft.EntityFrameworkCore;
namespace PortfolioManager.RepositoriesImp
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly DBPortfolioManager _dbContext;
        public PortfolioRepository(DBPortfolioManager dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Portfolio> GetAllPortfolio()
        {
            return _dbContext.Portfolios.ToList();
            throw new NotImplementedException();
        }

        public Portfolio GetPortfolioById(string portfolioID)
        {
            return _dbContext.Portfolios.Find(portfolioID);
            throw new NotImplementedException();
        }

        public void InsertPortfolio(Portfolio portfolio)
        {
            _dbContext.Portfolios.Add(portfolio);
            Save();
            throw new NotImplementedException();
        }

        public void UpdatePortfolio(Portfolio portfolio)
        {
            _dbContext.Entry(portfolio).State = EntityState.Modified;
            Save();
            throw new NotImplementedException();
        }
        public void DeletePortfolio(string portfolioID)
        {
            var portfolio = _dbContext.Portfolios.Find(portfolioID);
            _dbContext.Portfolios.Remove(portfolio);
            Save();
            throw new NotImplementedException();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
            throw new NotImplementedException();
        }
    }
}

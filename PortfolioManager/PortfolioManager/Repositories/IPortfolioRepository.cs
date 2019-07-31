using PortfolioManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioManager.Repositories
{
    public interface IPortfolioRepository
    {
        IEnumerable<Portfolio> GetAllPortfolio();
        Portfolio GetPortfolioById(string portfolioID);
        void InsertPortfolio(Portfolio portfolio);
        void UpdatePortfolio(Portfolio portfolio);
        void DeletePortfolio(string portfolioID);
        void Save();
    }
}

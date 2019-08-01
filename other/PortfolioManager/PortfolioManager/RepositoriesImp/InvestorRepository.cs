using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioManager.DBContext;
using PortfolioManager.Models;
using PortfolioManager.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PortfolioManager.RepositoriesImp
{
    public class InvestorRepository : IInvestorRepository
    {
        private readonly DBPortfolioManager _dbContext;
        public InvestorRepository(DBPortfolioManager dbContext) {
            _dbContext = dbContext;
        }
        public IEnumerable<Investor> GetAllInvestor()
        {
            return _dbContext.Investors.ToList();
            throw new NotImplementedException();
        }

        public Investor GetInvestorById(string investorID)
        {
            return _dbContext.Investors.Find(investorID);
            throw new NotImplementedException();
        }

        public void InsertInvestor(Investor investor)
        {
            _dbContext.Investors.Add(investor);
            Save();
            throw new NotImplementedException();
        }

        public void UpdateInvestor(Investor investor)
        {
            _dbContext.Entry(investor).State = EntityState.Modified;
            Save();
            throw new NotImplementedException();
        }
        public void DeleteInvestor(string investorID)
        {
            var investor = _dbContext.Investors.Find(investorID);
            _dbContext.Investors.Remove(investor);
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

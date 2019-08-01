using PortfolioManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioManager.Repositories
{
    public interface IInvestorRepository
    {
        IEnumerable<Investor> GetAllInvestor();
        Investor GetInvestorById(string investorID);
        void InsertInvestor(Investor investor);
        void UpdateInvestor(Investor investor);
        void DeleteInvestor(string investorID);
        void Save();
    }
}

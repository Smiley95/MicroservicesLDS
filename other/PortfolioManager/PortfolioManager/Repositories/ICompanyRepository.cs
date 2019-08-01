using PortfolioManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioManager.Repositories
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompany();
        Company GetCompanyById(string companyID);
        void InsertCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(string companyID);
        void Save();
    }
}

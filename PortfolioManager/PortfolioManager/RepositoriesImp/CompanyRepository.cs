using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioManager.DBContext;
using PortfolioManager.Models;
using PortfolioManager.Repositories;

namespace PortfolioManager.RepositoriesImp
{
    public class CompanyRepository : ICompanyRepository
    {

        private readonly DBPortfolioManager _dbContext;
        public CompanyRepository(DBPortfolioManager dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Company> GetAllCompany()
        {
            throw new NotImplementedException();
        }

        public Company GetCompanyById(string companyID)
        {
            throw new NotImplementedException();
        }

        public void InsertCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public void UpdateCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public void DeleteCompany(string companyID)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }


    }
}

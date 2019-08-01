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
    public class CompanyRepository : ICompanyRepository
    {

        private readonly DBPortfolioManager _dbContext;
        public CompanyRepository(DBPortfolioManager dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Company> GetAllCompany()
        {
            return _dbContext.Companies.ToList();
            throw new NotImplementedException();
        }

        public Company GetCompanyById(string companyID)
        {
            return _dbContext.Companies.Find(companyID);
            throw new NotImplementedException();
        }

        public void InsertCompany(Company company)
        {
            _dbContext.Companies.Add(company);
            Save();
            throw new NotImplementedException();
        }

        public void UpdateCompany(Company company)
        {
            _dbContext.Entry(company).State = EntityState.Modified;
            Save();
            throw new NotImplementedException();
        }

        public void DeleteCompany(string companyID)
        {
            var company = _dbContext.Companies.Find(companyID);
            _dbContext.Companies.Remove(company);
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

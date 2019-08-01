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
    public class UserRepository : IUserRepository
    {
        private readonly DBPortfolioManager _dbContext;
        public UserRepository(DBPortfolioManager dbContext) {
            _dbContext = dbContext;
        }
        public IEnumerable<User> GetAllUser()
        {
            return _dbContext.Users.ToList();
            throw new NotImplementedException();
        }

        public User GetUserById(string userID)
        {
            return _dbContext.Users.Find(userID);
            throw new NotImplementedException();
        }

        public void InsertUser(User user)
        {
            _dbContext.Users.Add(user);
            Save();
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            Save();
            throw new NotImplementedException();
        }

        public void DeleteUser(string userID)
        {
            var user= _dbContext.Users.Find(userID);
            _dbContext.Users.Remove(user);
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

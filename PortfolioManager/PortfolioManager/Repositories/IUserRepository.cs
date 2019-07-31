using PortfolioManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioManager.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUser();
        User GetUserById(string userID);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(string userID);
        void Save();
    }
}

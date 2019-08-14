using System;
using LDSData.DBContext;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LDSData.Repositories
{
    public class UserRepository : IDisposable
    {
        // SECURITY_DBEntities it is your context class
        LDSEntities context = new LDSEntities();
        //This method is used to check and validate the user credentials
        public User ValidateUser(string username, string password)
        {
            return context.User.FirstOrDefault(user =>
            user.User_name.Equals(username, StringComparison.OrdinalIgnoreCase)
            && user.User_pwd == password);
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
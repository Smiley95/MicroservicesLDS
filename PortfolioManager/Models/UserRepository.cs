using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioManager.Models
{
    public class UserRepository : IDisposable
    {
        // SECURITY_DBEntities it is your context class
        optimizerEntities context = new optimizerEntities();
        //This method is used to check and validate the user credentials
        public Users ValidateUser(string username, string password)
        {
            return context.Users.FirstOrDefault(user =>
            user.User_name.Equals(username, StringComparison.OrdinalIgnoreCase)
            && user.User_pwd == password);
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
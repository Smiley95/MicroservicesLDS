using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationAPI.Models
{
    public class UserRepository : IDisposable
    {
        // SECURITY_DBEntities it is your context class
        UsersEntities context = new UsersEntities();
        //This method is used to check and validate the user credentials
        public Users ValidateUser(string user_name, string user_pwd)
        {
            return context.Users.FirstOrDefault(user =>
            user.User_name.Equals(user_name, StringComparison.OrdinalIgnoreCase)
            && user.User_pwd == user_pwd);
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
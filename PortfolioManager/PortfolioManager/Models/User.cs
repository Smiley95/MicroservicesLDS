using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioManager.Models
{
    public class User
    {
        [Key]
        public string User_ID { get; set; }
        [Required]
        public string User_user_name { get; set; }
        [Required]
        public string User_password { get; set; }
        [Required]
        public string User_isExpert { get; set; }
        [Required]
        public string User_email { get; set; }
        public string User_Fname { get; set; }
        public string User_Lname { get; set; }
        public string User_address { get; set; }
        public virtual ICollection<Investor> investors { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioManager.Models
{
    public class Investor
    {
        [Key]
        public string investor_ID { get; set; }
        [Required]
        public string investor_FullName { get; set; }
        public int investor_age { get; set; }
        [Required]
        public int investor_time_horizon { get; set; }
        [Required]
        public string investor_email { get; set; }
        //indetification of the investor's expert manager 
        public string expertID { get; set; }
        public virtual User expert { get; set; }
        // the list of the investor's portfolios
        public virtual ICollection<Portfolio> portfolios { get; set; }
    }
}

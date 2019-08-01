using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioManager.Models
{
    public class Asset
    {
        [Key]
        public string Asset_ID { get; set; }
        [Required]
        public int Asset_nbShare { get; set; }
        [Required]
        public string Asset_name { get; set; }
        [Required]
        public DateTime Asset_AQS_date { get; set; }
        [Required]
        public float Asset_shareCost { get; set; }
        [Required]
        public float Asset_currentValue { get; set; }
        //the providing company
        public string companyID { get; set; }
        public virtual Company company { get; set; }
        //the holding portfolio
        public string portfolioID { get; set; }
        public virtual Portfolio portfolio { get; set; }

    }
}

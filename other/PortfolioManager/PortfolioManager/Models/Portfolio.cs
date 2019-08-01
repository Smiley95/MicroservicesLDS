using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManager.Models
{
    public class Portfolio
    {
        public Portfolio()
        {
            this.assets = new HashSet<Asset>();
        }
        [Key]
        public string Portfolio_ID { get; set; }
        [Required]
        public string Portfolio_title { get; set; }
        [Required]
        public DateTime Portfolio_Creation { get; set; }
        //list of Assets within
        public virtual ICollection<Asset> assets { get; set; }
        //identification of the owned investor
        public string investorID { get; set; }
        public virtual Investor investor { get; set; }

    }
}

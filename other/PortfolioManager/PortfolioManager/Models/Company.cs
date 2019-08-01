using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioManager.Models
{
    public class Company
    {
        [Key]
        public string company_symbol { get; set; }
        [Required]
        public string company_name { get; set; }
        [Required]
        public string company_type { get; set; }
        [Required]
        public string company_sector { get; set; }
        [Required]
        public float company_capital { get; set; }
        [Required]
        public int company_totalNbrShares { get; set; }
        //the invested parts
        public virtual ICollection<Asset> assets { get; set; }
    }
}

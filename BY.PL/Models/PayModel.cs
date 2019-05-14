using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BY.PL.Models
{
    public class PayModel
    {
        [StringLength(50)]
        public string Name { get; set; }
      
        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(16)]
        public string CreditDard { get; set; }

        public decimal Bill { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BY.PL.Models
{
    public class PaymentModel
    {
        
        [StringLength(50)]
        public string Name { get; set; }

     
        [StringLength(50)]
        public string Surname { get; set; }

        
        [StringLength(50)]
        public string Username { get; set; }


        [StringLength(50)]
        public string Tc{ get; set; }


        [StringLength(50)]
        public string CreditCard { get; set; }


        [StringLength(50)]
        public decimal Bill { get; set; }


        [StringLength(50)]
        public decimal Tl { get; set; }



    }
}
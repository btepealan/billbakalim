using BY.Entity.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BY.Entity.Identity
{
   public class ApplicationUser :IdentityUser
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Adı")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Soyadı")]
        public string Surname { get; set; }

        public int Counter { get; set; }
        public string AccountNo { get; set; }

        public virtual List<UserTrans> UserTrans { get; set; }
        public virtual List<Bonus> Bonus { get; set; }
    }
}

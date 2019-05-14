using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BY.Entity.Entity
{[Table("Staff")]
   public class Staff:EntityBase
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Tckno { get; set; }
        public string ContactNo { get; set; }
       public DateTime StrWork { get; set; }
        public decimal Salary { get; set; }
        public string Mission { get; set; }
        public bool IsAdmin { get; set; }
        
    }
}

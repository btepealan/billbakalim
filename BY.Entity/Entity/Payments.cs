using BY.Entity.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BY.Entity.Entity
{[Table("Payments")]
   public class Payments:EntityBase
    {
        public string UserId { get; set; }
        public int UserTransId { get; set; }
        public string OrderType { get; set; }
        public decimal  Price { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [ForeignKey("UserTransId")]
        public virtual UserTrans UserTrans { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUsers { get; set; }

    }
}

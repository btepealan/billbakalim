using BY.Entity.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BY.Entity.Entity
{[Table("UserTrans")]
    public class UserTrans : EntityBase

      {
        public string UserId { get; set; }
        public int? ContestId { get; set; }
        public bool PaymentId { get; set; } = false;
        public bool IsWheel { get; set; } = false;
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Prize { get; set; }
        public decimal Loose { get; set; }
        public decimal Balance { get; set; }
      
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("ContestId")]
        public virtual Contest Contest { get; set; }
    }
}

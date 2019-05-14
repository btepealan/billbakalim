using BY.Entity.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BY.Entity.Entity
{[Table("Bonus")]
    public class Bonus : EntityBase
    {
        public int WheelValueId { get; set; }

        public string UserId { get; set; }
        public string BonusName { get; set; }
        public DateTime date { get; set; } = DateTime.Now;



        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        //[ForeignKey("WheelValueId")]
        //public virtual Wheel Wheel { get; set; }
    }

}

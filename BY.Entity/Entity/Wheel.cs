using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BY.Entity.Entity
{[Table("Wheel")]
   public class Wheel:EntityBase
    {
        public string Description { get; set; }

        //public virtual List<Bonus> Bonus { get; set; }

    }
}

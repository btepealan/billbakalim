using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BY.Entity.Entity
{[Table("ContestType")]
   public class ContestType:EntityBase
    {
        public string TypeName { get; set; }

        //public virtual List<Contest> Contests { get; set; }
    }
}

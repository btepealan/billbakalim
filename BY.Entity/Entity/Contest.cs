using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BY.Entity.Entity
{[Table("Contest")]
   public class Contest:EntityBase
    {
       
        public string ConName { get; set; }
        public int ConTypeId { get; set; }
        public int QuesCount { get; set; }
        public int Level { get; set; }
        public decimal Price { get; set; }
        public decimal Prize { get; set; }
        public DateTime Date { get; set; }
     
        public string Picture { get; set; }

       
        public virtual List<ContestDetails> ContestDetails { get; set; }

        public virtual List<UserTrans> UserTrans { get; set; }
        //[ForeignKey("ConTypeId")]
        //public virtual ContestType ContestType { get; set; }

    }
}

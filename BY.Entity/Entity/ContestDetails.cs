using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BY.Entity.Entity
{[Table("ContestDetails")]
   public class ContestDetails:EntityBase
    {
        public int ContestId { get; set; }
        public int QuesId  { get; set; }
        public string ContestType { get; set; }
        public int QuesNum { get; set; }
        public string Question { get; set; }
        public string Ans1 { get; set; }
        public string Ans2 { get; set; }
        public string Ans3 { get; set; }
        public string Ans4 { get; set; }
        public string TAns { get; set; }

        [ForeignKey("ContestId")]
        public virtual Contest Contest { get; set; }

    }
}

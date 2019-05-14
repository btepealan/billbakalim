using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BY.Entity.Entity
{[Table("CaseTrans")]
   public class CaseTrans : EntityBase
    {
        public int? UserTransId { get; set; }
        public int? StaffId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public string TransType { get; set; }

        [ForeignKey("UserTransId")]
       public virtual UserTrans UserTrans { get; set; }
        //[ForeignKey("StaffId")]
        //public virtual Staff Staffs { get; set; }

    }
}

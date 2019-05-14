using BY.DAL.Migrations;
using BY.Entity.Entity;
using BY.Entity.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BY.DAL.Context
{
  public  class BillBakalimContext: IdentityDbContext<ApplicationUser>
    {
        public BillBakalimContext() : base("BillBakalimContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BillBakalimContext, Configuration>("BillBakalimContext"));
        }

        public virtual DbSet<Bonus> Bonus { get; set; }
        public virtual DbSet<CaseTrans> CaseTrans { get; set; }
        public virtual DbSet<Contest> Contests { get; set; }
        public virtual DbSet<ContestDetails> ContestDetails { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<UserTrans> UserTrans { get; set; }
        public virtual DbSet<Wheel> Wheels { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<ContestType> ContestTypes { get; set; }

        //public System.Data.Entity.DbSet<BY.Entity.Identity.ApplicationUser> ApplicationUsers { get; set; }
    }
}

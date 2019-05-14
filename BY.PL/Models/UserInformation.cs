using BY.Entity.Entity;
using BY.Entity.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BY.PL.Models
{
    public class UserInformation
    {
        public UserTrans UserTransList { get; set; }
        public ApplicationUser ApplicationUserList { get; set; }

        //public List<UserTrans> UserTransList { get; set; }
        //public List<ApplicationUser> ApplicationUserList { get; set; }

    }
}
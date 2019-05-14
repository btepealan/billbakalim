using BY.BLL.Repository;
using BY.DAL.Context;
using BY.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BY.PL.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        BillBakalimContext ent = new BillBakalimContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Repository<ContestType> repoC = new Repository<ContestType>(ent);
            ViewBag.gametype = repoC.GetAll();
            

            base.OnActionExecuting(filterContext);
        }
    }
}
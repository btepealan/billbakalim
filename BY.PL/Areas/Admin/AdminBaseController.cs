using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BY.PL.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        // GET: Admin/AdminController
        protected override void Initialize(RequestContext requestContext)
        {
            
                base.Initialize(requestContext);
            
        }
    }
}
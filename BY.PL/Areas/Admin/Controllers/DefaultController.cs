using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BY.PL.Areas.Admin.Controllers
{
    public class DefaultController : AdminBaseController
    {
        // GET: Admin/Default
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult LogOut()
        {
            IAuthenticationManager authmanager = HttpContext.GetOwinContext().Authentication;
            authmanager.SignOut();
            // return RedirectToAction("Index", "Home");
            return Redirect("/home");
        }
    }
}
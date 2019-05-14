using BY.BLL.Repository;
using BY.DAL.Context;
using BY.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BY.PL.Controllers
{
    public class HomeController : Controller
    {
        Repository<ContestDetails> repoConDeta = new Repository<ContestDetails>(new BillBakalimContext());
        Repository<Contest> repoCon = new Repository<Contest>(new BillBakalimContext());
        public ActionResult Index()
        {
        //    List<Contest> con=repoCon.GetAll(x => x.IsDeleted == false).ToList();
        //    {
        //        foreach (Contest item in con)
        //        {
        //        if(item.Date.AddMinutes(5) <= DateTime.Now)
        //            {
        //                item.IsDeleted = true;
        //                repoCon.Update();
        //            }
        //        }
        //    }
            return View();
        }
        [Authorize]
        public ActionResult CanliDestek()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ContestOfDate()
        {
            return View(repoCon.GetAll().ToList());
        }
        public ActionResult ContestBegin()
        {
            var conlist=repoCon.GetAll(x=>x.Date>DateTime.Now);
           var list= conlist.OrderBy(x => x.Date).Take(1);
            // return View( conlist.OrderBy(x => x.Date).Take(1));
            var dif=list.FirstOrDefault().Date - DateTime.Now;
            if(dif.TotalMilliseconds >0 && dif.TotalMilliseconds <= 300000)
            {
                ViewBag.Start = "Başla";
            }
            return View(list);
        }
        //[HttpPost]
        //public ActionResult ContestBegin()
        //{
        //    return View();   
        //}

    }
}

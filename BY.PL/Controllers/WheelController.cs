
using BY.BLL.Repository;
using BY.DAL.Context;
using BY.Entity.Entity;
using BY.Entity.Identity;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BY.BLL.Identity;
using BY.PL.Models;

namespace BY.PL.Controllers
{
    public class WheelController : Controller
    {
        // GET: Wheel
        Repository<Wheel> ent = new Repository<Wheel>(new BillBakalimContext());
        Repository<UserTrans> ent2 = new Repository<UserTrans>(new BillBakalimContext());
        Repository<Bonus> ent3 = new Repository<Bonus>(new BillBakalimContext());
        Repository<Payments> Payment = new Repository<Payments>(new BillBakalimContext());

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UserTrans(string desc)
        {

            var usermanager = IdentityTools.NewUserManager();
            ApplicationUser kullanici = usermanager.FindByName(User.Identity.Name);
            Payments pay = new Payments();


            if (desc == "10.000 Bill")
            {
                UserTrans userTrans = new UserTrans();
                decimal totalb = ent2.TotalBalance(kullanici.Id);
                userTrans.UserId = kullanici.Id;
                userTrans.IsWheel = true;
                userTrans.Prize = 10000;
                userTrans.Loose = 1000;
                userTrans.Balance = totalb + 9000;
                ent2.Add(userTrans);

            }
            else if (desc == "100.000 Bill")
            {
                UserTrans userTrans = new UserTrans();
                decimal totalb = ent2.TotalBalance(kullanici.Id);
                userTrans.UserId = kullanici.Id;
                userTrans.IsWheel = true;
                userTrans.Prize = 100000;
                userTrans.Loose = 1000;
                userTrans.Balance = totalb + 99000;
                ent2.Add(userTrans);

            }
            else if (desc == "5.000 Bill")
            {
                UserTrans userTrans = new UserTrans();
                decimal totalb = ent2.TotalBalance(kullanici.Id);
                userTrans.UserId = kullanici.Id;
                userTrans.IsWheel = true;
                userTrans.Prize = 5000;
                userTrans.Loose = 1000;
                userTrans.Balance = totalb + 4000;
                ent2.Add(userTrans);

            }

            else if (desc == "Yanlış Cevap Jokeri")
            {
                Bonus name = new Bonus();
                UserTrans userTrans = new UserTrans();
                decimal totalb = ent2.TotalBalance(kullanici.Id);
                userTrans.UserId = kullanici.Id;
                name.UserId = userTrans.UserId;
                userTrans.Balance = totalb - 1000;
                userTrans.Loose = 1000;
                userTrans.Prize = 0;
                name.WheelValueId = ent.Get(x=>x.Description== "Yanlış Cevap Jokeri").Id;
                name.BonusName = "Yanlış Cevap Jokeri";
                ent3.Add(name);
                ent2.Add(userTrans);
            }
            else if (desc == "+ Saniye")
            {
                Bonus name = new Bonus();
                UserTrans userTrans = new UserTrans();
                decimal totalb = ent2.TotalBalance(kullanici.Id);
                userTrans.UserId = kullanici.Id;
                name.UserId = userTrans.UserId;
                userTrans.Loose = 1000;
                userTrans.Prize = 0;
                userTrans.Balance = totalb - 1000;
                name.WheelValueId = ent.Get(x => x.Description == "+ Saniye").Id;
                name.BonusName = "+ Saniye";
                ent3.Add(name);
                ent2.Add(userTrans);
            }
            else if (desc == "Tekrar Çevir")
            {
                UserTrans userTrans = new UserTrans();
                decimal totalb = ent2.TotalBalance(kullanici.Id);
                userTrans.UserId = kullanici.Id;
                userTrans.Loose = 0;
                userTrans.Prize = 0;
                userTrans.Balance = totalb;
                ent2.Add(userTrans);
                // tekrar cevir.
            }
            else if (desc == "Boş")
            {
                UserTrans userTrans = new UserTrans();
                decimal totalb = ent2.TotalBalance(kullanici.Id);
                userTrans.UserId = kullanici.Id;
                userTrans.Loose = 1000;
                userTrans.Prize = 0;
                userTrans.Balance = totalb - 1000;
                ent2.Add(userTrans);
                // Hiç bir şey olmasın
            }
            return Json(desc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult WheelList()
        {
            var desc = ent.GetAll().ToList();
            return Json(desc, JsonRequestBehavior.AllowGet);
        }
      
    }
}

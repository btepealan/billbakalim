using BY.BLL.Identity;
using BY.BLL.Repository;
using BY.DAL.Context;
using BY.Entity.Entity;
using BY.Entity.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BY.PL.Controllers
{
    public class GameController : Controller
    {
        Repository<ContestDetails> repoConDeta = new Repository<ContestDetails>(new BillBakalimContext());
        Repository<Contest> repoCon = new Repository<Contest>(new BillBakalimContext());
        Repository<UserTrans> repoUserTrans = new Repository<UserTrans>(new BillBakalimContext());
        Repository<ApplicationUser> repoApp = new Repository<ApplicationUser>(new BillBakalimContext());
        Repository<Bonus> repoBonus = new Repository<Bonus>(new BillBakalimContext());
        // GET: Game

        [Authorize]
        public ActionResult Index()
        {
            Bonus bonus = new Bonus();
            var usermanager = IdentityTools.NewUserManager();
            //aktif kullanici
            ApplicationUser appuser = usermanager.FindByName(User.Identity.Name);
            //aktif kullanıcının hareketler
            var kullanici = repoUserTrans.Get(x => x.UserId == appuser.Id);
            //aktip kullanıcının toplam bakiyesi
            decimal totalb = repoUserTrans.TotalBalance(kullanici.UserId);
            appuser.Counter++;
            usermanager.Update(appuser);

            if (appuser.Counter - 1 == 0)
            {
              var basiccontest= repoCon.GetAll(x => x.Level == 1);
              var bc = basiccontest.OrderByDescending(x => x.Date).Take(1).FirstOrDefault();
                List<ContestDetails> basic = repoConDeta.GetAll(x => x.ContestId == bc.Id).ToList();
                if (totalb < bc.Price)
                {
                    ViewBag.Message = "Bu yarışmaya katılmak için yeterli Bill yok, hesabım kısmından yükleme yapabilirsiniz";
                    return Redirect("/Home/Index");
                }
                else
                {

                    UserTrans trans = new UserTrans();

                    trans.UserId = appuser.Id;
                    trans.ContestId = bc.Id;
                    trans.Prize = 0;
                    trans.Loose = bc.Price;
                    trans.Balance = totalb - bc.Price;
                    repoUserTrans.Add(trans);
                }
                //şuanda yarışmadaki bütün sorular burda -->cd
                for (int i = 0; i < basic.Capacity; i++)
                {
                    return View(basic[i]);
                }
            }
            //başlama tarihi şuan olan yarışma
           
            var conlist = repoCon.GetAll(x=>x.IsDeleted==false && x.Date>DateTime.Now);
              //bütün yarışmaları tarihe göre sıralayıp en yakını aldım
            var cont = conlist.OrderBy(x => x.Date).Take(1).FirstOrDefault();

            //başlayacak olan yarışmanın soruları
            List<ContestDetails> cd = repoConDeta.GetAll(k => k.ContestId == cont.Id).ToList();
            
            if (totalb < cont.Price)
            {
                ViewBag.Bakiye = "Bu yarışmaya katılmak için yeterli Bill yok, hesabım kısmından yükleme yapabilirsiniz";
                return Redirect("/Home/Index");
            }
            else
            {
                UserTrans trans = new UserTrans();

                trans.UserId = appuser.Id;
                trans.ContestId = cont.Id;
                trans.Prize = 0;
                trans.Loose = cont.Price;
                trans.Balance = totalb - cont.Price;
                repoUserTrans.Add(trans);
            }
            var bonuslist = repoBonus.GetAll(x => x.UserId == appuser.Id && x.IsDeleted == false);
            if (bonuslist != null)
            {
                foreach (var item in bonuslist)
                {
                    if (item.WheelValueId == 17 || item.WheelValueId==10)
                    {
                        ViewBag.Saniye = "+";
                    }

                }
            }
            //şuanda yarışmadaki bütün sorular burda -->cd
            for (int i = 0; i < cd.Capacity; i++)
            {
                return View(cd[i]);
            }

         
            return View(cd);
        }
        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            var usermanager = IdentityTools.NewUserManager();
            //aktif kullanici
            ApplicationUser appuser = usermanager.FindByName(User.Identity.Name);
            var bonus = repoBonus.GetAll(x => x.UserId == appuser.Id && x.IsDeleted == false);
            //yarışma id
            int conid = Convert.ToInt16(frm["conid"]);

            var contest = repoCon.Get(x => x.Id == conid);
            //yarışma soruları vvar
            List<ContestDetails> cde = repoConDeta.GetAll(k => k.ContestId == conid).ToList();
            //yarışma soruların id ekrandan geldi

            int id = Convert.ToInt16(frm["id"]);
            //soru

            ContestDetails cd = repoConDeta.Get(k => k.Id == id);

            string cvp = frm["answer"].ToString();
            string numara = frm["num"].ToString();
            if (bonus != null)
            {
                foreach (var item in bonus)
                {
                    if (item.WheelValueId == 17 || item.WheelValueId==10)
                    {
                        ViewBag.Saniye = "+";
                    }

                }
            }

            if (cd.TAns == cvp)
            {
                if ((contest.QuesCount).ToString() == numara)
                {
                    var lasttrans = repoUserTrans.GetAll().OrderByDescending(x => x.Date).Take(1).FirstOrDefault();
                    lasttrans.Prize = contest.Prize;
                    lasttrans.Balance = repoUserTrans.TotalBalance(appuser.Id) + contest.Prize;
                   repoUserTrans.Update();
                    return View("Win");

                }

                return View(cde[Convert.ToInt16(numara)]);
            }
            else
            {
                if (bonus != null)
                {
                    foreach (var item in bonus)
                    {
                        if (item.WheelValueId == 9 && item.IsDeleted==false)
                        {
                            item.IsDeleted = true;
                            repoBonus.Update();
                            ViewBag.Joker = "Yanlış cevap jokeri kullanıldı";
                            return View(cde[Convert.ToInt16(numara)]);
                        }
                    }
                }
                return View("Loose");
            }
           
        }
        public ActionResult Loose()
        {
            return View();
        }
        public ActionResult Win()
        {
            return View();
        }
        [HttpPost]
        public JsonResult BonusUpdate()
        {
            var usermanager = IdentityTools.NewUserManager();
            //aktif kullanici
            ApplicationUser appuser = usermanager.FindByName(User.Identity.Name);

            var bonus = repoBonus.GetAll(x => x.UserId == appuser.Id && x.IsDeleted == false && x.BonusName== "+ Saniye").OrderBy(x=>x.date).Take(1);
            foreach (var item in bonus)
            {
                item.IsDeleted = true;
            }
            bool result = repoBonus.Update();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
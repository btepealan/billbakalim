
using BY.BLL.Identity;
using BY.BLL.Repository;
using BY.DAL.Context;
using BY.Entity.Entity;
using BY.Entity.Identity;
using BY.PL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace BY.PL.Controllers
{
    public class AccountController : Controller
    {
        BillBakalimContext ent = new BillBakalimContext();
        Repository<UserTrans> repoUser = new Repository<UserTrans>(new BillBakalimContext());
        Repository<ApplicationUser> repoApp = new Repository<ApplicationUser>(new BillBakalimContext());
        Repository<Payments> repoPayments = new Repository<Payments>(new BillBakalimContext());
        Repository<CaseTrans> repoCaseTrans = new Repository<CaseTrans>(new BillBakalimContext());

        // GET: Account
        private UserManager<ApplicationUser> userManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new BillBakalimContext());
            userManager = new UserManager<ApplicationUser>(userStore);
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            Repository<Questions> rq = new Repository<Questions>(ent);
            ViewBag.Kategoriler = rq.GetAll();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var kullanici = userManager.FindByEmail(model.Email);
            
            if (kullanici != null)
            {
                ModelState.AddModelError("", "Bu e-mail sistemde kayıtlı");
                return View(model);
            }
            var rolemanager = IdentityTools.NewRoleManager();
            ApplicationRole ar = new ApplicationRole();
            ar.Name = "User";
            rolemanager.Create(ar);
            ApplicationUser user = new ApplicationUser();
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.AccountNo = model.AccountNo;
            user.PhoneNumber = model.ContactNo;
            user.UserName = model.Username;

            var result = userManager.Create(user, model.Password);
            userManager.AddToRole(user.Id, "User");
            if (result.Succeeded)
            {//üye olan kullanıcıya hediye 5000 bil verdim
                UserTrans userTrans = new UserTrans();
                userTrans.UserId = user.Id;
                userTrans.PaymentId = true;
                userTrans.Prize = 5000;
                userTrans.Loose = 0;
                userTrans.Balance = 5000;
                repoUser.Add(userTrans);
               return RedirectToAction("Login");
               
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult Login (string returnUrl)
        {
            LoginViewModel model = new LoginViewModel()
            {
                returnUrl = returnUrl
            };
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            
            if (!ModelState.IsValid)
               return View(model);
            var kullanici = userManager.FindByName(model.Username);
            if (kullanici == null)
            {
                ModelState.AddModelError("", "Böyle bir kullanıcı kayıtlı değil");
                return View();
            }
            else
            {
                var authManager = HttpContext.GetOwinContext().Authentication;
                var identity = userManager.CreateIdentity(kullanici, "ApplicationCookie");
                var authProperty = new AuthenticationProperties
                {
                    IsPersistent = false
                };

                authManager.SignIn(authProperty, identity);
                if(kullanici.Roles.FirstOrDefault().RoleId == "2c7433ca-151b-416f-a74a-e91a5c0ebbce")
                {
                    return Redirect("/admin");
                }
                return Redirect("/Home/Index");
            }
        }
        [Authorize]
        public ActionResult LogOut()
        {
            IAuthenticationManager authmanager = HttpContext.GetOwinContext().Authentication;
            authmanager.SignOut();
            return RedirectToAction("Index","Home");
        }
        [Authorize]
        public ActionResult AccountInfo()
        {//login olan kullanıcının kullanıcı adı
            var usermanager = IdentityTools.NewUserManager();
            UserInformation user = new UserInformation();
            user.ApplicationUserList = usermanager.FindByName(User.Identity.Name);
            string UserId = user.ApplicationUserList.Id;

            decimal sumprize = repoUser.GetAll(x=>x.UserId==UserId).Sum(x => x.Prize);
            ViewBag.totalprize = sumprize;

            user.UserTransList = repoUser.Get(x => x.UserId == UserId);

            ViewBag.total = repoUser.TotalBalance(user.ApplicationUserList.Id);

            //user nesnesini gönderdim
            return View(user);
        }
        [Authorize]
        [HttpPost]
        public ActionResult AccountInfo(UserInformation model)
        { 
            ApplicationUser au = new ApplicationUser();
            au=userManager.FindByName(User.Identity.Name);
            au.Name=model.ApplicationUserList.Name;
            au.Surname = model.ApplicationUserList.Surname;
            au.UserName = model.ApplicationUserList.UserName;
            au.Email = model.ApplicationUserList.Email;
            au.AccountNo = model.ApplicationUserList.AccountNo;
            au.PhoneNumber = model.ApplicationUserList.PhoneNumber;
            userManager.Update(au);
            ViewBag.Message = "Bilgileriniz Güncellendi";
            return Redirect("Login");
        }

        public ActionResult Payment()
        {
            PayModel pm = new PayModel();
           var list= userManager.FindByName(User.Identity.Name);
            pm.Name = list.Name;
            pm.Surname = list.Surname;
            return View(pm);
        }
        [HttpPost]
        public ActionResult Payment(PayModel model)
        {
            UserTrans us = new UserTrans();
            Payments paym = new Payments();
            CaseTrans ct = new CaseTrans();
            var user = userManager.FindByName(User.Identity.Name);
            decimal totalb = repoUser.TotalBalance(user.Id);
            us.UserId = user.Id;
            us.Prize = 0;
            us.Loose = 0;
            us.PaymentId = true;
            us.Balance = totalb + model.Bill;
            repoUser.Add(us);
            paym.UserId = user.Id;
            paym.UserTransId = us.Id;
            paym.Price =((model.Bill) *Convert.ToDecimal(7.42)/1000);
            paym.OrderType = "Ödeme";
            repoPayments.Add(paym);
            ct.UserTransId = us.Id;
            ct.Income = paym.Price;
            ct.Expense = 0;
            ct.TransType = "Bill Satış";
            repoCaseTrans.Add(ct);
            return RedirectToAction("AccountInfo", "Account");
        }
        [HttpPost]
        public JsonResult PaymTrans(decimal desc)
        {
            decimal trans = desc * (Convert.ToDecimal(7.42) / 1000);
            var user = userManager.FindByName(User.Identity.Name);
            UserTrans us = new UserTrans();
            us.Prize = 0;
            us.Balance = repoUser.TotalBalance(user.Id) - desc;
            us.UserId = user.Id;
            us.Loose = 0;
            repoUser.Add(us);
            CaseTrans cs = new CaseTrans();
            cs.UserTransId = us.Id;
            cs.Income = 0;
            cs.Expense = trans;
            cs.TransType = "Kullanıcı Ödemesi";
            bool result = repoCaseTrans.Add(cs);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BY.BLL.Repository;
using BY.DAL.Context;
using BY.Entity.Entity;

namespace BY.PL.Areas.Admin.Controllers
{
    public class ContestsController : BaseController
    {
        BillBakalimContext db = new BillBakalimContext();
        Repository<ContestType> repoConType = new Repository<ContestType>(new BillBakalimContext());

        // GET: Admin/Contests
        public ActionResult Index()
        {
            return View(db.Contests.ToList());
        }

        // GET: Admin/Contests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contest contest = db.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        // GET: Admin/Contests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ConName,ConTypeId,QuesCount,Level,Price,Prize,Date,IsDeleted")] Contest contest)
        {
            switch (contest.ConTypeId)
            {
                case 2:
                    contest.Picture = "/img/genelkültür.jpg";
                    break;
                case 4:
                    contest.Picture = "/img/cografyayarisma.jpg";
                    break;
                case 5:
                    contest.Picture = "/img/sanat.jpg";
                    break;
                case 6:
                    contest.Picture = "/img/tarih.jpg";
                    break;
                case 7:
                    contest.Picture = "/img/spor.jpg";
                    break;
                case 8:
                    contest.Picture = "/img/genelyarisma.jpg";
                    break;
            }
            if (ModelState.IsValid)
            {
                ContestDetails cd = new ContestDetails();
                ContestType contype = repoConType.Get(x => x.Id == contest.ConTypeId);
                string type = contype.TypeName;
                db.Contests.Add(contest);
                for (int i = 0; i < contest.QuesCount; i++)
                {
                    Questions qe = new Questions();
                    if (type == "Genel")
                        qe = db.Questions.Where(u => u.IsDeleted == false).OrderBy(u => Guid.NewGuid()).Take(1).FirstOrDefault();

                    else
                        qe = db.Questions.Where(u => u.IsDeleted == false && u.QType == type).OrderBy(u => Guid.NewGuid()).Take(1).FirstOrDefault();

                    cd.ContestId = contest.Id;
                    cd.QuesId = qe.Id;
                    cd.ContestType = type;
                    cd.QuesNum = (i + 1);
                    cd.Question = qe.Question;
                    cd.Ans1 = qe.Ans1;
                    cd.Ans2 = qe.Ans2;
                    cd.Ans3 = qe.Ans3;
                    cd.Ans4 = qe.Ans4;
                    cd.TAns = qe.TrueAns;
                    cd.IsDeleted = false;
                    db.ContestDetails.Add(cd);
                    qe.IsDeleted = true;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(contest);
        }

        // GET: Admin/Contests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contest contest = db.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        // POST: Admin/Contests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ConName,ConTypeId,QuesCount,Level,Price,Prize,Date,Picture,IsDeleted")] Contest contest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contest);
        }

        // GET: Admin/Contests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contest contest = db.Contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        // POST: Admin/Contests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contest contest = db.Contests.Find(id);
            db.Contests.Remove(contest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

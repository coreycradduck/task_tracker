using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{
    public class TimersController : Controller
    {
        private dbTask db = new dbTask();

        // GET: Timers
        public ActionResult Index()
        {
            var viewModel = from ts in db.Timers
                            join tg in db.Tags on ts.TaskId equals tg.Id
                            select new TimerTagViewModel { TimerId = ts.Id,
                                TaskId = tg.Id,
                                Description = tg.Description,
                                start_timestamp = ts.start_timestamp,
                                end_timestamp = ts.end_timestamp};

            return View(viewModel.ToList());
        }

        // GET: Timers/Summary
        public ActionResult Summary()
        {
            var viewModel = from ts in db.Timers
                            join tg in db.Tags on ts.TaskId equals tg.Id
                            select new SummaryViewModel
                            {
                                Description = tg.Description,
                                SecDiff = DbFunctions.DiffSeconds(ts.start_timestamp, ts.end_timestamp)
                            };

            var sumModel = viewModel.GroupBy(x => x.Description)
                .Select(y => new SummaryViewModel
                {
                    Description = y.Key,
                    SecDiff = y.Sum(x => x.SecDiff)
                })
                .OrderByDescending(o => o.SecDiff);

            return View(sumModel);
        }

        // GET: Timers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timer timer = db.Timers.Find(id);
            if (timer == null)
            {
                return HttpNotFound();
            }
            return View(timer);
        }

        // GET: Timers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Timers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,TaskId,start_timestamp,end_timestamp")] Timer timer)
        {
            if (ModelState.IsValid)
            {
                db.Timers.Add(timer);
                db.SaveChanges();
                // return RedirectToAction("Index");
            }

            return View(timer);
        }

        // GET: Timers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timer timer = db.Timers.Find(id);
            if (timer == null)
            {
                return HttpNotFound();
            }
            return View(timer);
        }

        // POST: Timers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,start_timestamp,end_timestamp")] Timer timer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timer);
        }

        // GET: Timers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timer timer = db.Timers.Find(id);
            if (timer == null)
            {
                return HttpNotFound();
            }
            return View(timer);
        }

        // POST: Timers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Timer timer = db.Timers.Find(id);
            db.Timers.Remove(timer);
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

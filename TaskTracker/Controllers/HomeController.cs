using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{
    public class HomeController : Controller
    {
        private dbTask db = new dbTask();

        public ActionResult Index()
        {
            var viewModel = (from ts in db.Timers
                            join tg in db.Tags on ts.TaskId equals tg.Id
                            select new TimerTagViewModel
                            {
                                TimerId = ts.Id,
                                TaskId = tg.Id,
                                Description = tg.Description,
                                start_timestamp = ts.start_timestamp,
                                end_timestamp = ts.end_timestamp
                            })
                            .OrderByDescending(o => o.end_timestamp)
                            .Take(5);

            var model = new TagsAndRecentTimers { Tag = db.Tags.ToList(), TimerTag = viewModel };
            return View(model);
        }

        public ActionResult Journal()
        {
            return View();
        }
    }
}
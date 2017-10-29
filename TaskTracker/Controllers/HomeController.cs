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
            return View(db.Tags.ToList());
        }

        public ActionResult Journal()
        {
            return View();
        }
    }
}
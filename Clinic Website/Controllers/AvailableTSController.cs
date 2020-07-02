using Clinic_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic_Website.Controllers
{
    public class AvailableTSController : Controller
    {
        private ApplicationDbContext db;
       public AvailableTSController()
        {
            db = new ApplicationDbContext();

        }
        public ActionResult Index()
        {
            var model = db.AvailableTimesLists.ToList();


            return View(model);
        }
    }
}
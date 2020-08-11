using Clinic_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic_Website.Controllers
{
    public class AppHistoryController : Controller
    {
        private ApplicationDbContext db;

        public AppHistoryController()
        {

            db = new ApplicationDbContext();
        }

        // GET: AppHistory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult App_graph()
        {
           ViewBag.Attended = db.Appointments.Where(z => z.AppointmentStatus.Name == "Attended").ToList().Count;
            ViewBag.NotAttended = db.Appointments.Where(z => z.AppointmentStatus.Name == "Not Attended").ToList().Count;

            ViewBag.Cancelled = db.Appointments.Where(z => z.AppointmentStatus.Name == "Cancelled").ToList().Count;

          //  var Scheduled = db.Appointments.Where(z => z.AppointmentStatus.Name == "Scheduled ").ToList().Count;

            return View();
        }


    }
}
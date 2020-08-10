using Clinic_Website.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic_Website.Controllers
{
    [Authorize]
    public class PatientStateController : Controller
    {
        private ApplicationDbContext db;

        public PatientStateController()
        {

            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult YourStates()
        {
            string currentUserId = User.Identity.GetUserId();

            var model = from r in db.PatientStates
                        where r.PatientId == currentUserId
                        select r;


            return View(model) ;
        }

    }
}
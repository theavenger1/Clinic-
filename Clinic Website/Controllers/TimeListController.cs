using Clinic_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic_Website.Controllers
{
    public class TimeListController : Controller
    {
        private ApplicationDbContext db;
        public TimeListController()
         {
                db = new ApplicationDbContext();
            }
 
    public void Maintain()
        {
            



        }


        // GET: TimeList
        public ActionResult Index()
        {
            var model = from r in db.TimeSlotLists
                        orderby r.Id
                        select r;

            return View(model);
        }
    }
}
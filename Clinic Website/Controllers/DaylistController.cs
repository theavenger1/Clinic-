using Clinic_Website.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Clinic_Website.Controllers
{
    public class DaylistController : Controller
    {  private ApplicationDbContext db;
       public DaylistController()
            {
                db = new ApplicationDbContext();
            }
           
        [Authorize(Roles ="Admins")]
        public ActionResult Index()
            {
         

                var model = from r in db.DayLists
                            orderby r.Id
                            select r;

                return View(model);
            }
        
        [Authorize(Roles = "Doctor")]
        public ActionResult YourclinicDaylist()
        {
            string currentUserId = User.Identity.GetUserId();

            var model = from r in db.DayLists
                        where r.Clinic.userId==currentUserId
                        orderby r.Id
                        select r;

            return View(model);
        }
        [Authorize(Roles = "Doctor")]
        public ActionResult ClinicDays(int? Id)
        {
            string currentUserId = User.Identity.GetUserId();

            var model = from r in db.DayLists
                        where r.Clinic.userId == currentUserId && r.ClinicId==Id
                        orderby r.Id
                        select r;

            

            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
            {

            string currentUserId = User.Identity.GetUserId();

            var model = from r in db.Clinics
                        where r.userId == currentUserId 

                        select r;


             if (model == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }


            var x = model.ToList();
            ViewBag.Clinics = new SelectList(x, "Id", "ClinicName");

            return View();
            }
         
        [HttpPost]
        public ActionResult Create(DayList d, FormCollection f)
        {
            string clinicid = f["Clinics"].ToString();

           

            if (clinicid == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }


            d.ClinicId = int.Parse(clinicid);
            var c = db.Clinics.Find(d.ClinicId);
            if (c.DayLists.ToList().Find(q => q.DayName == d.DayName) != null) { return RedirectToAction("YourclinicDaylist"); }

            if (ModelState.IsValid)
            {


                db.DayLists.Add(d);
                for (int i = (int)c.StartTime; i < (int)c.EndTime; i++)
                {
                    AvailableTimesList availableTimes = new AvailableTimesList { DayListId = d.Id, Taken = false, Slot_start = (TimeSlots)i };

                    db.AvailableTimesLists.Add(availableTimes);

                }

                db.SaveChanges();

                return RedirectToAction("YourclinicDaylist");
            }


            return View();
        }

        private void maintain(Clinic c)
        {

            
            var model = from r in db.DayLists
                        where r.ClinicId == c.Id

                        select r;
            var x = model.ToList();

          

        }

        public ActionResult Delete(int? id)

        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = User.Identity.GetUserId();

            DayList day = db.DayLists.Find(id);

            if (day.Clinic.userId == user)
            {
                db.DayLists.Remove(day);
                db.SaveChanges();

                return RedirectToAction("ClinicDays",new { Id=id });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

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

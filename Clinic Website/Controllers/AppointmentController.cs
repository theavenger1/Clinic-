using Clinic_Website.Migrations;
using Clinic_Website.Models;
using Hangfire.Storage.Monitoring;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Controllers;

namespace Clinic_Website.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private ApplicationDbContext db;

       public     AppointmentController()
        {
      
            db = new ApplicationDbContext();
        }
        [Authorize(Roles = "Admins")]
        public ActionResult Index()
        {
            var model = db.Appointments.ToList();


            return View(model);
        }

        public ActionResult MyAppointments( int? be)
        {
            string currentUserId = User.Identity.GetUserId();

            var model = from r in db.Appointments
                        where r.PatientState.PatientId == currentUserId
                        orderby r.Date_Created
                        select r;

            if (model ==null)
                { return View("NoApp"); }


            if (be == 1) { ViewBag.Result = "You cannot cancel this Appointment , please contact the clinic"; }
            return View(model);
        }





        [HttpGet]
        public ActionResult Create(DateTime date, TimeSlots time, int Id, int time1 )
        {         
            string currentUserId = User.Identity.GetUserId();

            var c = db.Appointments.Where(x => x.PatientState.PatientId == currentUserId && x.ClinicId == Id && x.DayofApp == date).ToList();

            if (c.Count >= 1)
            {

             return RedirectToAction("Details", "Home", new { ClinicId = Id, be=1 });

            }
            ViewBag.ClinicId = Id;
            ViewBag.Clinic = db.Clinics.Find(Id).ClinicName;
            ViewBag.date = date.ToString("dddd, dd MMMM yyyy");
            ViewBag.time = time.GetDisplayName();
            ViewBag.timeid=time1;

            return View();
        }
        [HttpPost]
        public ActionResult Create(DateTime date, TimeSlots time, int Id,int time1, FormCollection f)
        {
          // check if patient has already make an appointment with same clinic in same day 
            string currentUserId = User.Identity.GetUserId();
 
            var c = db.Appointments.Where(x => x.PatientState.PatientId == currentUserId && x.ClinicId == Id && x.DayofApp == date).ToList();

            if (c.Count > 1)
            { 
                ViewBag.Result = "You have applied before !";
 
                return View();
            }

            // check if he has a previous Patient state with same name
            string patientstatename = f["pa"];
            PatientState p;
            var ExpatientStates = db.PatientStates.Where(x => x.PatientId == currentUserId && x.StateName == patientstatename).ToList();

            // new patientstate if not there one
            if (ExpatientStates.Count == 0)
            {
                  p = new PatientState { PatientId = currentUserId, StateName=patientstatename, InProgress = true, StartTime = date };
                db.PatientStates.Add(p);

                db.SaveChanges(); 
            }

            //get the old one
            else {   p = ExpatientStates.FirstOrDefault(); }

            #region old way to get ava


            //Days st = Days.Saturday;
            //Days q=Days.Saturday;
            //for (int i = 1; i < 8; i++)
            //{
            //    if (q.GetDisplayName() == date.DayOfWeek.ToString()) {   st = q; }
            //    q = (Days)i;
            //}
            //get the time slot to make it unavailable :   1) get the day 
            //var day= from r in db.DayLists
            //               where r.ClinicId == Id && (r.DayName==st)
            //               select r;



            //    var x = db.AvailableTimesLists.TakeWhile(t => t.DayListId == day.First().Id && t.Slot_start == time);

            //var tim = from t in db.AvailableTimesLists
            //          where t.DayListId == day  && t.Slot_start == time
            //          select t;


            // var a = db.AvailableTimesLists.Where(s => s.DayListId == day.FirstOrDefault().Id && s.Slot_start == time).FirstOrDefault();


            //var a = tim.FirstOrDefault();
            #endregion    

            // get time slot of this day to make it taken 
            var a = db.AvailableTimesLists.Find(time1);
            a.Taken = true;
             
          db.Entry(a).State = EntityState.Modified;

          db.SaveChanges();
          
            //make an appointment with this tSlot  with status "scheduled" 
            var model = from r in db.AppointmentStatus
                        where r.Name == "Scheduled"
                        select r;
         
            Appointment app = new Appointment { ClinicId = Id, AppointmentStatusId=model.First().Id, DayofApp = date, PatientStateId = p.Id, TimeStart = time ,Slot=time1};
            db.Appointments.Add(app);
            db.SaveChanges();


            StatusHistory s = new StatusHistory { StatusId = model.First().Id, AppointmentId = app.Id, Details = "first" };

            db.StatusHistories.Add(s);
            db.SaveChanges();
            return RedirectToAction("MyAppointments");

            #region commented
            //if (clinicid == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }


            //d.ClinicId = int.Parse(clinicid);
            //var c = db.Clinics.Find(d.ClinicId);
            //if (c.DayLists.ToList().Find(q => q.DayName == d.DayName) != null) { return RedirectToAction("YourclinicDaylist"); }

            //if (ModelState.IsValid)
            //{


            //    db.DayLists.Add(d);
            //    for (int i = (int)c.StartTime; i < (int)c.EndTime; i++)
            //    {
            //        AvailableTimesList availableTimes = new AvailableTimesList { DayListId = d.Id, Taken = false, Slot_start = (TimeSlots)i };

            //        db.AvailableTimesLists.Add(availableTimes);

            //    }

            //    db.SaveChanges();

            //    return RedirectToAction("YourclinicDaylist");
            //}

            #endregion

        }


        public ActionResult Cancel (int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var appointment = db.Appointments.Find(id);

            if (appointment == null)
            {
                return HttpNotFound();

            }
             
            //check the availability to cancel  
            if (appointment.DayofApp.DayOfWeek - DateTime.Now.DayOfWeek < 1)
            {
                
               // ViewBag.Result = "You cannot cancel this Appointment , please contact the clinic ";

                return RedirectToAction("MyAppointments", new { be = 1 });
            }
          
            //cancelling 


            var model = from r in db.AppointmentStatus
                        where r.Name == "Cancelled"
                        select r;
            appointment.AppointmentStatusId = model.First().Id;

            //put the change in status history 
            StatusHistory s = new StatusHistory { AppointmentId = appointment.Id, StatusId = model.First().Id, Details = "before appointment " };
            db.StatusHistories.Add(s);

            // make the slot taken = false      
            var a = db.AvailableTimesLists.Find(appointment.Slot);
            a.Taken = false;

            db.Entry(a).State = EntityState.Modified;
            db.SaveChanges();


            db.Entry(appointment).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("MyAppointments");



        }



    }
}
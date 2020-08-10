using Clinic_Website.Migrations;
using Clinic_Website.Models;
using Clinic_Website.Controllers;
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
using Hangfire;

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

        [Authorize(Roles = "Doctor")]
        public ActionResult ClinicApps(int? id)
        {

            if (id == null)                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           
            
            var currentUserId = User.Identity.GetUserId();
            Clinic clinic = db.Clinics.Find(id);

            if (clinic.userId!=currentUserId) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            var model = from r in db.Appointments
                        where r.ClinicId==id
                        orderby r.Date_Created
                        select r;
       
            if (model == null)  return View("NoApp"); 
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

        public ActionResult State_Apps(int? state)
        {
            string currentUserId = User.Identity.GetUserId();

            var model = from r in db.Appointments
                        where r.PatientStateId == state
                        orderby r.Date_Created
                        select r;
  
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult Create(DateTime date, TimeSlots time, int Id, int time1 )
        {         
         
            ViewBag.ClinicId = Id;
            ViewBag.Clinic = db.Clinics.Find(Id).ClinicName;
            ViewBag.date = date.ToString("dddd, dd MMMM yyyy");
            ViewBag.time = time.GetDisplayName();
            ViewBag.timeid=time1;
           
            return View(db.Clinics.Find(Id));
        }
        [HttpPost]
        public ActionResult Create(DateTime date, TimeSlots time, int Id,int time1, FormCollection f)
        {
            // check if patient has already made an appointment with same clinic in same day 
            string currentUserId = User.Identity.GetUserId();
            var clinic= db.Clinics.Find(Id);
            var c = db.Appointments.Where(x => x.PatientState.PatientId == currentUserId && x.ClinicId == Id && x.DayofApp == date&& x.AppointmentStatus.Name == "Scheduled").ToList();

            if (c.Count >= 1)
            { 
                ViewBag.Result = "You have applied before !";

                return View(db.Clinics.Find(Id));
            }
           
            // check if patient has made an appointment with another clinic in same time slot

            var s= db.Appointments.Where(x => x.PatientState.PatientId == currentUserId &&  x.DayofApp == date && x.AppointmentStatus.Name == "Scheduled" && x.Slot==time1).ToList();

            if (s.Count >= 1)
            {
                ViewBag.Result = "You have another appointment in this date !";

                return View(db.Clinics.Find(Id));
            }

            // check if he has a previous Patient state with same name with same category 
            string patientstatename = f["pa"];

            List<Appointment> check =new List<Appointment>();   

            
            var ExpatientStates = db.PatientStates.Where(x => x.PatientId == currentUserId && x.StateName == patientstatename).ToList();
          if (ExpatientStates.Count != 0) { 
             var st1 = ExpatientStates.FirstOrDefault();
              check = db.Appointments.Where(u => u.PatientStateId == st1.Id && u.Clinic.CategoryId == clinic.CategoryId).ToList();
            }
            //if (check.Count == 0)
            PatientState p;
            // new patientstate if not there one
            if (ExpatientStates.Count == 0|| check.Count == 0)
            { 
                    p = new PatientState { PatientId = currentUserId, StateName = patientstatename, InProgress = true, StartTime = date };
                    db.PatientStates.Add(p);

                    db.SaveChanges();
                
            }

            //get the old one
            else
            {
              var qqqqq=  from e in check
                where e.Clinic.CategoryId == clinic.CategoryId
                select e.PatientStateId ;

                int id = qqqqq.FirstOrDefault();


                p = ExpatientStates.FirstOrDefault(e=>e.Id==id); }

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


            StatusHistory statushistory = new StatusHistory { StatusId = model.First().Id, AppointmentId = app.Id, Details = "first" };

            db.StatusHistories.Add(statushistory);
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

        [Authorize(Roles = "Doctor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var appointment = db.Appointments.Find(id);

            if (appointment == null)
            {
                return HttpNotFound();

            }
            string currentUserId = User.Identity.GetUserId();
            if (appointment.Clinic.userId != currentUserId) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.sts= new SelectList(db.AppointmentStatus, "Id", "Name");

            return View(appointment);



        }
        [HttpPost]
        public ActionResult Edit(Appointment app,FormCollection f)
        {
            string sts = f["sts"].ToString();
            string patientstatename = f["PSED"];

            var patientState = db.PatientStates.Find(app.PatientStateId);
            //CHANGE STATE NAME TO NEW NAME 

            if (patientstatename != null)
            {

                patientState.StateName = patientstatename;
                db.Entry(patientState).State = EntityState.Modified;
                db.SaveChanges();
            }
          
            if (ModelState.IsValid)
            {
           #region change status & save this change into status history 

                int APPSTATid = int.Parse(sts);
                app.AppointmentStatusId = APPSTATid;
                StatusHistory s = new StatusHistory { StatusId = APPSTATid, AppointmentId = app.Id, Details = "Doctor" };
                db.StatusHistories.Add(s);
                db.Entry(app).State = EntityState.Modified;
                db.SaveChanges();
                #endregion
              
                
                //check cancel before app date 
                var cancel_Id =   db.AppointmentStatus.Where(sa => sa.Name == "Cancelled").FirstOrDefault().Id;
                

                if (APPSTATid == cancel_Id && (app.DayofApp - DateTime.Today).TotalDays >= 0)

                {
                    // make the slot taken = false      
                    
                    var a = db.AvailableTimesLists.Find(app.Slot);
                    if (a != null)
                    {
                        a.Taken = false;

                        db.Entry(a).State = EntityState.Modified;
                        db.SaveChanges();

                        string S = app.TimeStart.GetDisplayName();
                        string Name = patientState.Patient.UserName;
                        string Email = patientState.Patient.Email;

                        BackgroundJob.Enqueue(() => BG_Methods.SendCancelEmailsAsync(S, Name, Email));


                    }
                     


                    return RedirectToAction("ClinicApps", new { id = app.ClinicId });
                }


                 #region other status 

                var Attended_Id = db.AppointmentStatus.Where(sa => sa.Name == "Attended").FirstOrDefault().Id;
                if (APPSTATid == Attended_Id)
                {
                    var pat = db.Users.Find(patientState.PatientId);
                    var no = db.Appointments.Where(c => c.PatientState.PatientId == pat.Id && c.AppointmentStatusId != cancel_Id).ToList().Count;
                    pat.Rate += Math.Round(1.0 / no, 2);
                    db.Entry(pat).State = EntityState.Modified;
                    db.SaveChanges();

                  
                    return RedirectToAction("ClinicApps", new { id = app.ClinicId });
                }
            
                
                var NAttended_Id = db.AppointmentStatus.Where(sa => sa.Name == "Not Attended").FirstOrDefault().Id;
                if (APPSTATid == NAttended_Id)
                {
                    var pat = db.Users.Find(patientState.PatientId);
                    var no = db.Appointments.Where(c => c.PatientState.PatientId == pat.Id && c.AppointmentStatusId != cancel_Id).ToList().Count;
                    pat.Rate -= Math.Round(1.0 / no, 2);

                    db.Entry(pat).State = EntityState.Modified;
                    db.SaveChanges();

                
                    return RedirectToAction("ClinicApps", new { id = app.ClinicId });

                }

                //cancel after 
                
                return RedirectToAction("ClinicApps", new { id = app.ClinicId });

                #endregion
                 
            }

            return View();
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
            if ((appointment.DayofApp - DateTime.Today).TotalDays < 1)
            {
                

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
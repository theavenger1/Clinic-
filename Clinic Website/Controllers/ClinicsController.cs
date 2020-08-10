using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clinic_Website.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using Clinic_Website.Migrations;

namespace Clinic_Website.Controllers
{
    [Authorize]   
    public class ClinicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admins")]
        public ActionResult Index()
        {
            //   var clinics = db.Clinics.Include(j => j.Category);
            var clinics = db.Clinics;
            return View(clinics.ToList());
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult YourClinics()
        {
            string currentUserId = User.Identity.GetUserId(); 


            var clinics  = from r in db.Clinics
                                      where r.userId == currentUserId
                                      orderby r.Id
                                      select r;
            return View(clinics);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = db.Clinics.Find(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }
        [Authorize(Roles = "Doctor")]   
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Clinic clinic,HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                clinic.ClinicImage= upload.FileName;
                clinic.userId = User.Identity.GetUserId();
            }
           
            if (ModelState.IsValid)
            {
                           
                db.Clinics.Add(clinic);
                db.SaveChanges();

               
                return RedirectToAction("YourClinics");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", clinic.CategoryId);
            return View(clinic);
        }

        //private void Maintain(Clinic c)
        //{
        //    //to do 

        //    //SElect specific timeslots based on app length now (Default 1 hour ) 

        //    // List<TimeSlots>  allAPP = new List<TimeSlots>();

        //    for (int i = (int)c.StartTime; i < (int)c.EndTime; i++)
        //    {
        //        //   allAPP.Add((TimeSlots)i);

        //        TimeSlotList timeSlotList = new TimeSlotList { ClinicId = c.Id, Slot_start = (TimeSlots)i, Length = c.AppointmentLength };
        //                    db.TimeSlotLists.Add(timeSlotList);



        //    }
        //    db.SaveChanges();
        //}

        private void ProduceAvaApp(List<DayList> dayList ,Clinic c)
        {
            foreach (var item in dayList)
            {  
                var model = from r in db.AvailableTimesLists
                            where r.DayListId == item.Id
                            select r;
              
                var modeltolist = model.ToList();
                foreach (var slot in modeltolist)
                {
                    db.AvailableTimesLists.Remove(slot);


                }
                 
                for (int i = (int)c.StartTime; i < (int)c.EndTime; i++)
                {
                    AvailableTimesList availableTimes = new AvailableTimesList { DayListId = item.Id, Taken = false, Slot_start = (TimeSlots)i };

                    db.AvailableTimesLists.Add(availableTimes);

                }

                //for 12 pm to 8:00 Am (if exists) 
                if (c.EndTime < c.StartTime)
                {
                    for (int i = (int)c.EndTime; i < (int)TimeSlots._23_00; i++)
                    {
                        AvailableTimesList availableTimes = new AvailableTimesList { DayListId = item.Id, Taken = false, Slot_start = (TimeSlots)i };

                        db.AvailableTimesLists.Add(availableTimes);

                    }

                    for (int i = (int)TimeSlots._00_00; i < (int)c.StartTime; i++)
                    {
                        AvailableTimesList availableTimes = new AvailableTimesList { DayListId =item.Id, Taken = false, Slot_start = (TimeSlots)i };

                        db.AvailableTimesLists.Add(availableTimes);

                    }

                }


                db.SaveChanges();
            } 
        
 
           
        }


        [Authorize(Roles = "Doctor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string currentUserId = User.Identity.GetUserId();



            var model = from r in db.Clinics
                        where r.userId == currentUserId && r.Id == id

                        select r;
       
            Clinic clinic = model.FirstOrDefault();

            if (clinic == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", clinic.CategoryId);
            return View(clinic);
        }



       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Clinic clinic, HttpPostedFileBase upload)
        {
            clinic.userId = User.Identity.GetUserId();
         
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Uploads"), clinic.ClinicImage);
                var checkdays = db.DayLists.Where(aa => aa.ClinicId == clinic.Id).ToList();
                if (checkdays != null) { ProduceAvaApp(checkdays, clinic); }

                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    clinic.ClinicImage = upload.FileName;
                }
                
                db.Entry(clinic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("YourClinics");
                
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", clinic.CategoryId);
            return View(clinic);
        }

        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Clinic clinic = db.Clinics.Find(id);
        //    if (clinic == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(clinic);
        //}


        [Authorize(Roles = "Doctor")]
        public ActionResult Delete(int? id)

        {
            if (id == null)
           return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = User.Identity.GetUserId();

            Clinic clinic = db.Clinics.Find(id);

            if (clinic.userId == user) { 
            db.Clinics.Remove(clinic);
            db.SaveChanges();

                return RedirectToAction("YourClinics");
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

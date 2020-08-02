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

               
                return RedirectToAction("YourclinicDaylist", "daylist");
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

        //private void ProduceAvaApp(Clinic c) {


        //    var avadays = c.DayLists.ToList();
        //    //var model = from r in db.DayLists
        //    //            where r.ClinicId == c.Id

        //    //            select r;
        //    //var x = model.ToList();

        //    var avatimesolts = c.TimeSlotLists.ToList();

        //    var model = from r in db.Appointments
        //                where r.ClinicId == c.Id

        //                select r;
        //    var appointments = model.ToList();

        //    var x = new List<TimeSlots>();

        // //   if (appointments == null) { return }
        //    foreach (var item in appointments)
        //    {
        //      //  if item.TimeStart!=avatimesolts


        //    }


        //}


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

                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    clinic.ClinicImage = upload.FileName;
                }
                
                db.Entry(clinic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
                
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

 
 
        public ActionResult Delete(int? id)

        {
            if (id == null)
           return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = User.Identity.GetUserId();

            Clinic clinic = db.Clinics.Find(id);

            if (clinic.userId == user) { 
            db.Clinics.Remove(clinic);
            db.SaveChanges();

                return RedirectToAction("Index");
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

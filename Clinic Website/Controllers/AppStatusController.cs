using Clinic_Website.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Clinic_Website.Controllers
{


    public class AppStatusController : Controller
    {
        private ApplicationDbContext db;

       public AppStatusController()
        {

            db = new ApplicationDbContext();

        }



        public ActionResult Index()
        {

            var sts = db.AppointmentStatus.ToList();

            return View(sts);
        }
    
    
     public ActionResult Create()
        {


            return View();
        }


         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentStatus appointmentStatus)
        {
            if (ModelState.IsValid)
            {
                db.AppointmentStatus.Add(appointmentStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

         
        public ActionResult Delete(int? id)

        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

           
            var appointmentStatus = db.AppointmentStatus.Find(id);

            if (appointmentStatus == null)
            {
                return HttpNotFound();
             
            }
            db.AppointmentStatus.Remove(appointmentStatus);
            db.SaveChanges();

            return RedirectToAction("Index");
             
        }




    }
}
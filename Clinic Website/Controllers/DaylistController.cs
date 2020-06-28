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
    { 
       
            private ApplicationDbContext db;

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

        public ActionResult YourclinicDaylist()
        {
            string currentUserId = User.Identity.GetUserId();

            var model = from r in db.DayLists
                        where r.Clinic.userId==currentUserId
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
        public ActionResult Create(DayList d , FormCollection f)
        {
            string clinicid = f["Clinics"].ToString();

            if (clinicid == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            if (ModelState.IsValid)
            {

               d.ClinicId = int.Parse(clinicid);
                db.DayLists.Add(d);        

                
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

        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var city = db.cities.Find(id);
        //    if (city == null)
        //    {
        //        return HttpNotFound();
        //    }


        //    ViewBag.Govs = new SelectList(db.govs, "Id", "gov_name");
        //    return View(city);


        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(city city, FormCollection f)
        //{

        //    string Govvalue = f["Govs"].ToString();

        //    if (ModelState.IsValid)
        //    {
        //        city.gov_id = int.Parse(Govvalue);

        //        var entry = db.Entry(city);
        //        entry.State = EntityState.Modified;

        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(city);

        //}

        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    var city = db.cities.Find(id);

        //    if (city == null)
        //    {
        //        return View("Not Found");
        //    }
        //    return View(city);

        //}
        //[HttpPost]

        //public ActionResult Delete(int id, FormCollection form)
        //{
        //    var city = db.cities.Find(id);
        //    db.cities.Remove(city);
        //    db.SaveChanges();


        //    return RedirectToAction("Index");

        //}


    }
}

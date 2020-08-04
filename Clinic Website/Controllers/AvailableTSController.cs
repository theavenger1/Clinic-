////using Clinic_Website.Models;
////using System;
////using System.Collections.Generic;
////using System.Data.Entity;
////using System.Linq;
////using System.Web;
////using System.Web.Mvc;

////namespace Clinic_Website.Controllers
////{
////    public class AvailableTSController : Controller
////    {
////         static ApplicationDbContext db = new ApplicationDbContext();
       
////        public ActionResult Index()
////        {
////            var model = db.AvailableTimesLists.ToList();


////            return View(model);
////        }
////      //  x.DayName.GetDisplayName() == DateTime.Now.AddDays(i).DayOfWeek.ToString()

////        public static void MakeTSAV()
////        {
////            Days st = Days.Saturday;
////            Days q = Days.Saturday;
////            for (int i = 1; i < 8; i++)
////            {
////                if (q.GetDisplayName() == DateTime.Now.AddDays(-2).DayOfWeek.ToString()) { st = q; }
////                q = (Days)i;
////            }

////            //  int x = (int)st;

////            //         var days = db.AvailableTimesLists.TakeWhile(x => x.DayList.DayName == st); 
////            //var days = db.DayLists.Where(o => o.DayName.ToString() == DateTime.Now.AddDays(-2).DayOfWeek.ToString());
////            var days = from r in db.DayLists
////                       where r.DayName == st
////                       select r;



////            foreach (var item in days)
////            {
////                var AV_TL_D = from av in db.AvailableTimesLists
////                        where av.DayListId == item.Id
////                        select av;
 
                

////                foreach (var w in AV_TL_D)
////                {
////                    w.Taken = false;
////                    db.Entry(w).State = EntityState.Modified;

////                    db.SaveChanges();
////                }

            
////            }

////        }

////    }
////}
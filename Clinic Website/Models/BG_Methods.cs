﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Clinic_Website.Models
{
    public  static class BG_Methods
    {
        
        public static void MakeTSAV()
        {
            ApplicationDbContext db = new ApplicationDbContext();


            Days st = Days.Saturday;
            Days q = Days.Saturday;
            for (int i = 1; i < 8; i++)
            {
                if (q.GetDisplayName() == DateTime.Now.AddDays(-2).DayOfWeek.ToString()) { st = q; }
                q = (Days)i;
            }

            //  int x = (int)st;

            //         var days = db.AvailableTimesLists.TakeWhile(x => x.DayList.DayName == st); 
            //var days = db.DayLists.Where(o => o.DayName.ToString() == DateTime.Now.AddDays(-2).DayOfWeek.ToString());
            var days = from r in db.DayLists
                       where r.DayName == st
                       select r;



            foreach (var item in days)
            {
                var AV_TL_D = from av in db.AvailableTimesLists
                              where av.DayListId == item.Id
                              select av;



                foreach (var w in AV_TL_D)
                {
                    w.Taken = false;
                    db.Entry(w).State = EntityState.Modified;

                    db.SaveChanges();
                }


            }

        }
    }
}
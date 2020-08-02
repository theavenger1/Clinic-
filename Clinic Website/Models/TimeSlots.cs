using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Clinic_Website.Models
{
    public enum TimeSlots
    {


        [Display(Name = "00:00")]
          _00_00 = 0,
       
        [Display(Name = "01:00")]
        _01_00 = 1,

        [Display(Name = "02:00")]
        _02_00 = 2,
        [Display(Name = "03:00")]
        _03_00 = 3,
        [Display(Name = "04:00")]
        _04_00 = 4,
        [Display(Name = "05:00")]
        _05_00 = 5,
        [Display(Name = "06:00")]
        _06_00 = 6,
        [Display(Name = "07:00")]
        _07_00 = 7,
        [Display(Name = "08:00")]
        _08_00 = 8,
        [Display(Name = "09:00")]
        _09_00 = 9,
        [Display(Name = "10:00")]
        _10_00 = 10,
        [Display(Name = "11:00")]
        _11_00 = 11,
        [Display(Name = "12:00")]
        _12_00 = 12,
        [Display(Name = "13:00")]
        _13_00 = 13,
        [Display(Name = "14:00")]
        _14_00 = 14,
        [Display(Name = "15:00")]
        _15_00 = 15,
        [Display(Name = "16:00")]
        _16_00 = 16,
        [Display(Name = "17:00")]
        _17_00 = 17,
        [Display(Name = "18:00")]
        _18_00 = 18,
        [Display(Name = "19:00")]
        _19_00 = 19,
        [Display(Name = "20:00")]
        _20_00 = 20,
        [Display(Name = "21:00")]
        _21_00 = 21,
        [Display(Name = "22:00")]
        _22_00 = 22,
        [Display(Name = "23:00")]
        _23_00 = 23,



    }
}
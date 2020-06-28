using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Clinic_Website.Models
{
    public enum Days
    {
        [Display(Name ="Saturday")]
        
        saturdasssss = 1,

        [Display(Name = "Sunday")]
        Sunday,

        [Display(Name = "Monday")]
        Monday,

        [Display(Name = "Tuesday")]
        Tuesday,

        [Display(Name = "Wednesday")]
        Wednesday,

        [Display(Name = "Thursday")]
        Thursday,

        [Display(Name = "Friday")]
        Friday,

    }

    
}
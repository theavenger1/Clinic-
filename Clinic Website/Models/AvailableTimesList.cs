using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Web;

namespace Clinic_Website.Models
{
    public class AvailableTimesList
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [ForeignKey("DayList")]
        public int DayListId { get; set; }
        public virtual DayList DayList{ get; set; }

        [Display(Name = "Appointment Start")]
        public TimeSlots Slot_start { get; set; }

    }
}
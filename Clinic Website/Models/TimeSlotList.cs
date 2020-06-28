using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Clinic_Website.Models
{
    public class TimeSlotList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [ForeignKey("Clinic")]
        public int ClinicId { get; set; }
        public virtual Clinic Clinic { get; set; }

        [Display(Name ="Appointment Start")]
        public TimeSlots Slot_start  { get; set; }

        public int Length { get; set; }

    }
}
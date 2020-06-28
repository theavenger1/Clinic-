using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Clinic_Website.Models
{
    public class StatusHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
      
      
        public int? AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }
      
        
       
        public int? StatusId { get; set; }
      public AppointmentStatus Status { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Date Created")]
        public DateTime Date_Created { get; set; }
        public string Details { get; set; }


    }
}
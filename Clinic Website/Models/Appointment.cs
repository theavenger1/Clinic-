using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Clinic_Website.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        

        //Entered by doctor 
        public String Prescription { get; set; }


        [Display(Name = "Day of Appointment")]
        public DateTime DayofApp { get; set; }

        [Display(Name = "Time of Appointment")]
        public TimeSlots TimeStart { get; set; }

        // entered at creation 

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name ="Date Created")]
        public DateTime Date_Created { get; set; }


        //relationships 



        //1  many to one 
        [ForeignKey("Clinic")]
        public int ClinicId { get; set; }
        public virtual Clinic Clinic { get; set; }

        //2 many to one 

        [ForeignKey("PatientState")]
        public int PatientStateId { get; set; }
        public virtual PatientState PatientState { get; set; }


        //3 many to one 
        public int? AppointmentStatusId { get; set; }
        public virtual AppointmentStatus AppointmentStatus { get; set; }

        //4 one to many 
        public virtual ICollection<StatusHistory> StatusHistoriess { get; set; }
      
      
    }
}
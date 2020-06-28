using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Clinic_Website.Models
{
    public class PatientState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string StateName { get; set; }

     
        public string PatientId { get; set; }
        public ApplicationUser Patient { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Date Created")]
        public DateTime Date_Created { get; set; }


        //when end date is assigned
        public bool InProgress { get; set; }
      
        //from date created for first appoint.
        public DateTime StartTime { get; set; }
        
        //from date created from last appoint.
        public DateTime? EndTime { get; set; }
   
        public virtual ICollection<Appointment> Appointments { get; set; }




    }
}
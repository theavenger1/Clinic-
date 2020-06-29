using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic_Website.Models
{
    public class Clinic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
      
        
        [DisplayName("Clinic Name")]
        public string ClinicName { get; set; }
       
        
        [DisplayName("Clinic Description")]
        [AllowHtml]//to allow string to be a html code
        public string ClinicDescription { get; set; }
       
        
        [DisplayName("Clinic Image")]
        
        public string ClinicImage { get; set; }

     
        
        [Required(ErrorMessage = "You must enter Length")]
        [DisplayName("Appointment length")]
        public int AppointmentLength { get; set; }


        [Required(ErrorMessage = "You must enter Address")]
        public string Address { get; set; }

        
        [Required(ErrorMessage = "You must enter Start Time")]
        public virtual TimeSlots StartTime { get; set; }
        [Required(ErrorMessage = "You must enter   End Time")]
   
        public virtual TimeSlots EndTime { get; set; }
        [Required(ErrorMessage = "You must enter Mobile No")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "You must enter Price")]
        public int Price { get; set; }

       
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        //This is one to many relationship
        [DisplayName("Clinic Category")]
        [ForeignKey("Category")]
        [Required(ErrorMessage = "You must choose a Category")]
        public int CategoryId { get; set; }
        //the Doctor id

        public string userId { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ICollection<DayList> DayLists { get; set; }

        public virtual ICollection<TimeSlotList> TimeSlotLists { get; set; }

    

        public virtual Category Category { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}
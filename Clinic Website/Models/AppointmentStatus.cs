using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Clinic_Website.Models
{
    public class AppointmentStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Name { get; set; }

        // two relationships (one to many )
        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ICollection<StatusHistory> StatusHistories { get; set; }

    }
}
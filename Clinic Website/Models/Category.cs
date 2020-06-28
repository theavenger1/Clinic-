using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Clinic_Website.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DisplayName("Clinic Category")]
        public string CategoryName { get; set; }
        [Required]
        [DisplayName("Category Description")] 
        public string CategoryDescription { get; set; }
        public virtual ICollection<Clinic> Clinics { get; set; }
    }
}
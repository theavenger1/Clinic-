using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Clinic_Website.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Clinic_Website.Models
{
    public class ApplicationUser : IdentityUser
    {
      
        public double Rate { get; set; }
        // public string UserType { get; set; }
        public virtual ICollection<Clinic> Clinics { get; set; }
        public virtual ICollection<PatientState> PatientStates { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Clinic_Website.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Clinic_Website.Models.Clinic> Clinics { get; set; }

        public System.Data.Entity.DbSet<Clinic_Website.Models.Appointment> Appointments { get; set; }

        public virtual DbSet<DayList> DayLists { get; set; }

      //  public virtual DbSet<TimeSlotList> TimeSlotLists { get; set; }
        public virtual DbSet<AvailableTimesList> AvailableTimesLists { get; set; }

        public virtual DbSet<StatusHistory> StatusHistories { get; set; }



        //public System.Data.Entity.DbSet<Clinic_Website.Models.ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //for identity user intialization 
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StatusHistory>()
                       .HasOptional(m => m.Appointment)
                       .WithMany(t => t.StatusHistoriess)
                       .HasForeignKey(m => m.AppointmentId)
                       .WillCascadeOnDelete(false);
            modelBuilder.Entity<StatusHistory>()
                     .HasOptional(m => m.Status)
                     .WithMany(t => t.StatusHistories)
                     .HasForeignKey(m => m.StatusId)
                     .WillCascadeOnDelete(false);

            modelBuilder.Entity<PatientState>()
                        .HasRequired(m => m.Patient)
                        .WithMany(t => t.PatientStates)
                        .HasForeignKey(m => m.PatientId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clinic>()
                      .HasRequired(m => m.user)
                      .WithMany(t => t.Clinics)
                      .HasForeignKey(m => m.userId)
                      .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<Clinic_Website.Models.AppointmentStatus> AppointmentStatus { get; set; }

        public System.Data.Entity.DbSet<Clinic_Website.Models.PatientState> PatientStates { get; set; }
    }
}
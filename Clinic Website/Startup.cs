using Clinic_Website.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Clinic_Website.Controllers;
using Hangfire;
using Hangfire.SqlServer;
using System.Collections.Generic;
using System;

using System.Diagnostics;

[assembly: OwinStartupAttribute(typeof(Clinic_Website.Startup))]
namespace Clinic_Website
{
    public partial class Startup
    {
        private IEnumerable<IDisposable> GetHangfireServers()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("HangfireTest", new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                });

            yield return new BackgroundJobServer();
        }




        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();

            app.UseHangfireAspNet(GetHangfireServers);
            app.UseHangfireDashboard();
            //  SendEmailController e1 = new SendEmailController();

            //  BackgroundJob.Enqueue(() => Debug.WriteLine("Hello world from Hangfire!"));
            //BackgroundJob.Enqueue(() => BG_Methods.MakeTSAV());
            // RecurringJob.AddOrUpdate( () =>  e1.SendEmail(), Cron.Minutely);
            RecurringJob.AddOrUpdate( () => BG_Methods.SendEmailsAsync(), Cron.Daily);  
            RecurringJob.AddOrUpdate( () => BG_Methods.MakeTSAV(), Cron.Daily);
        
        }

        // roles method
        public void CreateDefaultRolesAndUsers()
        {
            var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleManger.RoleExists("Admins"))
            {
                if (!roleManger.RoleExists("Assisstant"))
                {
                    role.Name = "Assisstant";
                }

                if (!roleManger.RoleExists("Doctor"))
                {
                    role.Name = "Doctor";
                }
                if (!roleManger.RoleExists("Patient"))
                {
                    role.Name = "Patient";
                    roleManger.Create(role);
                }

                role.Name = "Admins";
                roleManger.Create(role);

                ApplicationUser user = new ApplicationUser();
                user.UserName = "Ahmed";
                user.Email = "ahmedalshora53@gmail.com";
                var Check = userManger.Create(user, "Aaaa.12345");
                if (Check.Succeeded)
                {
                    userManger.AddToRoles(user.Id, "Admins");
                }
            }

        }
    }
}

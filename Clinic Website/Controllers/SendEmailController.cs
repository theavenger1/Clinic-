using Hangfire;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Clinic_Website.Controllers
{
    public class SendEmailController : Controller
    {
  
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> SendEmail(string S ,string Name,string Email, string n)
        {
            if (n == "1")
            {            //Email
                var message = EMailTemplate("App_Rem");
                message = message.Replace("PAT", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Name));
                message = message.Replace("SLOT", S);
                await SendEmailAsync(Email, "Appoinment Reminder", message);
                //End Email
            
            }
            if (n == "2")
            {
                var message = EMailTemplate("App_cancel");
                message = message.Replace("PAT", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Name));
                message = message.Replace("SLOT", S);
                await SendEmailAsync(Email, "Appoinment Reminder", message);
                //End Email
                


            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        public string EMailTemplate(string template)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Views/templetes/") + template + ".cshtml");
            return body.ToString();
        }


        public async static Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {

                var _email = "ctmzg.2020@gmail.com";
                var _epass = ConfigurationManager.AppSettings["EmailPassword"];
                var _dispName = "Test Mail";

                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(email);
                myMessage.From = new MailAddress(_email, _dispName);
                myMessage.Subject = subject;
                myMessage.Body = message;
                myMessage.IsBodyHtml = true;


                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _epass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(myMessage);
                }


            }
            catch (Exception)
            {

                throw;
            }

        }



    }
}
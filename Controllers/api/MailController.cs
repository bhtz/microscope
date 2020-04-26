using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Text;
using System;
using System.Net;

namespace IronHasura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MailController : ControllerBase
    {
        private readonly string _host;
        private readonly string _from;
        private readonly string _alias;
        private readonly string _mailUser;
        private readonly string _mailPwd;

        public MailController(IConfiguration configuration)
        {
            var smtpSection = configuration.GetSection("SMTP");
            if (smtpSection != null)
            {
                _host = smtpSection.GetSection("Host").Value;
                _from = smtpSection.GetSection("From").Value;
                _alias = smtpSection.GetSection("Alias").Value;
                _mailUser = smtpSection.GetSection("Username").Value;
                _mailPwd = smtpSection.GetSection("Password").Value;
            }
        }

        [HttpPost]
        public ActionResult SendMail()
        {
            using (SmtpClient client = new SmtpClient(_host))
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_from, _alias);
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.To.Add("benjamin.heintz@soprasterianext.com");
                mailMessage.Body = "Test microscope baas mailer";
                mailMessage.Subject = "Test mailer";
                mailMessage.IsBodyHtml = false;
                //mailMessage.Attachments.Add(new Attachment(Server.MapPath("~/myimage.jpg")));

                client.Credentials = new NetworkCredential(_mailUser, _mailPwd);
                client.EnableSsl = true;
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                client.Send(mailMessage);
            }

            return Ok();
        }
    }
}

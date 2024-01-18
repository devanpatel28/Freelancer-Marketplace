using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Configuration;
using System.Threading.Tasks;

namespace Freelancer_Marketplace.Models
{
    public class EmailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public async Task SendMailAsync()
        {
            try
            {
                using (MailMessage mc = new MailMessage(ConfigurationManager.AppSettings["Email"], To))
                {
                    mc.Subject = Subject;
                    mc.Body = Body;
                    mc.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Timeout = 1000000;
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                        NetworkCredential nc = new NetworkCredential("vedantbharad26@gmail.com", "dqfe bttn czuu xgwk");
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = nc;

                        await smtp.SendMailAsync(mc);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }

}
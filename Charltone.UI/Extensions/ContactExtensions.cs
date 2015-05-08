using Charltone.UI.Models;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace Charltone.UI.Extensions
{
    public static class ContactExtensions
    {
        public static string SendEmail(this Contact contact, string message)
        {
            try
            {
                var emailBody = new StringBuilder();

                emailBody.Append("<b>The following message was received from Charltone.com:</b>");
                emailBody.Append("<br>");
                emailBody.Append("<br>");
                emailBody.Append("From: " + contact.Name);
                emailBody.Append("<br>");
                emailBody.Append("Phone: " + contact.Phone);
                emailBody.Append("<br>");
                emailBody.Append("Email: " + contact.Email);
                emailBody.Append("<br>");
                emailBody.Append("<br>");
                emailBody.Append(message);

                var eMail = new MailMessage
                            {
                                IsBodyHtml = true,
                                Body = emailBody.ToString(),
                                From = new MailAddress(ConfigurationManager.AppSettings["AdminEmailAddress"]),
                                Subject = ConfigurationManager.AppSettings["ContactEmailSubject"]
                            };

                eMail.To.Add(ConfigurationManager.AppSettings["ContactEmailToAddress"]);

                using (var client = new SmtpClient())
                {
                    client.Credentials = new System.Net.NetworkCredential(
                        ConfigurationManager.AppSettings["SmtpCredentialsUserName"],
                        ConfigurationManager.AppSettings["SmtpCredentialsPassword"]);

                    client.Host = ConfigurationManager.AppSettings["SmtpHost"];
                    client.Send(eMail);
                }
                return null;
            }
            catch(SmtpException ex)
            {
                return ex.InnerException.Message;
            }
        }
    }
}
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace Charltone.UI.Library
{
    public static class Email
    {
        public static void Send(string name, string phone, string email, string message)
        {
            var emailBody = new StringBuilder();

            emailBody.Append("<b>The following message was received from Charltone.com:</b>");
            emailBody.Append("<br>");
            emailBody.Append("<br>");
            emailBody.Append("From: " + name);
            emailBody.Append("<br>");
            emailBody.Append("Phone: " + phone);
            emailBody.Append("<br>");
            emailBody.Append("Email: " + email);
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
        }
    }
}
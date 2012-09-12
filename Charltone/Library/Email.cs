using System.Net.Mail;
using Charltone.Domain;

namespace Charltone.Library
{
    public class Email
    {
        public void Send(Contact contact)
        {
            var mail = new MailMessage(
                contact.From,
                "john_charlton@sympatico.ca",
                //ConfigurationManager.AppSettings[Keys.ContactToEmail],
                contact.Subject,
                contact.Message);

            var smtp = new SmtpClient {Host = "127.0.0.1"};
            smtp.Send(mail);
        }
    }
}
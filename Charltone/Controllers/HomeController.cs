using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using Charltone.Domain;
using Charltone.ViewModels.Home;
using NHibernate;

namespace Charltone.Controllers
{
	public class HomeController : Controller
	{
		private readonly ISession _session;

		public HomeController(ISession session)
		{
            _session = session;
		}

		public ActionResult Index()
		{
            ViewBag.Message = "Welcome to Charltone Instruments";
            ViewBag.EventCount = _session.QueryOver<Instrument>().RowCount();

			return View();
		}

		public ActionResult About()
		{
			return View();
		}

        public ActionResult Contact()
        {
            return View(new ContactViewModel());
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel viewModel)
        {
            var contactName = viewModel.ContactName ?? "Not supplied";
            var contactPhone = viewModel.ContactPhone ?? "Not supplied";
            var contactEmail = viewModel.ContactEmail ?? "Not supplied";
            var emailBody = new StringBuilder();
            
            emailBody.Append("<b>The following message was received from Charltone.com:</b>");
            emailBody.Append("<br>");
            emailBody.Append("<br>");
            emailBody.Append("From: " + contactName);
            emailBody.Append("<br>");
            emailBody.Append("Phone: " + contactPhone);
            emailBody.Append("<br>");
            emailBody.Append("Email: " + contactEmail);
            emailBody.Append("<br>");
            emailBody.Append("<br>");
            emailBody.Append(viewModel.ContactMessage);

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

            viewModel.HeaderMessage = "We received your message.  Thank you.";
            return View(viewModel);
        }

        public ActionResult Events()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }
	}
}
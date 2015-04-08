using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Charltone.Data.Repositories;
using Charltone.Domain.Entities;
using Charltone.UI.ViewModels.Home;
using NHibernate;

namespace Charltone.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ISession _session;
        private readonly IPhotoRepository _photoRepository;

        public HomeController(ISession session, IPhotoRepository photoRepository)
		{
            _session = session;
            _photoRepository = photoRepository;
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

        [Authorize]
        public ActionResult Edit()
        {
            var image = _photoRepository.GetHomePageImage();
            return View(LoadHomePageImageEditViewModel(image));
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateHomePageImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var b = new BinaryReader(file.InputStream);
                    var data = b.ReadBytes(file.ContentLength);

                    _photoRepository.UpdateHomePageImage(data);
                }
            }
            var vm = new HomePageImageEditViewModel();
            return View("Edit",  vm);
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

        [HttpGet]
        public JsonResult GetHomePageImageJson()
        {
            var image = _photoRepository.GetHomePageImage();
            if (image == null) return null;

            var vm = new HomeViewModel<HomePageImageData>();

            var data = Convert.ToBase64String(image.Data);
            vm.HomePageImage = data;

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPhotoJson(int id)
        {
            var photo = _photoRepository.GetData(id);
            var data = Convert.ToBase64String(photo);

            return Json(data);
        }

        private static HomePageImageEditViewModel LoadHomePageImageEditViewModel(HomePageImage homePageImage)
        {
            var vm = new HomePageImageEditViewModel { HomePageImage = homePageImage };

            return vm;
        }
	}
}
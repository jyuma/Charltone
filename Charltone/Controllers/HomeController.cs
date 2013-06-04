using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Charltone.Domain;
using Charltone.Repositories;
using Charltone.ViewModels.Home;
using NHibernate;

namespace Charltone.Controllers
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

        [Authorize]
        [HttpPost]
        public ActionResult Edit(FormCollection collection, string actionType)
        {
            var images = _photoRepository.GetHomePageImageList();

            Delete(images, collection);

            return View(LoadHomePageImageEditViewModel(images));
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
            var images = _photoRepository.GetHomePageImageList();
            return View(LoadHomePageImageEditViewModel(images));
        }

        //[Authorize]
        //public ActionResult Edit(int id)
        //{
        //    var vm = new HomePageImageEditSingleViewModel(id);
        //    return View(vm);
        //}

        [Authorize]
        [HttpPost]
        public ActionResult AddHomePageImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var b = new BinaryReader(file.InputStream);
                    var f = b.ReadBytes(file.ContentLength);

                    _photoRepository.AddHomePageImage(f);
                }
            }

            return View("Edit", LoadHomePageImageEditViewModel(_photoRepository.GetHomePageImageList()));
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateHomePageImage(int id, HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var b = new BinaryReader(file.InputStream);
                    var f = b.ReadBytes(file.ContentLength);

                    _photoRepository.UpdateHomePageImage(id, f, 1);
                }
            }
            var vm = new HomePageImageEditSingleViewModel(id);
            return View("Edit",  vm);
        }

        private void Delete(ICollection<HomePageImage> images, FormCollection collection)
        {
            HomePageImage image = null;

            foreach (var i in images.Where(x => collection.GetValue("Delete_" + x.Id) != null))
            {
                _photoRepository.DeleteHomePageImage(i.Id);
                image = i;
            }

            if (image == null) return;
            images.Remove(image);
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

        //private HomeViewModel<HomePageImageData> LoadHomeViewModel(IEnumerable<HomePageImage> homePageImages)
        //{
        //    var vm = new HomeViewModel<HomePageImageData>();

        //    foreach (var image in homePageImages)
        //    {
        //        var imgData = new HomePageImageData(image.Id, image.Data);
        //        vm.HomePageImageList.Add(imgData);
        //    }
        //    return vm;
        //}

        private HomePageImageEditViewModel<HomePageImages> LoadHomePageImageEditViewModel(IEnumerable<HomePageImage> homePageImages)
        {
            var vm = new HomePageImageEditViewModel<HomePageImages>();

            foreach (var image in homePageImages)
            {
                vm.HomePageImages.Add(new HomePageImages(image.Id, image.Data, image.SortOrder));
            }

            return vm;
        }

        [HttpPost]
        public JsonResult GetHomePageImageListJson(int next, int last)
        {
            var images = _photoRepository.GetHomePageImageList();
            var vm = new HomeViewModel<HomePageImageData>();
            var total = 10; // images.Count;
            var counter = 1;
            var index = 1;

            vm.ImageCount = total;

            foreach (var i in images)
            {
                index++;
                if ((i.SortOrder >= last) && (i.SortOrder <= (next + last)))
                {
                    var data = Convert.ToBase64String(i.Data);
                    vm.HomePageImageList.Add(new HomePageImageData(i.Id, data));
                    counter++;
                }
                if (counter > next) break; 
            }
            if (index >= total) vm.Complete = 1;
            return Json(vm);
        }

        [HttpPost]
        public JsonResult GetPhotoJson(int id)
        {
            var photo = _photoRepository.GetData(id);
            var data = Convert.ToBase64String(photo);

            return Json(data);
        }
	}
}
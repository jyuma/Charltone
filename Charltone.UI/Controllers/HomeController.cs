using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Charltone.Data.Repositories;
using Charltone.Domain.Entities;
using Charltone.UI.Extensions;
using Charltone.UI.ViewModels.Home;
using NHibernate;

namespace Charltone.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ISession _session;
        private readonly IPhotoRepository _photos;

        public HomeController(ISession session, IPhotoRepository photos)
		{
            _session = session;
            _photos = photos;
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

        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            return View(LoadHomePageImageEditViewModel());
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateHomePageImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var reader = new BinaryReader(file.InputStream);
                    var data = reader.ReadBytes(file.ContentLength)
                        .ByteArrayToImage()
                        .CropHomePageImage()
                        .ImageToByteArray();

                    _photos.UpdateHomePageImage(data);
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
            var contactMessage = viewModel.ContactMessage;

            Library.Email.Send(contactName, contactPhone, contactEmail, contactMessage);

            viewModel.HeaderMessage = "We received your message.  Thank you.";
            return View(viewModel);
        }

        [HttpGet]
        public JsonResult GetHomePageImageJson()
        {
            var image = _photos.GetHomePageImage();
            var vm = new HomePageImageViewModel();

            var data = Convert.ToBase64String(image);
            vm.Data = data;

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult GetHomePageImage()
        {
            var photo = _photos.GetHomePageImage();

            return File(photo, "image/jpeg");
        }

        private HomePageImageEditViewModel LoadHomePageImageEditViewModel()
        {
            var image = _photos.GetHomePageImage();
            var vm = new HomePageImageEditViewModel { HomePageImage = image };

            return vm;
        }
	}
}
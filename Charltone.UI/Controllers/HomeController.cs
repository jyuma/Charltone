using Charltone.Data.Repositories;
using Charltone.UI.Extensions;
using Charltone.UI.ViewModels.Home;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Charltone.UI.Controllers
{
	public class HomeController : Controller
	{
        private readonly IHomeContentRepository _content;

        public HomeController(IHomeContentRepository content)
		{
            _content = content;
		}

		public ActionResult Index()
		{
            return View(LoadHomeViewModel());
		}

        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            return View(LoadHomePageEditViewModel());
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(HomeEditViewModel viewModel)
        {
            UpdateHomeContent(viewModel);

            return RedirectToAction("Index", LoadHomeViewModel());
        }

        [HttpGet]
        [Authorize]
        public ActionResult Photo()
        {
            return View(LoadHomeImageViewModel());
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

                    var content = _content.GetAll().Single();
                    content.Photo = data;

                    _content.Update(content);
                }
            }

            var vm = new HomeViewModel();

            return View("Photo",  vm);
        }

        [HttpGet]
        public JsonResult GetHomePageImageJson()
        {
            var image = _content.GetAll().Single().Photo;
            var vm = new HomeImageViewModel();

            var data = Convert.ToBase64String(image);
            vm.Data = data;

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult GetHomePageImage()
        {
            var image = _content.GetAll().Single().Photo;

            return File(image, "image/jpeg");
        }

        private HomeViewModel LoadHomeImageViewModel()
        {
            var image = _content.GetAll().Single().Photo;

            var vm = new HomeViewModel { Photo = image };

            return vm;
        }

        private HomeViewModel LoadHomeViewModel()
        {
            var content = _content.GetAll().Single();

            var vm = new HomeViewModel { Introduction = content.Introduction, Greeting = content.Greeting};

            return vm;
        }

        private HomeEditViewModel LoadHomePageEditViewModel()
        {
            var content = _content.GetAll().Single();

            var vm = new HomeEditViewModel { Greeting = content.Greeting, Introduction = content.Introduction };

            return vm;
        }

        private void UpdateHomeContent(HomeEditViewModel viewModel)
        {
            var content = _content.GetAll().Single();

            content.Introduction = viewModel.Introduction;
            content.Greeting = viewModel.Greeting;

            _content.Update(content);
        }
	}
}
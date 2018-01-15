using Charltone.Data.Repositories;
using Charltone.UI.ViewModels.Home;
using System;
using System.IO;
using System.Linq;
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

        [HttpPost]
        [Authorize]
        public ActionResult Upload()
        {
            var file = Request.Files[0];
            if (file == null) return Json(new { success = false });

            var reader = new BinaryReader(file.InputStream);
            var data = reader.ReadBytes(file.ContentLength);

            var content = _content.GetAll().FirstOrDefault();
            if (content == null) return Json(new { success = false });

            content.Photo = data;
            _content.Update(content);

            return Json(new { success = true });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            return View(LoadHomeEditViewModel());
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(HomeEditViewModel viewModel)
        {
            UpdateHomeContent(viewModel);

            return RedirectToAction("Index", LoadHomeEditViewModel());
        }

        private HomeViewModel LoadHomeViewModel()
        {
            var content = _content.GetAll().Single();

            var vm = new HomeViewModel
            {
                Introduction = content.Introduction, 
                Greeting = content.Greeting,
                MaxImageWidth = Constants.HomePageImageSize.Width,
                MaxImageHeight = Constants.HomePageImageSize.Height,
                Photo = "data:image/jpg;base64," +  Convert.ToBase64String(content.Photo)
            };

            return vm;
        }

        private HomeEditViewModel LoadHomeEditViewModel()
        {
            var content = _content.GetAll().Single();

            var vm = new HomeEditViewModel { Introduction = content.Introduction, Greeting = content.Greeting };

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
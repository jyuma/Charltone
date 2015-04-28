using Charltone.Data.Repositories;
using Charltone.UI.Extensions;
using Charltone.UI.ViewModels.Home;
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
            var reader = new BinaryReader(file.InputStream);
            var data = reader.ReadBytes(file.ContentLength);
            var path = Server.MapPath("~/Content/images/homepage.jpg");

            data.SaveHomePageImage(path);

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
                         IsAuthenticated = Request.IsAuthenticated,
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
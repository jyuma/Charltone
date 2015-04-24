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
        public ActionResult Index(HomeViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(LoadHomeViewModel());
            if (viewModel == null || viewModel.File == null || viewModel.File.ContentLength <= 0) return View(LoadHomeViewModel());

            var reader = new BinaryReader(viewModel.File.InputStream);

            var data = reader.ReadBytes(viewModel.File.ContentLength);
            var path = Server.MapPath("~/Content/images/homepage.jpg");

            data.Save(path);

            return View(LoadHomeViewModel());
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
using System.Linq;
using System.Web.Mvc;
using Charltone.Data.Repositories;
using Charltone.UI.ViewModels.About;

namespace Charltone.UI.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutContentRepository _aboutContent;

        public AboutController(IAboutContentRepository aboutContent)
        {
            _aboutContent = aboutContent;
		}

        public ActionResult Index()
        {
            return View(LoadAboutViewModel());
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            return View(LoadAboutViewModel());
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(AboutViewModel viewModel)
        {
            UpdateAboutContent(viewModel);

            return View("Index", LoadAboutViewModel());
        }

        private AboutViewModel LoadAboutViewModel()
        {
            var content = _aboutContent.GetAll().Single();
            var vm = new AboutViewModel
            {
                Name = content.Name,
                CompanyName = content.CompanyName,
                Origins = content.Origins,
                Materials = content.Materials
            };

            return vm;
        }

        private void UpdateAboutContent(AboutViewModel viewModel)
        {
            var aboutContent = _aboutContent.GetAll().Single();

            aboutContent.Name = viewModel.Name;
            aboutContent.CompanyName = viewModel.CompanyName;
            aboutContent.Origins = viewModel.Origins;
            aboutContent.Materials = viewModel.Materials;

            _aboutContent.Update(aboutContent);
        }
    }
}
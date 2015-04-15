using System.Web.Mvc;
using Charltone.UI.ViewModels.Contact;

namespace Charltone.UI.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            return View(new ContactViewModel());
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
    }
}
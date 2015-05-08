using Charltone.UI.Extensions;
using Charltone.UI.Models;
using Charltone.UI.ViewModels.Contact;
using System.Web.Mvc;

namespace Charltone.UI.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            return View(new ContactViewModel());
        }

        [HttpPost]
        public ActionResult Index(ContactViewModel viewModel)
        {
            var contactName = viewModel.ContactName ?? "Not supplied";
            var contactPhone = viewModel.ContactPhone ?? "Not supplied";
            var contactEmail = viewModel.ContactEmail ?? "Not supplied";
            var contactMessage = viewModel.ContactMessage;

            var contact = new Contact
                          {
                              Name = contactName,
                              Phone = contactPhone,
                              Email = contactEmail
                          };
            var result = contact.SendEmail(contactMessage);

            if (result != null) return Json(new {success = false, message = result});

            return Json(new { success = true });
        }
    }
}
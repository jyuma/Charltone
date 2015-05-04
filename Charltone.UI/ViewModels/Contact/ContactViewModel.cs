using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Charltone.UI.ViewModels.Contact
{
    public class ContactViewModel
    {
        public string HeaderMessage { get; set; }

        public ContactViewModel()
        {
            HeaderMessage = "Want to drop us a line?";
        }

        [DisplayName("Name")]
        public string ContactName { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string ContactEmail { get; set; }

        [DisplayName("Phone")]
        public string ContactPhone { get; set; }

        [DisplayName("Message")]
        public string ContactMessage { get; set; }

        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
    }
}

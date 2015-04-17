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

        [Required(ErrorMessage = "Name is required")]
        [DisplayName("Name")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        [DisplayName("Email")]
        public string ContactEmail { get; set; }

        [DisplayName("Phone")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [DisplayName("Message")]
        public string ContactMessage { get; set; }

        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
    }
}

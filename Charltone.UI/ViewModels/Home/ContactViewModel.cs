using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Charltone.UI.ViewModels.Home
{
    public class ContactViewModel
    {
        public ContactViewModel()
        {
            HeaderMessage = "Want to drop us a line?";
        }

        [Required]
        [DisplayName("Name")]
        public string ContactName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string ContactEmail { get; set; }

        [DisplayName("Phone")]
        public string ContactPhone { get; set; }

        [Required]
        [DisplayName("Message")]
        public string ContactMessage { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }

        public string HeaderMessage { get; set; }
    }
}

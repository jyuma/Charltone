using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Charltone.UI.ViewModels.Home
{
    public class HomeEditViewModel
    {
        [Required(ErrorMessage = "Introduction is required")]
        [StringLength(500, ErrorMessage = "Introduction cannot exceed 500 characters")]
        [DisplayName("Introduction")]
        public string Introduction { get; set; }

        [Required(ErrorMessage = "Greeting Message is required")]
        [StringLength(1000, ErrorMessage = "Greeting Message cannot exceed 1000 characters")]
        [DisplayName("Greeting Message")]
        public string Greeting { get; set; }
    }
}
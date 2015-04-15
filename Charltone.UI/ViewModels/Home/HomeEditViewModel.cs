using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Charltone.UI.ViewModels.Home
{
    public class HomeEditViewModel
    {
        [Required]
        [StringLength(500, ErrorMessage = "Introduction cannot exceed 500 characters")]
        [DisplayName("Introduction")]
        public string Introduction { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Greeting cannot exceed 1000 characters")]
        [DisplayName("Greeting")]
        public string Greeting { get; set; }
    }
}
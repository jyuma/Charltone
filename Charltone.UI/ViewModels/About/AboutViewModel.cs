using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Charltone.UI.ViewModels.About
{
    public class AboutViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(1000, ErrorMessage = "Name cannot exceed 1000 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(1000, ErrorMessage = "Company Name cannot exceed 1000 characters")]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Origins is required")]
        [StringLength(1000, ErrorMessage = "Origins Options cannot exceed 1000 characters")]
        [DisplayName("Origins")]
        public string Origins { get; set; }

        [Required(ErrorMessage = "Materials is required")]
        [StringLength(1000, ErrorMessage = "Materials Policy cannot exceed 1000 characters")]
        [DisplayName("Materials")]
        public string Materials { get; set; }
    }
}
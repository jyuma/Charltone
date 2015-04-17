using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Charltone.UI.ViewModels.Ordering
{
    public class OrderingHeaderEditViewModel
    {
        [Required]
        [StringLength(1000, ErrorMessage = "Summary cannot exceed 1000 characters")]
        [DisplayName("Summary")]
        public string Summary { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Pricing cannot exceed 1000 characters")]
        [DisplayName("Pricing")]
        public string Pricing { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Payment Options cannot exceed 1000 characters")]
        [DisplayName("Payment Options")]
        public string PaymentOptions { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Payment Policy cannot exceed 1000 characters")]
        [DisplayName("Payment Policy")]
        public string PaymentPolicy { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Shipping cannot exceed 1000 characters")]
        [DisplayName("Shipping")]
        public string Shipping { get; set; }
    }
}
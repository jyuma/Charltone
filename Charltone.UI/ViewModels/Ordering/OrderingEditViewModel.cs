using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Charltone.UI.ViewModels.Ordering
{
    public class OrderingEditViewModel
    {
        public int Id;

        [DisplayName("Instrument Type")]
        public SelectList InstrumentTypes { get; set; }

        [DisplayName("Classification")]
        public SelectList ClassificationTypes { get; set; }

        [DisplayName("Style")]
        public SelectList SubClassificationTypes { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [StringLength(25, ErrorMessage = "Model cannot exceed 25 characters")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Typical Price is required")]
        [StringLength(200, ErrorMessage = "Typical Price cannot exceed 200 characters")]
        [DisplayName("Typical Price")]
        public string TypicalPrice { get; set; }

        [Required(ErrorMessage = "Comments are required")]
        [StringLength(1000, ErrorMessage = "Comments cannot exceed 1000 characters")]
        public string Comments { get; set; }

        public int InstrumentTypeId { get; set; }
        public int ClassificationId { get; set; }
        public int SubClassificationId { get; set; }
        public int MaxImageWidth { get; set; }
        public int MaxImageHeight { get; set; }
    }
}

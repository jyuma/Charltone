using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Charltone.UI.ViewModels.Orderings
{
    public class OrderingEditViewModel
    {
        public int Id;

        //[UIHint("SelectList")]
        [DisplayName("Instrument Type")]
        public SelectList InstrumentTypes { get; set; }

        //[UIHint("SelectList")]
        [DisplayName("Classification")]
        public SelectList ClassificationTypes { get; set; }

        //[UIHint("SelectList")]
        [DisplayName("Style")]
        public SelectList SubClassificationTypes { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Description cannot exceed 25 characters")]
        public string Model { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        [DisplayName("Typical Price")]
        public string TypicalPrice { get; set; }

        [StringLength(1000, ErrorMessage = "Comments cannot exceed 1000 characters")]
        public string Comments { get; set; }

        public int InstrumentTypeId { get; set; }
        public int ClassificationId { get; set; }
        public int SubClassificationId { get; set; }
        public byte[] Photo { get; set; }
    }
}

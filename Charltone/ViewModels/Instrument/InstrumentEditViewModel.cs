using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Charltone.UI.ViewModels.Instrument
{
    public class InstrumentEditViewModel
    {
        public int Id;
        public int ProductId;

        [DisplayName("Instrument Type")]
        public SelectList InstrumentTypes { get; set; }

        [DisplayName("Classification")]
        public SelectList ClassificationTypes { get; set; }

        [DisplayName("Style")]
        public SelectList SubClassificationTypes { get; set; }

        [DisplayName("Status")]
        public SelectList StatusTypes { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Sn { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Top { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        [DisplayName("Back & Sides")]
        public string BackAndSides { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Body { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Binding { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Neck { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Faceplate { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Fingerboard { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        [DisplayName("Fret Markers")]
        public string FretMarkers { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        [DisplayName("Edge Dots")]
        public string EdgeDots { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Bridge { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Finish { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Tuners { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        [DisplayName("Pick Guard")]
        public string PickGuard { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Pickup { get; set; }

        [DisplayName("Nut Width")]
        public string NutWidth { get; set; }

        [DisplayName("Scale Length")]
        public string ScaleLength { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Comments { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        [DisplayName("Case")]
        public string CaseDetail { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        [DisplayName("Strings")]
        public string Strings { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        [DisplayName("Fun Facts")]
        public string FunFacts { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be numeric")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Display Price")]
        public string DisplayPrice { get; set; }

        public int InstrumentTypeId { get; set; }
        public int ClassificationId { get; set; }
        public int SubClassificationId { get; set; }
        public int StatusId { get; set; }

        [DisplayName("Posted")]
        public bool IsPosted { get; set; }

        public int DefaultPhotoId { get; set; }
    }
}

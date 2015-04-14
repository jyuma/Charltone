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

        [Required(ErrorMessage = "Model is required")]
        public virtual string Model { get; set; }

        [Required(ErrorMessage = "SN is required")]
        public virtual string Sn { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be numeric")]
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Display Price is required")]
        [DisplayName("Display Price")]
        public string DisplayPrice { get; set; }

        [StringLength(200, ErrorMessage = "Top cannot exceed 200 characters")]
        public string Top { get; set; }

        [StringLength(200, ErrorMessage = "Back & Sides cannot exceed 200 characters")]
        [DisplayName("Back & Sides")]
        public string BackAndSides { get; set; }

        [StringLength(200, ErrorMessage = "Body cannot exceed 200 characters")]
        public string Body { get; set; }

        [StringLength(200, ErrorMessage = "Binding cannot exceed 200 characters")]
        public string Binding { get; set; }

        [StringLength(200, ErrorMessage = "Neck cannot exceed 200 characters")]
        public string Neck { get; set; }

        [StringLength(200, ErrorMessage = "Faceplate cannot exceed 200 characters")]
        public string Faceplate { get; set; }

        [StringLength(200, ErrorMessage = "Fingerboard cannot exceed 200 characters")]
        public string Fingerboard { get; set; }

        [StringLength(200, ErrorMessage = "Fret Markers cannot exceed 200 characters")]
        [DisplayName("Fret Markers")]
        public string FretMarkers { get; set; }

        [StringLength(200, ErrorMessage = "Edge Dots cannot exceed 200 characters")]
        [DisplayName("Edge Dots")]
        public string EdgeDots { get; set; }

        [StringLength(200, ErrorMessage = "Bridge cannot exceed 200 characters")]
        public string Bridge { get; set; }

        [StringLength(200, ErrorMessage = "Finish cannot exceed 200 characters")]
        public string Finish { get; set; }

        [StringLength(200, ErrorMessage = "Tuners cannot exceed 200 characters")]
        public string Tuners { get; set; }

        [StringLength(200, ErrorMessage = "PickGuard cannot exceed 200 characters")]
        [DisplayName("Pick Guard")]
        public string PickGuard { get; set; }

        [StringLength(200, ErrorMessage = "Pickup cannot exceed 200 characters")]
        public string Pickup { get; set; }

        [DisplayName("Nut Width")]
        [StringLength(200, ErrorMessage = "Nut Width cannot exceed 200 characters")]
        public string NutWidth { get; set; }

        [DisplayName("Scale Length")]
        [StringLength(200, ErrorMessage = "Scale Length cannot exceed 200 characters")]
        public string ScaleLength { get; set; }

        [StringLength(200, ErrorMessage = "Case cannot exceed 200 characters")]
        [DisplayName("Case")]
        public string CaseDetail { get; set; }

        [StringLength(200, ErrorMessage = "Strings cannot exceed 200 characters")]
        [DisplayName("Strings")]
        public string Strings { get; set; }

        [StringLength(200, ErrorMessage = "Fret Wire cannot exceed 200 characters")]
        [DisplayName("Fret Wire")]
        public string FretWire { get; set; }

        [StringLength(200, ErrorMessage = "Dimensions cannot exceed 200 characters")]
        [DisplayName("Dimensions")]
        public string Dimensions { get; set; }

        [StringLength(1000, ErrorMessage = "Comments cannot exceed 1000 characters")]
        public string Comments { get; set; }

        [StringLength(1000, ErrorMessage = "Fun Facts cannot exceed 1000 characters")]
        [DisplayName("Fun Facts")]
        public string FunFacts { get; set; }

        public int InstrumentTypeId { get; set; }
        public int ClassificationId { get; set; }
        public int SubClassificationId { get; set; }
        public int StatusId { get; set; }

        [DisplayName("Posted")]
        public bool IsPosted { get; set; }

        public int DefaultPhotoId { get; set; }
    }
}

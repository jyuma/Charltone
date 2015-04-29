using Charltone.UI.Attributes;
using System.ComponentModel;
using System.Web;

namespace Charltone.UI.ViewModels.Instrument
{
    public class InstrumentDetailViewModel
    {
        public int Id { get; set; }
        public string InstrumentType { get; set; }
        public int ProductId { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Status { get; set; }
        public string StatusCssClassId { get; set; }
        public string Price { get; set; }
        public bool ShowPrice { get; set; }
        public int DefaultPhotoId { get; set; }

        public int MaxDisplayImageWidth { get; set; }
        public int MaxDisplayImageHeight { get; set; }
        public int MaxSaveImageWidth { get; set; }
        public int MaxSaveImageHeight { get; set; }
        public int MaxZoomImageWidth { get; set; }
        public int MaxZoomImageHeight { get; set; }

        [DisplayName("Class")]
        public string Classification { get; set; }

        [DisplayName("Style")]
        public string SubClassification { get; set; }

        public string ModelSn { get; set; }
        public string Top { get; set; }

        [DisplayName("Back & Sides")]
        public string BackAndSides { get; set; }

        public string Body { get; set; }
        public string Binding { get; set; }
        public string Bridge { get; set; }

        [DisplayName("Case")]
        public string CaseDetail { get; set; }

        public string Dimensions { get; set; }

        [DisplayName("Edge Dots")]
        public string EdgeDots { get; set; }

        public string Faceplate { get; set; }
        public string Finish { get; set; }
        public string Fingerboard { get; set; }

        [DisplayName("Fret Markers")]
        public string FretMarkers { get; set; }

        [DisplayName("Fret Wire")]
        public string FretWire { get; set; }

        public string Neck { get; set; }

        [DisplayName("Nut Width")]
        public string NutWidth { get; set; }

        [DisplayName("Pick Guard")]
        public string PickGuard { get; set; }

        [DisplayName("Pickup(s)")]
        public string Pickup { get; set; }

        [DisplayName("Scale Length")]
        public string ScaleLength { get; set; }

        public string Strings { get; set; }
        public string Tailpiece { get; set; }
        public string Tuners { get; set; }

        public string Comments { get; set; }

        [DisplayName("Fun Facts")]
        public string FunFacts { get; set; }
    }

    public class UploadFileModel
    {
        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase File { get; set; }
    }
}
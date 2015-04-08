using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Charltone.Data.Repositories;
using Charltone.Domain.Entities;
using System.Web.Mvc;
using DataAnnotationsExtensions;

namespace Charltone.UI.ViewModels.Instruments
{
    public class InstrumentEditViewModel
    {
        public InstrumentEditViewModel()
        {
        }

        public InstrumentEditViewModel(Product product, IInstrumentTypeRepository types)
        {
            var instrument = product.Instrument;

            Id = instrument.Id;

            InstrumentTypeId = instrument.InstrumentType.Id;
            ClassificationId = instrument.Classification.Id;
            SubClassificationId = instrument.SubClassification.Id;

            Model = instrument.Model;
            Sn = instrument.Sn;
            Top = instrument.Top;
            BackAndSides = instrument.BackAndSides;
            Body = instrument.Body;
            Binding = instrument.Binding;
            Neck = instrument.Neck;
            Faceplate = instrument.Faceplate;
            Fingerboard = instrument.Fingerboard;
            FretMarkers = instrument.FretMarkers;
            EdgeDots = instrument.EdgeDots;
            Bridge = instrument.Bridge;
            Finish = instrument.Finish;
            Tuners = instrument.Tuners;
            PickGuard = instrument.PickGuard;
            Pickup = instrument.Pickup;
            NutWidth = instrument.NutWidth;
            ScaleLength = instrument.ScaleLength;
            FunFacts = instrument.FunFacts;
            Comments = instrument.Comments;
            CaseDetail = instrument.CaseDetail;
            Strings = instrument.Strings;

            InstrumentTypes = new SelectList(types.GetInstrumentTypeList(), "Id", "InstrumentTypeDesc", instrument.InstrumentType.Id);
            ClassificationTypes = new SelectList(types.GetClassificationList(), "Id", "ClassificationDesc", instrument.Classification.Id);
            SubClassificationTypes = new SelectList(types.GetSubClassificationList(), "Id", "SubClassificationDesc", instrument.SubClassification.Id);
            StatusTypes = new SelectList(types.GetProductStatusList(), "Id", "StatusDesc", product.ProductStatus.Id);

            StatusId = product.ProductStatus.Id;
            Price = product.Price;
            DisplayPrice = product.DisplayPrice;
            IsPosted = product.IsPosted;
        }

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

        //[UIHint("SelectList")]
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

        [Numeric(ErrorMessage = "Price must be numeric")]
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
        public List<Photo> Photos { get; set; }
    }
}

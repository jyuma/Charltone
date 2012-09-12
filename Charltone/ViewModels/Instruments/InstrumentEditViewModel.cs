using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Charltone.Domain;
using Charltone.Repositories;
using System.Web.Mvc;
using DataAnnotationsExtensions;

namespace Charltone.ViewModels.Instruments
{
    public class InstrumentEditViewModel
    {
        private readonly ITypeRepository _types;
        private readonly IRepository<Product> _repository;

        public InstrumentEditViewModel()
        {
        }

        public InstrumentEditViewModel (Instrument instrument, ITypeRepository types, IRepository<Product> repository)
        {
            _types = types;
            _repository = repository;

            Id = instrument.Id;
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

            if (instrument.InstrumentType == null) instrument.InstrumentType = new InstrumentType();
            InstrumentTypes = new SelectList(_types.GetInstrumentTypeList(), "Id", "InstrumentTypeDesc", instrument.InstrumentType.Id);
            InstrumentTypeId = instrument.InstrumentType.Id;

            if (instrument.Classification == null) instrument.Classification = new Classification();
            ClassificationTypes = new SelectList(_types.GetClassificationList(), "Id", "ClassificationDesc", instrument.Classification.Id);
            ClassificationId = instrument.Classification.Id;

            if (instrument.SubClassification == null) instrument.SubClassification = new SubClassification();
            SubClassificationTypes = new SelectList(_types.GetSubClassificationList(), "Id", "SubClassificationDesc", instrument.SubClassification.Id);
            SubClassificationId = instrument.SubClassification.Id;

            if (instrument.Product == null)
            {
                //var product = products.GetSingle(instrument.Id);
                var product = _repository.GetSingle(instrument.Id);
                instrument.Product = product ?? new Product {ProductStatus = new ProductStatus {Id = 1}, Price = 0};
            }

            StatusTypes = new SelectList(_types.GetProductStatusList(), "Id", "StatusDesc", instrument.Product.ProductStatus.Id);
            StatusId = instrument.Product.ProductStatus.Id;
            Price = instrument.Product.Price;
            DisplayPrice = instrument.Product.DisplayPrice;

            IsPosted = instrument.Product.IsPosted;
        }

        public int Id;

        [UIHint("SelectList")]
        [DisplayName("Instrument Type")]
        public SelectList InstrumentTypes { get; set; }

        [UIHint("SelectList")]
        [DisplayName("Classification")]
        public SelectList ClassificationTypes { get; set; }

        [UIHint("SelectList")]
        [DisplayName("Style")]
        public SelectList SubClassificationTypes { get; set; }

        [UIHint("SelectList")]
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

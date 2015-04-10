using System.Collections.Generic;
using Charltone.Domain;
using Charltone.Repositories;

namespace Charltone.ViewModels.Instruments
{
    public class InstrumentDeleteViewModel
    {
        private readonly ITypeRepository _types;

        public InstrumentDeleteViewModel()
        {
        }

        public InstrumentDeleteViewModel(Instrument instrument)
        {
            Id = instrument.Id;

            Model = instrument.Model + ' ' + instrument.Sn;
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
            InstrumentTypeDesc = instrument.InstrumentType.InstrumentTypeDesc;
            ClassificationDesc = instrument.Classification.ClassificationDesc;
            SubClassificationDesc = instrument.SubClassification.SubClassificationDesc;
            StatusDesc = instrument.Product.ProductStatus.StatusDesc;
        }

        public int Id;

        public string Model { get; set; }
        public string Top { get; set; }
        public string BackAndSides { get; set; }
        public string Body { get; set; }
        public string Binding { get; set; }
        public string Neck { get; set; }
        public string Faceplate { get; set; }
        public string Fingerboard { get; set; }
        public string FretMarkers { get; set; }
        public string EdgeDots { get; set; }
        public string Bridge { get; set; }
        public string Finish { get; set; }
        public string Tuners { get; set; }
        public string PickGuard { get; set; }
        public string Pickup { get; set; }

        public string InstrumentTypeDesc { get; set; }
        public string ClassificationDesc { get; set; }
        public string SubClassificationDesc { get; set; }
        public string StatusDesc { get; set; }
        public int DefaultPhotoId { get; set; }
        public List<Photo> Photos { get; set; }
    }
}

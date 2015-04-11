﻿using System.Collections.Generic;
using System.ComponentModel;

namespace Charltone.UI.ViewModels.Instrument
{
    public class InstrumentDetailViewModel
    {
        public int Id { get; set; }
        public string InstrumentType { get; set; }

        [DisplayName("Class")]
        public string Classification { get; set; }

        [DisplayName("Style")]
        public string SubClassification { get; set; }

        public string Model { get; set; }
        public string Top { get; set; }

        [DisplayName("Back & Sides")]
        public string BackAndSides { get; set; }

        public string Body { get; set; }
        public string Binding { get; set; }
        public string Neck { get; set; }
        public string Faceplate { get; set; }
        public string Fingerboard { get; set; }

        [DisplayName("Fret Markers")]
        public string FretMarkers { get; set; }

        [DisplayName("Edge Dots")]
        public string EdgeDots { get; set; }

        public string Bridge { get; set; }
        public string Finish { get; set; }
        public string Tuners { get; set; }

        [DisplayName("Pick Guard")]
        public string PickGuard { get; set; }

        [DisplayName("Pickup(s)")]
        public string Pickup { get; set; }

        [DisplayName("Nut Width")]
        public string NutWidth { get; set; }

        [DisplayName("Scale Length")]
        public string ScaleLength { get; set; }

        [DisplayName("Fun Facts")]
        public string FunFacts { get; set; }

        public string Comments { get; set; }

        [DisplayName("Case")]
        public string CaseDetail { get; set; }

        public string Strings { get; set; }

        public string InstrumentStatus { get; set; }
        public int DefaultPhotoId { get; set; }
        public string Price { get; set; }
        public IList<int> PhotoIds { get; set; }
    }
}
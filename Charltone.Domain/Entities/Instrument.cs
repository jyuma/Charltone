namespace Charltone.Domain.Entities
{
    public class Instrument : EntityBase
    {
        public virtual InstrumentType InstrumentType { get; set; }
        public virtual Classification Classification { get; set; }
        public virtual SubClassification SubClassification { get; set; }
        public virtual string Model { get; set; }
        public virtual string Sn { get; set; }
        public virtual string Top { get; set; }

        public virtual string BackAndSides { get; set; }
        public virtual string Body { get; set; }
        public virtual string Binding { get; set; }
        public virtual string Bridge { get; set; }
        public virtual string CaseDetail { get; set; }
        public virtual string Dimensions { get; set; }
        public virtual string EdgeDots { get; set; }
        public virtual string Faceplate { get; set; }
        public virtual string Finish { get; set; }
        public virtual string Fingerboard { get; set; }
        public virtual string FretMarkers { get; set; }
        public virtual string FretWire { get; set; }
        public virtual string Neck { get; set; }
        public virtual string NutWidth { get; set; }
        public virtual string PickGuard { get; set; }
        public virtual string Pickup { get; set; }
        public virtual string Strings { get; set; }
        public virtual string ScaleLength { get; set; }
        public virtual string Tailpiece { get; set; }
        public virtual string Tuners { get; set; }

        public virtual string Comments { get; set; }
        public virtual string FunFacts { get; set; }
    }
}
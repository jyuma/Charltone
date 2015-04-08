namespace Charltone.Domain.Entities
{
    public class Ordering : EntityBase
    {
        public virtual InstrumentType InstrumentType { get; set; }
        public virtual Classification Classification { get; set; }
        public virtual SubClassification SubClassification { get; set; }
        public virtual string Model { get; set; }
        public virtual string TypicalPrice { get; set; }
        public virtual string Comments { get; set; }
        public virtual byte[] Photo { get; set; }
    }
}
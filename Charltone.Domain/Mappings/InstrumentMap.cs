using Charltone.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Charltone.Domain.Mappings

{
    public sealed class InstrumentMap : ClassMap<Instrument>
    {
        public InstrumentMap()
        {
            Table("Instrument");

            Id(x => x.Id).Column("Id");
            Map(m => m.Model).Column("Model");
            Map(m => m.Sn).Column("SN");
            Map(m => m.Top).Column("[Top]");
            Map(m => m.BackAndSides).Column("BackAndSides");
            Map(m => m.Body).Column("Body");
            Map(m => m.Binding).Column("Binding");
            Map(m => m.Neck).Column("Neck");
            Map(m => m.Faceplate).Column("Faceplate");
            Map(m => m.Fingerboard).Column("Fingerboard");
            Map(m => m.FretMarkers).Column("FretMarkers");
            Map(m => m.EdgeDots).Column("EdgeDots");
            Map(m => m.Bridge).Column("Bridge");
            Map(m => m.Finish).Column("Finish");
            Map(m => m.Tuners).Column("Tuners");
            Map(m => m.PickGuard).Column("PickGuard");
            Map(m => m.Pickup).Column("Pickup");
            Map(m => m.NutWidth).Column("NutWidth");
            Map(m => m.ScaleLength).Column("ScaleLength");
            Map(m => m.Comments).Column("Comments");
            Map(m => m.CaseDetail).Column("CaseDetail");
            Map(m => m.FunFacts).Column("FunFacts");
            Map(m => m.Strings).Column("Strings");
            Map(m => m.FretWire).Column("FretWire");
            Map(m => m.Dimensions).Column("Dimensions");

            References(m => m.InstrumentType).Column("InstrumentTypeId");
            References(m => m.Classification).Column("ClassificationId");
            References(m => m.SubClassification).Column("SubClassificationId");
        }
    }
}
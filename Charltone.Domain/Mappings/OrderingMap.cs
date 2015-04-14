using Charltone.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Charltone.Domain.Mappings
{
    public sealed class OrderingMap : ClassMap<Ordering>
    {
        public OrderingMap()
        {
            Table("Ordering");

            Id(x => x.Id).Column("Id");
            Map(m => m.Model).Column("Model");
            Map(m => m.TypicalPrice).Column("TypicalPrice");
            Map(m => m.Comments).Column("Comments");
            Map(m => m.Photo).Column("Photo").Length(int.MaxValue);

            References(m => m.InstrumentType).Column("InstrumentTypeId");
            References(m => m.Classification).Column("ClassificationId");
            References(m => m.SubClassification).Column("SubClassificationId");
        }
    }
}
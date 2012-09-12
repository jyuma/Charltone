using Charltone.Domain;
using FluentNHibernate.Mapping;

namespace Charltone.Mappings
{
    public sealed class SubClassificationMap : ClassMap<SubClassification>
    {
        public SubClassificationMap()
        {
            Table("SubClassification");
            Id(x => x.Id).Column("Id");
            Map(m => m.SubClassificationDesc).Column("SubClassificationDesc");
            Map(m => m.SortOrder).Column("SortOrder");

            //HasMany(m => m.Instruments);
        }
    }
}
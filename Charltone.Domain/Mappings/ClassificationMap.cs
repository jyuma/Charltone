using Charltone.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Charltone.Domain.Mappings
{
    public sealed class ClassificationMap : ClassMap<Classification>
    {
        public ClassificationMap()
        {
            Table("Classification");

            Id(x => x.Id).Column("Id");
            Map(m => m.ClassificationDesc).Column("ClassificationDesc");
            Map(m => m.SortOrder).Column("SortOrder");
        }
    }
}
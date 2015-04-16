using Charltone.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Charltone.Domain.Mappings
{
    public sealed class PhotoMap : ClassMap<Photo>
    {
        public PhotoMap()
        {
            Table("Photo");

            Id(x => x.Id).Column("Id");
            Map(m => m.IsDefault).Column("IsDefault");
            Map(m => m.Data).Column("Data").Length(int.MaxValue);
            Map(m => m.SortOrder).Column("SortOrder");
            References(m => m.Product).Fetch.Join().Column("ProductId").Cascade.None();
        }
    }
}
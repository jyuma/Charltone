using Charltone.Domain;
using FluentNHibernate.Mapping;

namespace Charltone.Mappings
{
    public sealed class PhotoMap : ClassMap<Photo>
    {
        public PhotoMap()
        {
            Table("Photo");

            Id(x => x.Id).Column("Id");
            Map(m => m.ProductId).Column("ProductId");
            Map(m => m.Data).Column("Data").Length(int.MaxValue);
            Map(m => m.IsDefault).Column("IsDefault");

            References(m => m.Product).Column("ProductId").ReadOnly().Cascade.None();
        }
    }
}
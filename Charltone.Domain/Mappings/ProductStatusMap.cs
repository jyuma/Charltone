using Charltone.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Charltone.Domain.Mappings
{
    public sealed class ProductStatusMap : ClassMap<ProductStatus>
    {
        public ProductStatusMap()
        {
            Table("ProductStatus");

            Id(x => x.Id).Column("Id");
            Map(m => m.StatusDesc).Column("StatusDesc");

            //HasMany(m => m.Instruments);
        }
    }
}
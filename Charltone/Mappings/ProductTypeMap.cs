using Charltone.Domain;
using FluentNHibernate.Mapping;

namespace Charltone.Mappings
{
    public class ProductTypeMap : ClassMap<ProductType>
    {
        public ProductTypeMap()
        {
            Table("ProductType");

            Id(x => x.Id).Column("Id");
            Map(m => m.ProductTypeDesc).Column("ProductTypeDesc");
            Map(m => m.SortOrder).Column("SortOrder");
        }
    }
}
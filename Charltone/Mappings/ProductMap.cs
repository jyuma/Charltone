using System.Collections.Generic;
using Charltone.Domain;
using FluentNHibernate.Mapping;

namespace Charltone.Mappings
{
    public sealed class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("Product");

            Id(x => x.Id).Column("Id");
            Map(m => m.ProductDesc).Column("ProductDesc");
            Map(m => m.Price).Column("Price");
            Map(m => m.DisplayPrice).Column("DisplayPrice");
            Map(m => m.IsPosted).Column("IsPosted");

            HasMany(m => m.Photos).KeyColumn("ProductId").Cascade.Delete();
            References(m => m.ProductType).Column("ProductTypeId");
            References(m => m.ProductStatus).Column("StatusId");
            References(m => m.Instrument).Column("InstrumentId").Cascade.All();
        }
    }
}
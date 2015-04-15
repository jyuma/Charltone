using Charltone.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Charltone.Domain.Mappings
{
    internal class OrderingHeaderContentMap : ClassMap<OrderingHeaderContent>
    {
        public OrderingHeaderContentMap()
        {
            Table("OrderingHeaderContent");

            Id(x => x.Id).Column("Id");
            Map(m => m.Summary).Column("Summary");
            Map(m => m.Pricing).Column("Pricing");
            Map(m => m.PaymentOptions).Column("PaymentOptions");
            Map(m => m.PaymentPolicy).Column("PaymentPolicy");
            Map(m => m.Shipping).Column("Shipping");
        }
    }
}

using Charltone.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Charltone.Domain.Mappings
{
    public sealed class HomePageImageMap : ClassMap<HomePageImage>
    {
        public HomePageImageMap()
        {
            Table("HomePageImage");

            Id(x => x.Id).Column("Id");
            Map(m => m.Data).Column("Data").Length(int.MaxValue);
        }
    }
}
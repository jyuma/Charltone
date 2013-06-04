using Charltone.Domain;
using FluentNHibernate.Mapping;

namespace Charltone.Mappings
{
    public sealed class HomePageImageMap : ClassMap<HomePageImage>
    {
        public HomePageImageMap()
        {
            Table("HomePageImage");

            Id(x => x.Id).Column("Id");
            Map(m => m.Data).Column("Data").Length(int.MaxValue);
            Map(m => m.SortOrder).Column("SortOrder");
        }
    }
}
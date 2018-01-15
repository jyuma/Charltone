using Charltone.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Charltone.Domain.Mappings
{
    public sealed class HomeContentMap : ClassMap<HomeContent>
    {
        public HomeContentMap()
        {
            Table("HomeContent");

            Id(x => x.Id).Column("Id");
            Map(m => m.Introduction).Column("Introduction");
            Map(m => m.Greeting).Column("Greeting");
            Map(m => m.Photo).Column("Photo").Length(int.MaxValue);
        }
    }
}
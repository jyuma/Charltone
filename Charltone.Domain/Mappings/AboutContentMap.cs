using Charltone.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Charltone.Domain.Mappings
{
    internal class AboutContentMap : ClassMap<AboutContent>
    {
        public AboutContentMap()
        {
            Table("AboutContent");

            Id(x => x.Id).Column("Id");
            Map(m => m.Name).Column("Name");
            Map(m => m.CompanyName).Column("CompanyName");
            Map(m => m.Origins).Column("Origins");
            Map(m => m.Materials).Column("Materials");
        }
    }
}

using Charltone.Domain;
using FluentNHibernate.Mapping;

namespace Charltone.Mappings
{
    public sealed class AdminUserMap : ClassMap<AdminUser>
    {
        public AdminUserMap()
        {
            Table("AdminUser");
            Id(x => x.Id).Column("Id");
            Map(m => m.AdminPassword).Column("AdminPassword");
        }
    }
}
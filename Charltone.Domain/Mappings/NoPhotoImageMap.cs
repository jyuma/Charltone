using Charltone.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Charltone.Domain.Mappings
{
    public sealed class NoPhotoImageMap : ClassMap<NoPhotoImage>
    {
        public NoPhotoImageMap()
        {
            Id(x => x.Id).Column("Id");
            Map(m => m.Data).Column("Data").Length(int.MaxValue);
        }
    }
}
using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IAboutContentRepository : IRepositoryBase<AboutContent>
    {
    }

    public class AboutContentRepository : RepositoryBase<AboutContent>, IAboutContentRepository
    {
        public AboutContentRepository(ISession session)
            : base(session)
        {
        }
    }
}
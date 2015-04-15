using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IHomeContentRepository : IRepositoryBase<HomeContent>
    {
    }

    public class HomeContentRepository : RepositoryBase<HomeContent>, IHomeContentRepository
    {
        public HomeContentRepository(ISession session)
            : base(session)
        {
        }
    }
}

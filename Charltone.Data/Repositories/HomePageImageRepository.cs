using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IHomePageImageRepository : IRepositoryBase<HomePageImage>
    {
    }

    public class HomePageImageRepository : RepositoryBase<HomePageImage>, IHomePageImageRepository
    {
        public HomePageImageRepository(ISession session)
            : base(session)
        {
        }
    }
}

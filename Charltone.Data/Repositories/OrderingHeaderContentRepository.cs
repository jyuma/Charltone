using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IOrderingHeaderContentRepository : IRepositoryBase<OrderingHeaderContent>
    {
    }

    public class OrderingHeaderContentRepository : RepositoryBase<OrderingHeaderContent>, IOrderingHeaderContentRepository
    {
        public OrderingHeaderContentRepository(ISession session)
            : base(session)
        {
        }
    }
}

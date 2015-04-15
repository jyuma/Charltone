using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IProductStatusRepository : IRepositoryBase<ProductStatus>
    {
    }

    public class ProductStatusRepository : RepositoryBase<ProductStatus>, IProductStatusRepository
    {
        public ProductStatusRepository(ISession session)
            : base(session)
        {
        }
    }
}

using System.Collections.Generic;
using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        IList<Product> GetInstrumentList(bool includeUnposted);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ISession session)
            : base(session)
        {
        }

        public IList<Product> GetInstrumentList(bool includeUnposted)
        {
            var products = includeUnposted 
                ? Session.QueryOver<Product>()
                    .Where(x => x.ProductType.Id == 1)   // Instruments
                    .List()

                : Session.QueryOver<Product>()
                    .Where(x => x.ProductType.Id == 1)   // Instruments
                    .Where(x => x.IsPosted)
                    .List();

            return products;
        }
    }
}

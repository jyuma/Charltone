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
        private readonly IPhotoRepository _photoRepository;

        public ProductRepository(ISession session)
            : base(session)
        {
            _photoRepository = new PhotoRepository(session);
        }

        public override void Delete(int id)
        {
            var photos = _photoRepository.GetListByProductId(id);
            foreach (var photo in photos)
            {
                _photoRepository.Delete(photo.Id);
            }

            base.Delete(id);
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

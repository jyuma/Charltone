using Charltone.Domain;

namespace Charltone.Repositories
{
	using NHibernate;
    using System.Collections.Generic;

	public class ProductRepository : IProductRepository
	{
		//private readonly int pageSize;
		private readonly ISession _session;

        public ProductRepository(ISession session)  //int pageSize
		{
            //this.pageSize = pageSize;
            _session = session;
            //_photos = photos;
		}

        public IList<Product> GetInstrumentList(int pageNumber, bool includeUnposted)
        {
            //var firstResult = pageSize * (pageNumber - 1);
            using (var tx = _session.BeginTransaction())
            {
                //--- product type "1" is for instruments
                var products = includeUnposted ? _session.QueryOver<Product>().Where(x => x.ProductType.Id == 1).List() : 
                    _session.QueryOver<Product>().Where(x => x.IsPosted).List();
                
                tx.Commit();
                return products;
            }
        }

        //public int Count()
        //{
        //    var totalcount = _session.QueryOver<Product>().ToRowCountQuery().FutureValue<int>().Value;
        //    return totalcount;
        //}

        //public Product GetSingle(int id)
        //{
        //    using (var tx = _session.BeginTransaction())
        //    {
        //        var product = _session.QueryOver<Product>().Where(x => x.Id == id).SingleOrDefault();

        //        tx.Commit();
        //        return product;
        //    }
        //}

        //public void Update(Product product)
        //{
        //    using (var tx = _session.BeginTransaction())
        //    {
        //        _session.SaveOrUpdate(product);
        //        tx.Commit();
        //    }
        //}

        //public void Save(Product product)
        //{
        //    using (var tx = _session.BeginTransaction())
        //    {
        //        _session.Save(product);
        //        tx.Commit();
        //    }
        //}

        //public void Delete(Product product)
        //{
        //    //var photos = _photos.GetList(product.Id);
        //    //foreach (var p in photos)
        //    //    _photos.Delete(p.Id);

        //    using (var tx = _session.BeginTransaction())
        //    {
        //        _session.Delete(product);
        //        tx.Commit();
        //    }
        //}
    }
}
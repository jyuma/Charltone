using Charltone.Domain;

namespace Charltone.Repositories
{
    using System.Collections.Generic;

	public interface IProductRepository
	{
        IList<Product> GetInstrumentList(int pageNumber, bool includeUnposted);
        //Product GetSingle(int id);
        //int Count();
        //void Update(Product product);
        //void Save(Product product);
        //void Delete(Product product);
	}
}
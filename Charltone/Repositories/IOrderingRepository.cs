using Charltone.Domain;

namespace Charltone.Repositories
{
    using System.Collections.Generic;

	public interface IOrderingRepository
	{
        IList<Ordering> GetList(int pageNumber);
        Ordering GetSingle(int id);
        int Count();
	    void Update(Ordering ordering);
        void Save(Ordering ordering);
        void Delete(Ordering ordering);
	    byte[] GetPhoto(int id);
	}
}
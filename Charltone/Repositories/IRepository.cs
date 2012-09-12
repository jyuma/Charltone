using Charltone.Domain;

namespace Charltone.Repositories
{
    using System.Collections.Generic;

	public interface IRepository<T> where T : class
	{
        IList<T> GetList();
        T GetSingle(int id);
        int Count();
	    void Update(T item);
        void Save(T item);
        void Delete(T item);
	}
}
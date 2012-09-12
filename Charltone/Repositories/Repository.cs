using Charltone.Domain;
using NHibernate.Criterion;

namespace Charltone.Repositories
{
	using NHibernate;
    using System.Collections.Generic;

	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ISession _session;

        public Repository(ISession session)
		{
            _session = session;
		}

        public IList<T> GetList()
        {
            using (var tx = _session.BeginTransaction())
            {
                var items = _session.QueryOver<T>().List();
                
                tx.Commit();
                return items;
            }
        }

        public int Count()
        {
            var totalcount = _session.QueryOver<T>().ToRowCountQuery().FutureValue<int>().Value;
            return totalcount;
        }

        public T GetSingle(int id)
        {
            ICriterion query = Restrictions.Eq("Id", id);

            using (var tx = _session.BeginTransaction())
            {

                var item = _session.QueryOver<T>().Where(query).SingleOrDefault();

                tx.Commit();
                return item;
            }
        }

        public void Update(T item)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(item);
                tx.Commit();
            }
        }

        public void Save(T item)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(item);
                tx.Commit();
            }
        }

        public void Delete(T item)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(item);
                tx.Commit();
                //_session.Flush();
            }
        }
	}
}
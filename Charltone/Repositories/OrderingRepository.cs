using Charltone.Domain;

namespace Charltone.Repositories
{
	using NHibernate;
    using System.Collections.Generic;

	public class OrderingRepository : IOrderingRepository
	{
		//private readonly int pageSize;
		private readonly ISession _session;

        public OrderingRepository(ISession session)  //int pageSize, 
		{
            //this.pageSize = pageSize;
            _session = session;
		}

        public IList<Ordering> GetList(int pageNumber)
        {
            //var firstResult = pageSize * (pageNumber - 1);
            using (var tx = _session.BeginTransaction())
            {
                var orderings = _session.QueryOver<Ordering>().List();
                
                tx.Commit();
                return orderings;
            }
        }

        public int Count()
        {
            var totalcount =  _session.QueryOver<Ordering>().ToRowCountQuery().FutureValue<int>().Value;
            return totalcount;
        }

        public Ordering GetSingle(int id)
        {
            using (var tx = _session.BeginTransaction())
            {
                var ordering = _session.QueryOver<Ordering>().Where(x => x.Id == id).SingleOrDefault();

                tx.Commit();
                return ordering;
            }
        }

        public void Update(Ordering ordering)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(ordering);
                tx.Commit();
            }
        }

        public void Save(Ordering ordering)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(ordering);
                tx.Commit();
            }
        }

        public void Delete(Ordering ordering)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(ordering);
                tx.Commit();
                //_session.Flush();
            }
        }

        public byte[] GetPhoto(int id)
        {
            var ordering = _session.QueryOver<Ordering>().Where(x => x.Id == id).SingleOrDefault();
            return ordering.Photo;
        }
	}
}
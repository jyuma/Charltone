using Charltone.Domain;

namespace Charltone.Repositories
{
	using NHibernate;
    using System.Collections.Generic;

	public class InstrumentRepository : IInstrumentRepository
	{
		//private readonly int pageSize;
		private readonly ISession _session;
        private readonly IRepository<Instrument> _repository;

        public InstrumentRepository(ISession session, IRepository<Instrument> repository)  //int pageSize, 
		{
            //this.pageSize = pageSize;
            _session = session;
            _repository = repository;
		}

        public IList<Instrument> GetList(int pageNumber, bool includeUnposted)
        {
            //var firstResult = pageSize * (pageNumber - 1);
            using (var tx = _session.BeginTransaction())
            {
                var instruments = includeUnposted ? _session.QueryOver<Instrument>().List() : _session.QueryOver<Instrument>().JoinQueryOver(x => x.Product).Where(x => x.IsPosted).List();
                
                tx.Commit();
                return instruments;
            }
        }

        public int Count(bool includeUnposted)
        {
            var totalcount =  includeUnposted ? _session.QueryOver<Instrument>().ToRowCountQuery().FutureValue<int>().Value : _session.QueryOver<Instrument>().JoinQueryOver(x => x.Product).Where(x => x.IsPosted).ToRowCountQuery().FutureValue<int>().Value;
            return totalcount;
        }

        public Instrument GetSingle(int id)
        {
            using (var tx = _session.BeginTransaction())
            {
                var instrument = _session.QueryOver<Instrument>().Where(x => x.Id == id).SingleOrDefault();

                tx.Commit();
                return instrument;
            }
        }

        public void Update(Instrument instrument)
        {
            _repository.Update(instrument);
            //using (var tx = _session.BeginTransaction())
            //{
            //    _session.SaveOrUpdate(instrument);
            //    tx.Commit();
            //}
        }

        //public void Save(Instrument instrument)
        //{
        //    using (var tx = _session.BeginTransaction())
        //    {
        //        _session.Save(instrument);
        //        tx.Commit();
        //    }
        //}

        //public void Delete(Instrument instrument)
        //{
        //    using (var tx = _session.BeginTransaction())
        //    {
        //        _session.Delete(instrument);
        //        tx.Commit();
        //        //_session.Flush();
        //    }
        //}
	}
}
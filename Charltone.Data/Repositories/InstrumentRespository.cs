using System.Collections.Generic;
using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IInstrumentRepository : IRepositoryBase<Instrument>
    {
        //IList<Instrument> GetList(int pageNumber, bool includeUnposted);
        //int Count(bool includeUnposted);
    }

    public class InstrumentRepository : RepositoryBase<Instrument>, IInstrumentRepository
    {
        public InstrumentRepository(ISession session)
            : base(session)
        {
        }

        //public IList<Instrument> GetList(int pageNumber, bool includeUnposted)
        //{
        //    using (var tx = Session.BeginTransaction())
        //    {
        //        var instruments = includeUnposted ? Session.QueryOver<Instrument>().List() : Session.QueryOver<Instrument>().JoinQueryOver(x => x.Product).Where(x => x.IsPosted).List();

        //        tx.Commit();
        //        return instruments;
        //    }
        //}

        //public int Count(bool includeUnposted)
        //{
        //    var totalcount = includeUnposted ? Session.QueryOver<Instrument>().ToRowCountQuery().FutureValue<int>().Value : Session.QueryOver<Instrument>().JoinQueryOver(x => x.Product).Where(x => x.IsPosted).ToRowCountQuery().FutureValue<int>().Value;
        //    return totalcount;
        //}
    }
}

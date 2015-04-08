using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IInstrumentRepository : IRepositoryBase<Instrument>
    {
    }

    public class InstrumentRepository : RepositoryBase<Instrument>, IInstrumentRepository
    {
        public InstrumentRepository(ISession session)
            : base(session)
        {
        }
    }
}

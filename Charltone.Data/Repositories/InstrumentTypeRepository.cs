using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IInstrumentTypeRepository : IRepositoryBase<InstrumentType>
    {
    }

    public class InstrumentTypeRepository : RepositoryBase<InstrumentType>, IInstrumentTypeRepository
    {
        public InstrumentTypeRepository(ISession session)
            : base(session)
        {
        }
    }
}

using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface ISubClassificationRepository : IRepositoryBase<SubClassification>
    {
    }

    public class SubClassificationRepository : RepositoryBase<SubClassification>, ISubClassificationRepository
    {
        public SubClassificationRepository(ISession session)
            : base(session)
        {
        }
    }
}

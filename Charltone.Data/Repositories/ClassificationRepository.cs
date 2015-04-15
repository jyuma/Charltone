using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IClassificationRepository : IRepositoryBase<Classification>
    {
    }

    public class ClassificationRepository : RepositoryBase<Classification>, IClassificationRepository
    {
        public ClassificationRepository(ISession session)
            : base(session)
        {
        }
    }
}


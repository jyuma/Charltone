using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IOrderingRepository : IRepositoryBase<Ordering>
    {
        byte[] GetPhoto(int ordingId);
        void UpdatePhoto(int orderingId, byte[] photo);
    }

    public class OrderingRepository : RepositoryBase<Ordering>, IOrderingRepository
    {
        public OrderingRepository(ISession session)
            : base(session)
        {
        }

        public byte[] GetPhoto(int ordingId)
        {
            return Session.QueryOver<Ordering>()
                .Where(x => x.Id == ordingId)
                .SingleOrDefault().Photo;
        }

        public void UpdatePhoto(int orderingId, byte[] photo)
        {
            var ordering = Get(orderingId);
            ordering.Photo = photo;
            Update(ordering);
        }
    }
}
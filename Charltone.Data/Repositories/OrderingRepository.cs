using System.Collections.Generic;
using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IOrderingRepository : IRepositoryBase<Ordering>
    {
        IList<Ordering> GetList(int pageNumber);
        byte[] GetPhoto(int id);
    }

    public class OrderingRepository : RepositoryBase<Ordering>, IOrderingRepository
    {
        public OrderingRepository(ISession session)
            : base(session)
        {
        }

        public IList<Ordering> GetList(int pageNumber)
        {
            return Session.QueryOver<Ordering>().List();
        }

        public byte[] GetPhoto(int id)
        {
            return Session.QueryOver<Ordering>()
                .Where(x => x.Id == id)
                .SingleOrDefault().Photo;
        }
    }
}
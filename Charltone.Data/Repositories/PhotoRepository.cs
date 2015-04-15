using System.Collections.Generic;
using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IPhotoRepository : IRepositoryBase<Photo>
    {
        byte[] GetData(int id);
        byte[] GetDefaultInstrumentImage();

        void SetProductDefault(int id, int newid);
    }

    public class PhotoRepository : RepositoryBase<Photo>, IPhotoRepository
    {
        public PhotoRepository(ISession session) : base(session) {}

        public byte[] GetData(int id)
        {
            return Session.Load<Photo>(id).Data;
        }

        public byte[] GetDefaultInstrumentImage()
        {
            return Session.QueryOver<NoPhotoImage>().SingleOrDefault().Data;
        }

        public void SetProductDefault(int productId, int newid)
        {
            var photos = GetListByProductId(productId);
            using (var tx = Session.BeginTransaction())
            {
                foreach (var p in photos)
                {
                    p.IsDefault = (p.Id == newid);
                    Session.SaveOrUpdate(p);
                }
                tx.Commit();
            }
        }

        private IEnumerable<Photo> GetListByProductId(int productId)
        {
            return Session.QueryOver<Photo>()
                .Where(x => x.ProductId == productId)
                .List();
        }
    }
}

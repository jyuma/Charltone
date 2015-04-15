using System.Collections.Generic;
using System.Linq;
using Charltone.Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Charltone.Data.Repositories
{
    public interface IPhotoRepository : IRepositoryBase<Photo>
    {
        IList<Photo> GetListByProductId(int productId);
        IList<int> GetIdsByProductId(int productId);
        int CountByProductId(int productId);

        byte[] GetData(int id);
        byte[] GetDefaultInstrumentImage();

        void SetProductDefault(int id, int newid);

        int GetDefaultId(int id);
    }

    public class PhotoRepository : RepositoryBase<Photo>, IPhotoRepository
    {
        public PhotoRepository(ISession session) : base(session) {}

        public IList<Photo> GetListByProductId(int productId)
        {
            return Session.QueryOver<Photo>()
                .Where(x => x.ProductId == productId)
                .List();
        }

        public IList<int> GetIdsByProductId(int productId)
        {
            var ids = Session.Query<Photo>()
                .Where(x => x.ProductId == productId)
                .Select(x => x.Id)
                .ToList();
         
            return ids;
        }

        public int CountByProductId(int productId)
        {
            return Session.QueryOver<Photo>()
                .Where(x => x.ProductId == productId)
                .ToRowCountQuery()
                .FutureValue<int>().Value;
        }

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

        public int GetDefaultId(int productId)
        {
            if (CountByProductId(productId) <= 0) return -1;

            var photo = Session.QueryOver<Photo>()
                .Where(x => x.ProductId == productId)
                .And(x => x.IsDefault)
                .SingleOrDefault();

            return photo.Id;
        }
    }
}

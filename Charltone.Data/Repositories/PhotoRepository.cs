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

        byte[] GetData(int id);
        byte[] GetHomePageImage();
        byte[] GetDefaultInstrumentImage();

        void Add(int id, byte[] file);
        void UpdateHomePageImage(byte[] data);
        void SetProductDefault(int id, int newid);

        int GetDefaultId(int id);
        int Count(int id);
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

        public byte[] GetData(int id)
        {
            return Session.Load<Photo>(id).Data;
        }

        public byte[] GetHomePageImage()
        {
            return Session.QueryOver<HomePageImage>()
                .SingleOrDefault()
                .Data;
        }

        public byte[] GetDefaultInstrumentImage()
        {
            return Session.QueryOver<NoPhotoImage>().SingleOrDefault().Data;
        }

        public void Add(int productId, byte[] data)
        {
            var photos = GetListByProductId(productId);
            var photo = new Photo { IsDefault = (Count(productId) == 0), ProductId = productId, Data = data };

            photos.Add(photo);

            using (var tx = Session.BeginTransaction())
            {
                Session.SaveOrUpdate(photo);
                tx.Commit();
            }
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

        public void UpdateHomePageImage(byte[] data)
        {
            var image = Session.QueryOver<HomePageImage>().SingleOrDefault();
            image.Data = data;

            using (var tx = Session.BeginTransaction())
            {
                Session.SaveOrUpdate(image);
                tx.Commit();
            }
        }

        public int GetDefaultId(int id)
        {
            if (Count(id) <= 0) return -1;

            var photo = Session.QueryOver<Photo>()
                .Where(x => x.ProductId == id)
                .And(x => x.IsDefault)
                .SingleOrDefault();

            return photo.Id;
        }

        public int Count(int id)
        {
            return Session.QueryOver<Photo>()
                .Where(x => x.ProductId == id)
                .ToRowCountQuery()
                .FutureValue<int>().Value;
        }
    }
}

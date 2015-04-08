using System.Collections.Generic;
using System.Linq;
using Charltone.Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Charltone.Data.Repositories
{
    public interface IPhotoRepository : IRepositoryBase<Photo>
    {
        IList<Photo> GetList(int id);
        IList<int> GetIdList(int id);
        byte[] GetData(int id);
        byte[] GetHomePageImageData();
        HomePageImage GetHomePageImage();
        void UpdateHomePageImage(byte[] data);
        int GetDefaultId(int id);
        void UpdateDefault(int id, int newid);
        void AddPhoto(int id, byte[] file);
        int Count(int id);
    }

    public class PhotoRepository : RepositoryBase<Photo>, IPhotoRepository
    {
        public PhotoRepository(ISession session) : base(session) {}

        public IList<Photo> GetList(int id)
        {
            return Session.QueryOver<Photo>()
                .Where(x => x.ProductId == id)
                .List();
        }

        public IList<int> GetIdList(int id)
        {
            var ids = Session.Query<Photo>()
                .Where(x => x.ProductId == id)
                .Select(x => x.Id)
                .ToList();
         
            return ids;
        }

        public byte[] GetData(int id)
        {
            try
            {
                return id > 0 ? Session.Load<Photo>(id).Data : GetNoPhotoImageData();
            }
            catch (HibernateException e)
            {
                var ex = e.InnerException;
            }
            return null;
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

        public void AddPhoto(int id, byte[] file)
        {
            var photos = GetList(id);
            var photo = new Photo { IsDefault = (Count(id) == 0), ProductId = id, Data = file };

            photos.Add(photo);

            using (var tx = Session.BeginTransaction())
            {
                Session.SaveOrUpdate(photo);
                tx.Commit();
            }
        }

        public void UpdateDefault(int id, int newid)
        {
            var photos = GetList(id);
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

        public byte[] GetHomePageImageData()
        {
            return Session.QueryOver<HomePageImage>()
                .SingleOrDefault()
                .Data;
        }

        public HomePageImage GetHomePageImage()
        {
            return Session.QueryOver<HomePageImage>()
                .SingleOrDefault();
        }

        public void UpdateHomePageImage(byte[] data)
        {
            var image = GetHomePageImage();
            image.Data = data;

            using (var tx = Session.BeginTransaction())
            {
                Session.SaveOrUpdate(image);
                tx.Commit();
            }
        }

        public int Count(int id)
        {
            return Session.QueryOver<Photo>()
                .Where(x => x.ProductId == id)
                .ToRowCountQuery()
                .FutureValue<int>().Value;
        }

        private byte[] GetNoPhotoImageData()
        {
            return Session.QueryOver<NoPhotoImage>().SingleOrDefault().Data;
        }
    }
}

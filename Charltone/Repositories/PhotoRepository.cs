using System.Linq;
using NHibernate.Linq;
using Charltone.Domain;

namespace Charltone.Repositories
{
    using NHibernate;
    using System.Collections.Generic;

    public class PhotoRepository : IPhotoRepository
    {
        private readonly ISession _session;
        private readonly IRepository<Photo> _photos;
        private readonly IRepository<HomePageImage> _homePageimages;

        public PhotoRepository(ISession session, IRepository<Photo> photos, IRepository<HomePageImage> homePageimages)
		{
            _session = session;
            _photos = photos;
            _homePageimages = homePageimages;
		}

        public IList<Photo> GetList(int id)
        {
            return _session.QueryOver<Photo>()
                .Where(x => x.ProductId == id).List();
        }

        public IList<int> GetIdList(int id)
        {
            var ids = from i in _session.Query<Photo>()
                      where i.ProductId == id
                      select i.Id;
            return ids.ToList();
        }

        public Photo GeSingle(int id)
        {
            return _photos.GetSingle(id);
        }

        public byte[] GetData(int id)
        {
            return id > 0 ? _session.Load<Photo>(id).Data : GetNoPhotoImageData();
        }

        public int GetDefaultId(int id)
        {
            if (Count(id) <= 0) return -1;
            var photo =_session.QueryOver<Photo>()
                .Where(x => x.ProductId == id)
                .And(x => x.IsDefault)
                .SingleOrDefault();
            return photo.Id;
        }

        public void Delete(int id)
        {
            var photo = GeSingle(id);

            _photos.Delete(photo);
        }

        public void AddPhoto(int id, byte[] file)
        {
            var photos = GetList(id);
            var photo = new Photo {IsDefault = (Count(id) == 0), ProductId = id, Data = file};

            photos.Add(photo);

            using (var tx = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(photo);
                tx.Commit();
            }
        }

        public void UpdateDefault(int id, int newid)
        {
            //--- update the default photo
            var defaultid = GetDefaultId(id);

            if (newid == defaultid) return;

            var photos = GetList(id);
            foreach (var p in photos)
            {
                var photo = GeSingle(p.Id);
                photo.IsDefault = (p.Id == newid);
                _session.SaveOrUpdate(photo);
            }
        }

        private byte[] GetNoPhotoImageData()
        {
            return _session.QueryOver<NoPhotoImage>().SingleOrDefault().Data;
        }

        public byte[] GetHomePageImageData(int id)
        {
            return _session.QueryOver<HomePageImage>()
                .Where(x => x.Id == id)                
                .SingleOrDefault()
                .Data;
        }

        public HomePageImage GetHomePageImage(int id)
        {
            return _session.QueryOver<HomePageImage>()
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public void AddHomePageImage(byte[] file)
        {
            var sortOrder = GetNextHomePageImageSortOrder();
            var image = new HomePageImage { Data = file, SortOrder = sortOrder };
            var imageList = GetHomePageImageList();
            imageList.Add(image);

            using (var tx = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(image);
                tx.Commit();
            }
        }

        public void UpdateHomePageImage(int id, byte[] file, int sortOrder)
        {
            var image = GetHomePageImage(id);
            image.Data = file;
            image.SortOrder = sortOrder;

            using (var tx = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(image);
                tx.Commit();
            }
        }

        private int GetNextHomePageImageSortOrder()
        {
            return _session.Query<HomePageImage>().Max(x => x.SortOrder) + 1;
        }

        public IList<int> GetHomePageImageIdList()
        {
            var ids = from i in _session.Query<HomePageImage>()
                        orderby i.SortOrder
                        select i.Id;
            return ids.ToList();
        }

        public IList<HomePageImage> GetHomePageImageList()
        {
            var images = from i in _session.Query<HomePageImage>()
                      orderby i.SortOrder
                      select i;
            return images.ToList();
        }

        public void DeleteHomePageImage(int id)
        {
            var image = GetHomePageImage(id);
            _homePageimages.Delete(image);
        }

        public int Count(int id)
        {
            var totalcount = _session.QueryOver<Photo>().Where(x => x.ProductId == id).ToRowCountQuery().FutureValue<int>().Value;
            return totalcount;
        }

    }
}
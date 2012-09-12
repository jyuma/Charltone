using Charltone.Domain;

namespace Charltone.Repositories
{
    using NHibernate;
    using System.Collections.Generic;

    public class PhotoRepository : IPhotoRepository
    {
        private readonly ISession _session;
        private readonly IRepository<Photo> _repository;

        public PhotoRepository(ISession session, IRepository<Photo> repository)
		{
            _session = session;
            _repository = repository;
		}

        public IList<Photo> GetList(int id)
        {
            return _session.QueryOver<Photo>().Where(x => x.ProductId == id).List();
        }

        public Photo GeSingle(int id)
        {
            return _repository.GetSingle(id);
            //return _session.QueryOver<Photo>().Where(x => x.Id == id).SingleOrDefault();
        }

        public byte[] GetData(int id)
        {
            return id > 0 ? _session.Load<Photo>(id).Data : GetNoPhotoImageData();
        }

        public int GetDefaultId(int id)
        {
            if (Count(id) <= 0) return -1;
            var photo =_session.QueryOver<Photo>().Where(x => x.ProductId == id).And(x => x.IsDefault).SingleOrDefault();
            return photo.Id;
        }

        public void Delete(int id)
        {
            var photo = GeSingle(id);

            _repository.Delete(photo);
            //_session.Delete(photo);

            //using (var tx = _session.BeginTransaction())
            //{
            //    _session.Delete(photo);
            //    tx.Commit();
            //}
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

            //using (var tx = _session.BeginTransaction())
            //{
                var photos = GetList(id);
                foreach (var p in photos)
                {
                    var photo = GeSingle(p.Id);
                    photo.IsDefault = (p.Id == newid);
                    _session.SaveOrUpdate(photo);
                }
                //tx.Commit();
            //}
        }

        private byte[] GetNoPhotoImageData()
        {
            return _session.QueryOver<NoPhotoImage>().SingleOrDefault().Data;
        }

        public int Count(int id)
        {
            var totalcount = _session.QueryOver<Photo>().Where(x => x.ProductId == id).ToRowCountQuery().FutureValue<int>().Value;
            return totalcount;
        }

    }
}
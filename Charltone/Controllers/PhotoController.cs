using System.Collections.Generic;
using System.IO;
using System.Web;
using Charltone.Domain;
using System.Linq;
using Charltone.ViewModels.Photos;

namespace Charltone.Controllers
{
    using System.Web.Mvc;
    using Repositories;

    public class PhotoController : Controller
    {
        private readonly IInstrumentRepository _instruments;
        private readonly IPhotoRepository _photos;

        public PhotoController(IInstrumentRepository instruments, IPhotoRepository photos)
        {
            _instruments = instruments;
            _photos = photos;
        }

        [Authorize]
        public ActionResult Index(int id)
        {
            var photos = _photos.GetList(id);

            return View(LoadPhotosEditViewModel(id, photos));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(int id, FormCollection collection, string actionType)
        {
            var photos = _photos.GetList(id);

            Delete(photos, collection);
            SetDefault(id, photos, collection);

            return View(LoadPhotosEditViewModel(id, photos));
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadPhoto(int id, HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var b = new BinaryReader(file.InputStream);
                    var f = b.ReadBytes(file.ContentLength);

                    _photos.AddPhoto(id, f);
                }
            }
            var photos = _photos.GetList(id);
            return View("Index", LoadPhotosEditViewModel(id, photos));
        }

        private void Delete(ICollection<Photo> photos, FormCollection collection)
        {
            Photo photo = null;

            foreach (var p in photos.Where(p => collection.GetValue("Delete_" + p.Id) != null))
            {
                _photos.Delete(p.Id);
                photo = p;
            }

            if (photo == null) return;
            photos.Remove(photo);
        }

        private void SetDefault(int id, IEnumerable<Photo> photos, FormCollection collection)
        {
            foreach (var p in photos.Where(p => collection.GetValue("SetDefault_" + p.Id) != null))
            {
                _photos.UpdateDefault(id, p.Id);
            }            
        }

        private PhotosEditViewModel LoadPhotosEditViewModel(int id, IEnumerable<Photo> photos)
        {
            var vm = new PhotosEditViewModel();
            var photolist = photos.ToList();
            var instrument = _instruments.GetSingle(id);

            vm.Photos = photolist;
            vm.ProductId = id;
            vm.DefaultPhotoId = _photos.GetDefaultId(id);
            vm.Model = instrument.Model + ' ' + instrument.Sn;

            return vm;
        }

        public FileResult GetPhoto(int id)
        {
            var photo = _photos.GetData(id);

            return File(photo, "image/jpeg");
        }
    }
}

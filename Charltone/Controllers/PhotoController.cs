using System;
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
        private readonly IPhotoRepository _photoRepository;

        public PhotoController(IInstrumentRepository instruments, IPhotoRepository photoRepository)
        {
            _instruments = instruments;
            _photoRepository = photoRepository;
        }

        [Authorize]
        public ActionResult Index(int id)
        {
            var photos = _photoRepository.GetList(id);

            return View(LoadPhotosEditViewModel(id, photos));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(int id, FormCollection collection, string actionType)
        {
            var photos = _photoRepository.GetList(id);

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

                    _photoRepository.AddPhoto(id, f);
                }
            }
            var photos = _photoRepository.GetList(id);
            return View("Index", LoadPhotosEditViewModel(id, photos));
        }

        private void Delete(ICollection<Photo> photos, FormCollection collection)
        {
            Photo photo = null;

            foreach (var p in photos.Where(p => collection.GetValue("Delete_" + p.Id) != null))
            {
                _photoRepository.Delete(p.Id);
                photo = p;
            }

            if (photo == null) return;
            photos.Remove(photo);
        }

        private void SetDefault(int id, IEnumerable<Photo> photos, FormCollection collection)
        {
            foreach (var p in photos.Where(p => collection.GetValue("SetDefault_" + p.Id) != null))
            {
                _photoRepository.UpdateDefault(id, p.Id);
            }            
        }

        private PhotosEditViewModel LoadPhotosEditViewModel(int id, IEnumerable<Photo> photos)
        {
            var vm = new PhotosEditViewModel();
            var photolist = photos.ToList();
            var instrument = _instruments.GetSingle(id);

            vm.Photos = photolist;
            vm.ProductId = id;
            vm.DefaultPhotoId = _photoRepository.GetDefaultId(id);
            vm.Model = instrument.Model + ' ' + instrument.Sn;

            return vm;
        }

        public FileResult GetPhoto(int id)
        {
            var photo = _photoRepository.GetData(id);

            return File(photo, "image/jpeg");
        }

        [HttpPost]
        public JsonResult GetPhotoJson(int id)
        {
            var photo = _photoRepository.GetData(id);
            var data = Convert.ToBase64String(photo);

            return Json(data);
        }

        public FileResult GetHomePageImage(int id)
        {
            var photo = _photoRepository.GetHomePageImageData(id);

            return File(photo, "image/jpeg");
        }
    }
}

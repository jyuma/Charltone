﻿using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Charltone.Data.Repositories;
using System.Linq;
using Charltone.UI.ViewModels.Photos;

namespace Charltone.UI.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IProductRepository _products;
        private readonly IPhotoRepository _photos;

        public PhotoController(IProductRepository products, IPhotoRepository photos)
        {
            _products = products;
            _photos = photos;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index(int id)
        {
            return View(LoadPhotosEditViewModel(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(int id, FormCollection collection)
        {
            var key = collection.Keys.Get(0);
            var delimiterIndex = key.IndexOf("_", StringComparison.Ordinal) + 1;
            var photoId = Convert.ToInt32(key.Substring(delimiterIndex, key.Length - delimiterIndex));

            var isDelete = collection.AllKeys.Select(x => x.StartsWith("Delete")).Single();
            var isSetDefault = collection.AllKeys.Select(x => x.StartsWith("SetDefault")).Single();

            if (isDelete)
            {
                _photos.Delete(photoId);
            }
            else if (isSetDefault)
            {
                _photos.SetProductDefault(id, photoId);
            }

            return View(LoadPhotosEditViewModel(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult UploadPhoto(int id, HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var b = new BinaryReader(file.InputStream);
                    var f = b.ReadBytes(file.ContentLength);

                    _photos.Add(id, f);
                }
            }

            return View("Index", LoadPhotosEditViewModel(id));
        }

        [HttpGet]
        public FileResult GetPhoto(int id)
        {
            var photo = id > 0
                ? _photos.GetData(id)
                : _photos.GetDefaultInstrumentImage();

            return File(photo, "image/jpeg");
        }

        [HttpGet]
        public JsonResult GetPhotoJson(int id)
        {
            var photo = _photos.GetData(id);
            var data = Convert.ToBase64String(photo);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult GetHomePageImage()
        {
            var photo = _photos.GetHomePageImage();

            return File(photo, "image/jpeg");
        }

        private PhotosEditViewModel LoadPhotosEditViewModel(int productid)
        {
            var vm = new PhotosEditViewModel();
           
            var product = _products.Get(productid);
            vm.PhotoIds = _photos.GetIdsByProductId(productid);
            vm.ProductId = productid;
            vm.DefaultPhotoId = _photos.GetDefaultId(productid);
            vm.Model = product.Instrument.Model + ' ' + product.Instrument.Sn;

            return vm;
        }
    }
}

using Charltone.Data.Repositories;
using Charltone.Domain.Entities;
using Charltone.UI.Constants;
using Charltone.UI.Extensions;
using Charltone.UI.ViewModels.Instrument;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Charltone.UI.Controllers
{
    [RoutePrefix("Instrument/{id}")]
    public class InstrumentController : Controller
    {
        private readonly IProductRepository _products;
        private readonly IInstrumentTypeRepository _instrumentTypes;
        private readonly IClassificationRepository _classifications;
        private readonly ISubClassificationRepository _subClassifications;
        private readonly IProductStatusRepository _productStatus;
        private readonly IPhotoRepository _photos;


        public InstrumentController(IProductRepository products, 
            IInstrumentTypeRepository instrumentTypes,
            IClassificationRepository classifications,
            ISubClassificationRepository subClassifications,
            IProductStatusRepository productStatus, 
            IPhotoRepository photos)
        {
            _products = products;
            _instrumentTypes = instrumentTypes;
            _classifications = classifications;
            _subClassifications = subClassifications;
            _productStatus = productStatus;
            _photos = photos;
        }

        public ActionResult Index()
        {
            return View(LoadInstrumentListViewModel());
        }

        [HttpGet]
        [Route("Detail")]
        public ActionResult Detail(int id)
        {
            return View(LoadInstrumentDetailViewModel(id));
        }

        [HttpPost]
        [Authorize]
        [Route("Upload")]
        public ActionResult Upload(int id)
        {
            var file = Request.Files[0];
            var product = _products.Get(id);
            var count = product.Photos.Count;

            var reader = new BinaryReader(file.InputStream);
            var data = reader.ReadBytes(file.ContentLength)
                .ByteArrayToImage()
                .CropInstrument()
                .ImageToByteArray();

            var photo = new Photo
                        {
                            IsDefault = (count == 0),
                            Data = data,
                            SortOrder = count + 1,
                            Product = product
                        };

            product.Photos.Add(photo);
            var newId = _photos.Add(photo);

            return Json( new { success = true, id = newId } );
        }

        [HttpGet]
        [Authorize]
        [Route("Edit")]
        public ActionResult Edit(int id)
        {
            return View(LoadInstrumentEditViewModel(id));
        }

        [HttpPost]
        [Authorize]
        [Route("Edit")]
        public ActionResult Edit(int id, InstrumentEditViewModel viewModel)
        {
            var product = _products.Get(id);

            UpdateProductInstrument(product, viewModel);

            return RedirectToAction("Index", "Instrument");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View(LoadInstrumentEditViewModel(-1));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(InstrumentEditViewModel viewModel)
        {
            AddInstrument(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        [Route("Delete")]
        public ActionResult Delete(int id)
        {
            return View(LoadInstrumentEditViewModel(id));
        }

        [HttpPost]
        [Authorize]
        [Route("Delete")]
        public ActionResult Delete(int id, InstrumentEditViewModel viewModel)
        {
            _products.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public void SetDefaultPhoto(int id, int photoId)
        {
            var product = _products.Get(id);
            var photo = product.Photos.Single(x => x.Id == photoId);
            _photos.SetProductDefault(id, photoId);
            photo.IsDefault = true;
        }

        [HttpPost]
        [Authorize]
        public void DeletePhoto(int id, int photoId)
        {
            var product = _products.Get(id);
            var photo = product.Photos.Single(x => x.Id == photoId);

            if (photo.IsDefault) return;

            _photos.Delete(photoId);
            product.Photos.Remove(photo);
            product.Photos.ResetSortOrder();

            foreach (var p in product.Photos)
            {
                _photos.Update(p);
            }
        }

        [HttpPost]
        [Authorize]
        public void MovePhoto(int id, int adjacentId, int sortOrder)
        {
            var photos = _photos.GetAll().Where(x => x.Id == id || x.Id == adjacentId).ToArray();
            var p = photos.Single(x => x.Id == id);
            var pAdjacent = photos.Single(x => x.Id == adjacentId);

            pAdjacent.SortOrder = p.SortOrder;
            p.SortOrder = sortOrder;

            _photos.Update(p);
            _photos.Update(pAdjacent);
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
        public FileResult GetListPhoto(int id)
        {
            var photo = id > 0
                ? _photos.GetData(id)
                : _photos.GetDefaultInstrumentImage();

            var data = photo.Crop(new Size(InstrumentListPhotoSize.Width, InstrumentListPhotoSize.Height));

            return File(data, "image/jpeg");
        }

        [HttpGet]
        public JsonResult GetDetailPhoto(int id)
        {
            var photo = _photos.GetData(id).Crop(new Size(InstrumentDetailPhotoSize.Width, InstrumentDetailPhotoSize.Height));
            var data = Convert.ToBase64String(photo);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult GetThumbnail(int id)
        {
            var photo = id > 0
                ? _photos.GetData(id)
                : _photos.GetDefaultInstrumentImage();

            var data = photo.Thumbnail(InstrumentThumbnailSize.Width, InstrumentThumbnailSize.Height);

            return File(data, "image/jpeg");
        }
 
        // For zooming
        [HttpGet]
        public JsonResult GetPhotoZoom(int id)
        {
            var photo = _photos.GetData(id);
            var data = Convert.ToBase64String(photo);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDefaultPhotoIds()
        {
            var ids = _photos.GetAll()
                .Where(x => x.IsDefault)
                .Select(x => new { x.Id }).ToArray();

            return Json(ids, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetInstrumentPhotos(int id)
        {
            var photos = _products.Get(id).Photos
                .OrderBy(x => x.SortOrder)
                .Select(x => new
                             {
                                 x.Id,
                                 x.IsDefault,
                             }).ToArray();

            return Json(photos, JsonRequestBehavior.AllowGet);
        }

        #region View Models

        private InstrumentListViewModel LoadInstrumentListViewModel()
        {
            var instruments = _products.GetInstrumentList(Request.IsAuthenticated);

            var sortedList = instruments
                    .Where(product => product.Instrument != null)
                    .OrderBy(product => product.Instrument.InstrumentType.SortOrder)
                    .ThenBy(product => product.Instrument.Classification.SortOrder)
                    .ThenBy(product => product.Instrument.SubClassification.SortOrder)
                    .ThenBy(product => product.Instrument.Model)
                    .ThenBy(product => product.Instrument.Sn);

            var vm = new InstrumentListViewModel
                     {
                         Instruments = sortedList
                             .Select(product => new InstrumentViewModel
                                    {
                                        Id = product.Instrument.Id,
                                        ProductId = product.Id,
                                        InstrumentType = product.Instrument.InstrumentType.InstrumentTypeDesc,
                                        Classification = product.Instrument.Classification.ClassificationDesc,
                                        SubClassification = product.Instrument.SubClassification.SubClassificationDesc,
                                        ModelSn = string.Format("{0} {1}", product.Instrument.Model, product.Instrument.Sn),
                                        Status = product.ProductStatus.StatusDesc,
                                        StatusCssClassId = Regex.Replace(product.ProductStatus.StatusDesc, @"\s", "").ToLower(),
                                        Price = product.DisplayPrice,
                                        ShowPrice = product.ProductStatus.Id == ProductStatusTypeId.Available,
                                        NotPostedMessage = product.IsPosted ? string.Empty : Messages.NotPostedText,
                                        DefaultPhotoId = product.GetDefaultPhotoId()
                                    }).ToList(),

                         Banner = string.Format("{0} currently available", string.Format("{0} instrument{1}", 
                            instruments.Where(x => x.ProductStatus.Id == ProductStatusTypeId.Available).ToArray().Count(),
                            instruments.Where(x => x.ProductStatus.Id == ProductStatusTypeId.Available).ToArray().Count() > 1 ? "s" : null))
                     };

            return vm;
        }

        private InstrumentDetailViewModel LoadInstrumentDetailViewModel(int productId)
        {
            var product = _products.Get(productId);
            var instrument = product.Instrument;

            var vm = new InstrumentDetailViewModel
                     {
                         Id = instrument.Id,
                         InstrumentType = instrument.InstrumentType.InstrumentTypeDesc,
                         ProductId = productId,
                         IsAuthenticated = Request.IsAuthenticated,
                         Classification = instrument.Classification.ClassificationDesc,
                         SubClassification = instrument.SubClassification.SubClassificationDesc,
                         ModelSn = string.Format("{0} {1}", instrument.Model, instrument.Sn),
                         Price = product.DisplayPrice,
                         Status = product.ProductStatus.StatusDesc,
                         StatusCssClassId = Regex.Replace(product.ProductStatus.StatusDesc, @"\s", "").ToLower(),
                         ShowPrice = product.ProductStatus.Id == ProductStatusTypeId.Available,
                         DefaultPhotoId = product.GetDefaultPhotoId(),
                         
                         Top = instrument.Top,
                         BackAndSides = instrument.BackAndSides,
                         Body = instrument.Body,
                         Binding = instrument.Binding,
                         Bridge = instrument.Bridge,
                         CaseDetail = instrument.CaseDetail,
                         Dimensions = instrument.Dimensions,
                         EdgeDots = instrument.EdgeDots,
                         Faceplate = instrument.Faceplate,
                         Finish = instrument.Finish,
                         Fingerboard = instrument.Fingerboard,
                         FretMarkers = instrument.FretMarkers,
                         FretWire = instrument.FretWire,
                         PickGuard = instrument.PickGuard,
                         Pickup = instrument.Pickup,
                         Neck = instrument.Neck,
                         NutWidth = instrument.NutWidth,
                         ScaleLength = instrument.ScaleLength,
                         Strings = instrument.Strings,
                         Tailpiece = instrument.Tailpiece,
                         Tuners = instrument.Tuners,
                         Comments = instrument.Comments,
                         FunFacts = instrument.FunFacts,

                         InstrumentPhotos = product.Photos
                            .OrderBy(x => x.SortOrder)
                            .Select(x => new InstrumentPhoto
                             {
                                Id = x.Id,
                                IsDefault = x.IsDefault,
                             }).ToArray(),
                         PhotoIds = product.Photos.Select(x => x.Id).ToArray()
                     };

            return vm;
        }

        private InstrumentEditViewModel LoadInstrumentEditViewModel(int productId)
        {
            var product = GetInstrumentProduct(productId);
            var instrument = product.Instrument;

            var vm = new InstrumentEditViewModel
                     {
                         Id = instrument.Id,
                         ProductId = productId,
                         Model = instrument.Model,
                         Sn = instrument.Sn,
                         Top = instrument.Top,

                         BackAndSides = instrument.BackAndSides,
                         Body = instrument.Body,
                         Binding = instrument.Binding,
                         Bridge = instrument.Bridge,
                         CaseDetail = instrument.CaseDetail,
                         Dimensions = instrument.Dimensions,
                         EdgeDots = instrument.EdgeDots,
                         Faceplate = instrument.Faceplate,
                         Finish = instrument.Finish,
                         Fingerboard = instrument.Fingerboard,
                         FretMarkers = instrument.FretMarkers,
                         FretWire = instrument.FretWire,
                         Neck = instrument.Neck,
                         NutWidth = instrument.NutWidth,
                         PickGuard = instrument.PickGuard,
                         Pickup = instrument.Pickup,
                         ScaleLength = instrument.ScaleLength,
                         Strings = instrument.Strings,
                         Tailpiece = instrument.Tailpiece,
                         Tuners = instrument.Tuners,

                         Comments = instrument.Comments,
                         FunFacts = instrument.FunFacts,

                         InstrumentTypes = new SelectList(_instrumentTypes.GetAll(), "Id", "InstrumentTypeDesc", instrument.InstrumentType.Id),
                         InstrumentTypeId = instrument.InstrumentType.Id,

                         ClassificationTypes = new SelectList(_classifications.GetAll(), "Id", "ClassificationDesc", instrument.Classification.Id),
                         ClassificationId = instrument.Classification.Id,

                         SubClassificationTypes = new SelectList(_subClassifications.GetAll(), "Id", "SubClassificationDesc", instrument.SubClassification.Id),
                         SubClassificationId = instrument.SubClassification.Id,

                         StatusTypes = new SelectList(_productStatus.GetAll(), "Id", "StatusDesc", product.ProductStatus.Id),
                         StatusId = product.ProductStatus.Id,

                         Price = product.Price,
                         DisplayPrice = product.DisplayPrice,
                         IsPosted = product.IsPosted,
                         DefaultPhotoId = product.GetDefaultPhotoId()
                     };

            return vm;
        }

        private Product GetInstrumentProduct(int productId)
        {
            return productId > 0
                ? _products.Get(productId)
                : new Product
                {
                    ProductStatus = new ProductStatus { Id = ProductStatusTypeId.NotForSale },
                    IsPosted = false,
                    Instrument = new Instrument
                    {
                        InstrumentType = new InstrumentType { Id = InstrumentTypeId.Guitar },
                        Classification = new Classification { Id = ClassificationTypeId.SteelString },
                        SubClassification = new SubClassification { Id = SubClassificationTypeId.Classical }
                    }
                };   
        }

        #endregion

        #region Update

        private void AddInstrument(InstrumentEditViewModel viewModel)
        {
            var product = new Product
            {
                ProductType = new ProductType { Id = 1 },
                Instrument = new Instrument
                {
                    InstrumentType = new InstrumentType { Id = viewModel.InstrumentTypeId },
                    Classification = new Classification { Id = viewModel.ClassificationId },
                    SubClassification = new SubClassification { Id = viewModel.SubClassificationId },

                    Top = viewModel.Top,
                    Body = viewModel.Body,
                    BackAndSides = viewModel.BackAndSides,
                    Binding = viewModel.Binding,
                    Bridge = viewModel.Bridge,
                    CaseDetail = viewModel.CaseDetail,
                    Comments = viewModel.Comments,
                    Dimensions = viewModel.Dimensions,
                    EdgeDots = viewModel.EdgeDots,
                    Faceplate = viewModel.Faceplate,
                    Fingerboard = viewModel.Fingerboard,
                    Finish = viewModel.Finish,
                    FretMarkers = viewModel.FretMarkers,
                    FretWire = viewModel.FretWire,
                    FunFacts = viewModel.FunFacts,
                    Model = viewModel.Model,
                    Neck = viewModel.Neck,
                    NutWidth = viewModel.NutWidth,
                    PickGuard = viewModel.PickGuard,
                    Pickup = viewModel.Pickup,
                    ScaleLength = viewModel.ScaleLength,
                    Sn = viewModel.Sn,
                    Tailpiece = viewModel.Tailpiece,
                    Tuners = viewModel.Tuners
                },
                ProductDesc = "Instrument",
                Price = viewModel.Price,
                DisplayPrice = viewModel.DisplayPrice,
                ProductStatus = new ProductStatus { Id = viewModel.StatusId },
                IsPosted = false
            };

            _products.Add(product);
        }

        private void UpdateProductInstrument(Product product, InstrumentEditViewModel viewModel)
        {
            var posted = viewModel.IsPosted;
            var statusid = viewModel.StatusId;
            if (product.ProductStatus.Id != statusid)
            {
                product.ProductStatus = _productStatus.GetAll().Single(x => x.Id == statusid);
            }
            product.ProductStatus.Id = viewModel.StatusId;
            product.IsPosted = posted;
            product.Price = viewModel.Price;
            product.DisplayPrice = viewModel.DisplayPrice;
            UpdateInstrument(product.Instrument, viewModel);
            _products.Update(product);
        }

        private void UpdateInstrument(Instrument instrument, InstrumentEditViewModel viewModel)
        {
            if (instrument.InstrumentType.Id != viewModel.InstrumentTypeId)
                instrument.InstrumentType = _instrumentTypes.GetAll().Single(x => x.Id == viewModel.InstrumentTypeId);

            if (instrument.Classification.Id != viewModel.ClassificationId)
                instrument.Classification = _classifications.GetAll().Single(x => x.Id == viewModel.ClassificationId);

            if (instrument.SubClassification.Id != viewModel.SubClassificationId)
                instrument.SubClassification = _subClassifications.GetAll().Single(x => x.Id == viewModel.SubClassificationId);

            instrument.Top = viewModel.Top;
            instrument.BackAndSides = viewModel.BackAndSides;
            instrument.Binding = viewModel.Binding;
            instrument.Body = viewModel.Body;
            instrument.Bridge = viewModel.Bridge;
            instrument.CaseDetail = viewModel.CaseDetail;
            instrument.Comments = viewModel.Comments;
            instrument.Dimensions = viewModel.Dimensions;
            instrument.EdgeDots = viewModel.EdgeDots;
            instrument.Faceplate = viewModel.Faceplate;
            instrument.Fingerboard = viewModel.Fingerboard;
            instrument.Finish = viewModel.Finish;
            instrument.FretMarkers = viewModel.FretMarkers;
            instrument.FretWire = viewModel.FretWire;
            instrument.FunFacts = viewModel.FunFacts;
            instrument.Model = viewModel.Model;
            instrument.Neck = viewModel.Neck;
            instrument.NutWidth = viewModel.NutWidth;
            instrument.PickGuard = viewModel.PickGuard;
            instrument.Pickup = viewModel.Pickup;
            instrument.ScaleLength = viewModel.ScaleLength;
            instrument.Sn = viewModel.Sn;
            instrument.Strings = viewModel.Strings;
            instrument.Tailpiece = viewModel.Tailpiece;
            instrument.Tuners = viewModel.Tuners;
        }

        #endregion
    }
}

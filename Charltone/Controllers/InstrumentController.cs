using System.IO;
using System.Web;
using Charltone.Data.Repositories;
using Charltone.Domain.Entities;
using Charltone.UI.ViewModels.Instrument;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Charltone.UI.ViewModels.Photo;

namespace Charltone.UI.Controllers
{
    public class InstrumentController : Controller
    {
        private readonly IProductRepository _products;
        private readonly IInstrumentTypeRepository _types;
        private readonly IPhotoRepository _photos;

        public InstrumentController(IProductRepository products, IInstrumentTypeRepository types, IPhotoRepository photos)
        {
            _products = products;
            _types = types;
            _photos = photos;
        }

        public ActionResult Index()
        {
            return View(LoadInstrumentListViewModel());
        }

        public ActionResult Detail(int id)
        {
            return View(LoadInstrumentDetailViewModel(id));
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View(LoadInstrumentEditViewModel(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, InstrumentEditViewModel viewModel)
        {
            var product = _products.Get(id);

            UpdateProductInstrument(product, viewModel);

            return  RedirectToAction("Index", LoadInstrumentListViewModel());
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
            var product = new Product
                              {
                                  ProductType = new ProductType {Id = 1},
                                  Instrument = new Instrument
                                                         {
                                                             InstrumentType = new InstrumentType {Id = viewModel.InstrumentTypeId},
                                                             Classification = new Classification {Id = viewModel.ClassificationId},
                                                             SubClassification = new SubClassification {Id = viewModel.SubClassificationId},
                                                             Model = viewModel.Model,
                                                             Sn = viewModel.Sn,
                                                             Top = viewModel.Top,
                                                             Body = viewModel.Body,
                                                             BackAndSides = viewModel.BackAndSides,
                                                             Binding = viewModel.Binding,
                                                             Neck = viewModel.Neck,
                                                             Faceplate = viewModel.Faceplate,
                                                             Fingerboard = viewModel.Fingerboard,
                                                             FretMarkers = viewModel.FretMarkers,
                                                             EdgeDots = viewModel.EdgeDots,
                                                             Bridge = viewModel.Bridge,
                                                             Finish = viewModel.Finish,
                                                             Tuners = viewModel.Tuners,
                                                             PickGuard = viewModel.PickGuard,
                                                             Pickup = viewModel.Pickup,
                                                             NutWidth = viewModel.NutWidth,
                                                             ScaleLength = viewModel.ScaleLength,
                                                             CaseDetail = viewModel.CaseDetail,
                                                             Comments = viewModel.Comments,
                                                             FunFacts = viewModel.FunFacts
                                                         },
                                  ProductDesc = "Instrument",
                                  Price = viewModel.Price,
                                  DisplayPrice = viewModel.DisplayPrice,
                                  ProductStatus = new ProductStatus {Id = viewModel.StatusId},
                                  IsPosted = false
                              };

            _products.Add(product);

            return RedirectToAction("Index", LoadInstrumentListViewModel());
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View(LoadInstrumentEditViewModel(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id, InstrumentEditViewModel viewModel)
        {
            _products.Delete(id);

            return RedirectToAction("Index", LoadInstrumentListViewModel());
        }

        [HttpGet]
        [Authorize]
        public ActionResult Photos(int id)
        {
            return View(LoadInstrumentPhotosEditViewModel(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Photos(int id, FormCollection collection)
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

            return View(LoadInstrumentPhotosEditViewModel(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult UploadPhoto(int id, HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var reader = new BinaryReader(file.InputStream);
                    var data = reader.ReadBytes(file.ContentLength);
                    var count = _photos.CountByProductId(id);

                    var photo = new Photo { IsDefault = (count == 0), ProductId = id, Data = data };
                    _photos.Add(photo);
                }
            }

            return View("Photos", LoadInstrumentPhotosEditViewModel(id));
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

        #region LoadViewModels

        private InstrumentPhotosEditViewModel LoadInstrumentPhotosEditViewModel(int productid)
        {
            var vm = new InstrumentPhotosEditViewModel();

            var product = _products.Get(productid);
            vm.PhotoIds = _photos.GetIdsByProductId(productid);
            vm.ProductId = productid;
            vm.DefaultPhotoId = _photos.GetDefaultId(productid);
            vm.Model = product.Instrument.Model + ' ' + product.Instrument.Sn;

            return vm;
        }

        private InstrumentListViewModel LoadInstrumentListViewModel()
        {
            var products = _products.GetInstrumentList(Request.IsAuthenticated);
            var totalitems = _products.InstrumentCount(Request.IsAuthenticated);

            var vm = new InstrumentListViewModel
                     {
                         TotalItemsCount = totalitems
                     };

            var sortedInstruments = products
                    .Where(product => product.Instrument != null)
                    .OrderBy(product => product.Instrument.InstrumentType.SortOrder)
                    .ThenBy(product => product.Instrument.Classification.SortOrder)
                    .ThenBy(product => product.Instrument.SubClassification.SortOrder)
                    .ThenBy(product => product.Instrument.Model)
                    .ThenBy(product => product.Instrument.Sn);

            var instruments = (from product in sortedInstruments
                let instrument = product.Instrument
                select new InstrumentViewModel
                       {
                           Id = instrument.Id, ProductId = product.Id, InstrumentType = instrument.InstrumentType.InstrumentTypeDesc, Classification = instrument.Classification.ClassificationDesc, SubClassification = instrument.SubClassification.SubClassificationDesc, Model = instrument.Model + ' ' + instrument.Sn, InstrumentStatusPrice = (product.ProductStatus.Id == Constants.ProductStatusTypeId.Available ? product.DisplayPrice : product.ProductStatus.StatusDesc), NotPostedMessage = product.IsPosted ? "" : "NOT POSTED", DefaultPhotoId = _photos.GetDefaultId(product.Id)
                       }).ToList();

            vm.Instruments = instruments;

            //--- construct the banner message
            if (totalitems == 1) vm.Banner = "1 instrument";
            else vm.Banner = totalitems + " instruments";
            vm.Banner += " currently in stock";

            //--- get the row count (in groups of 3)
            vm.RowCount = totalitems <= 3 ? 1 : Convert.ToInt32(totalitems / 3);
            if ((totalitems > 3) && (totalitems % 3 != 0)) vm.RowCount++;

            //--- calculate the pixel height for the background image
            var rowheight = Convert.ToInt32(ConfigurationManager.AppSettings["InstrumentListRowHeight"]);
            var menuheight = Convert.ToInt32(ConfigurationManager.AppSettings["MenuContainerHeight"]);

            vm.BackgroundImageHeight = (rowheight * vm.RowCount) + menuheight + "px;";

            return vm;
        }

        private InstrumentDetailViewModel LoadInstrumentDetailViewModel(int productId)
        {
            var product = _products.Get(productId);
            var instrument = product.Instrument;
            var photoIds = _photos.GetIdsByProductId(productId);

            var vm = new InstrumentDetailViewModel
                     {
                         Id = instrument.Id,
                         InstrumentType = instrument.InstrumentType.InstrumentTypeDesc,
                         Classification = instrument.Classification.ClassificationDesc,
                         SubClassification = instrument.SubClassification.SubClassificationDesc,
                         Model = instrument.Model + " " + instrument.Sn,
                         Top = instrument.Top,
                         BackAndSides = instrument.BackAndSides,
                         Body = instrument.Body,
                         Binding = instrument.Binding,
                         Neck = instrument.Neck,
                         Faceplate = instrument.Faceplate,
                         Fingerboard = instrument.Fingerboard,
                         FretMarkers = instrument.FretMarkers,
                         EdgeDots = instrument.EdgeDots,
                         Bridge = instrument.Bridge,
                         Finish = instrument.Finish,
                         Tuners = instrument.Tuners,
                         PickGuard = instrument.PickGuard,
                         Pickup = instrument.Pickup,
                         NutWidth = instrument.NutWidth,
                         ScaleLength = instrument.ScaleLength,
                         Comments = instrument.Comments,
                         CaseDetail = instrument.CaseDetail,
                         Strings = instrument.Strings,
                         FunFacts = instrument.FunFacts,
                         Price = product.DisplayPrice,
                         InstrumentStatus = product.ProductStatus.StatusDesc,
                         DefaultPhotoId = _photos.GetDefaultId(productId),
                         PhotoIds = photoIds
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
                         Neck = instrument.Neck,
                         Faceplate = instrument.Faceplate,
                         Fingerboard = instrument.Fingerboard,
                         FretMarkers = instrument.FretMarkers,
                         EdgeDots = instrument.EdgeDots,
                         Bridge = instrument.Bridge,
                         Finish = instrument.Finish,
                         Tuners = instrument.Tuners,
                         PickGuard = instrument.PickGuard,
                         Pickup = instrument.Pickup,
                         NutWidth = instrument.NutWidth,
                         ScaleLength = instrument.ScaleLength,
                         FunFacts = instrument.FunFacts,
                         Comments = instrument.Comments,
                         CaseDetail = instrument.CaseDetail,
                         Strings = instrument.Strings,

                         InstrumentTypes = new SelectList(_types.GetInstrumentTypeList(), "Id", "InstrumentTypeDesc", instrument.InstrumentType.Id),
                         InstrumentTypeId = instrument.InstrumentType.Id,

                         ClassificationTypes = new SelectList(_types.GetClassificationList(), "Id", "ClassificationDesc", instrument.Classification.Id),
                         ClassificationId = instrument.Classification.Id,

                         SubClassificationTypes = new SelectList(_types.GetSubClassificationList(), "Id", "SubClassificationDesc", instrument.SubClassification.Id),
                         SubClassificationId = instrument.SubClassification.Id,

                         StatusTypes = new SelectList(_types.GetProductStatusList(), "Id", "StatusDesc", product.ProductStatus.Id),
                         StatusId = product.ProductStatus.Id,

                         Price = product.Price,
                         DisplayPrice = product.DisplayPrice,
                         IsPosted = product.IsPosted,
                         DefaultPhotoId = _photos.GetDefaultId(product.Id)
                     };

            return vm;
        }

        private Product GetInstrumentProduct(int productId)
        {
            return productId > 0
                ? _products.Get(productId)
                : new Product
                {
                    ProductStatus = new ProductStatus { Id = Constants.ProductStatusTypeId.NotForSale },
                    IsPosted = false,
                    Instrument = new Instrument
                    {
                        InstrumentType = new InstrumentType { Id = Constants.InstrumentTypeId.Guitar },
                        Classification = new Classification { Id = Constants.ClassificationTypeId.SteelString },
                        SubClassification = new SubClassification { Id = Constants.SubClassificationTypeId.Classical }
                    }
                };   
        }

        #endregion

        private void UpdateProductInstrument(Product product, InstrumentEditViewModel viewModel)
        {
            var posted = viewModel.IsPosted;
            var statusid = viewModel.StatusId;

            if (product.ProductStatus.Id != statusid)
            {
                product.ProductStatus = _types.GetProductStatus(statusid);
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
                instrument.InstrumentType = _types.GetInstrumentType(viewModel.InstrumentTypeId);

            if (instrument.Classification.Id != viewModel.ClassificationId)
                instrument.Classification = _types.GetClassification(viewModel.ClassificationId);

            if (instrument.SubClassification.Id != viewModel.SubClassificationId)
                instrument.SubClassification = _types.GetSubClassification(viewModel.SubClassificationId);

            instrument.BackAndSides = viewModel.BackAndSides;
            instrument.Binding = viewModel.Binding;
            instrument.Body = viewModel.Body;
            instrument.Bridge = viewModel.Bridge;
            instrument.CaseDetail = viewModel.CaseDetail;
            instrument.Comments = viewModel.Comments;
            instrument.EdgeDots = viewModel.EdgeDots;
            instrument.Faceplate = viewModel.Faceplate;
            instrument.Fingerboard = viewModel.Fingerboard;
            instrument.Finish = viewModel.Finish;
            instrument.FretMarkers = viewModel.FretMarkers;
            instrument.FunFacts = viewModel.FunFacts;
            instrument.Model = viewModel.Model;
            instrument.Neck = viewModel.Neck;
            instrument.NutWidth = viewModel.NutWidth;
            instrument.PickGuard = viewModel.PickGuard;
            instrument.Pickup = viewModel.Pickup;
            instrument.ScaleLength = viewModel.ScaleLength;
            instrument.Sn = viewModel.Sn;
            instrument.Strings = viewModel.Strings;
            instrument.Top = viewModel.Top;
            instrument.Tuners = viewModel.Tuners;
        }
    }
}

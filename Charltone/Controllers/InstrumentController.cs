using System;
using System.Configuration;
using Charltone.Data.Repositories;
using Charltone.Domain.Entities;
using Charltone.UI.ViewModels.Instruments;
using System.Linq;
using System.Web.Mvc;

namespace Charltone.UI.Controllers
{
    public class InstrumentController : Controller
    {
        private readonly IProductRepository _products;
        private readonly IPhotoRepository _photos;
        private readonly IInstrumentTypeRepository _types;
        private readonly IProductRepository _productRepository;

        public InstrumentController(IProductRepository products, IInstrumentTypeRepository types, IPhotoRepository photos, IProductRepository productRepository)
        {
            _products = products;
            _types = types;
            _photos = photos;
            _productRepository = productRepository;
        }

        public ActionResult Index()
        {
            return View(LoadInstrumentListViewModel());
        }

        public ActionResult Detail(int id)
        {
            return View(LoadInstrumentDetailViewModel(id));
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(LoadInstrumentEditViewModel(id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, InstrumentEditViewModel viewModel)
        {
            _productRepository.Delete(id);

            return RedirectToAction("Index", LoadInstrumentListViewModel());
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            return View(LoadInstrumentEditViewModel(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, InstrumentEditViewModel viewModel)
        {
            var product = _products.Get(id);

            UpdateInstrument(product.Instrument, viewModel);
            UpdateProduct(product, viewModel);

            _products.Update(product);

            return  RedirectToAction("Index", LoadInstrumentListViewModel());
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View(LoadInstrumentEditViewModel(0));
        }

        [Authorize]
        [HttpPost]
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

            _productRepository.Add(product);

            return RedirectToAction("Index", LoadInstrumentListViewModel());
        }

        [HttpGet]
        public FileResult GetPhoto(int id)
        {
            var photo = _photos.GetData(id);

            return File(photo, "image/jpeg");
        }

        private void UpdateProduct(Product product, InstrumentEditViewModel viewModel)
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
        }

        private void UpdateInstrument(Instrument instrument, InstrumentEditViewModel viewModel)
        {
            //--- update the instrument
            instrument.InstrumentType = _types.GetSingleInstrumentType(viewModel.InstrumentTypeId);
            instrument.Classification = _types.GetSingleClassification(viewModel.ClassificationId);
            instrument.SubClassification = _types.GetSingleSubClassification(viewModel.SubClassificationId);
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

        private InstrumentListViewModel<InstrumentInfo> LoadInstrumentListViewModel()
        {
            var products = _products.GetInstrumentList(Request.IsAuthenticated);

            var vm = new InstrumentListViewModel<InstrumentInfo>();
            var sortedlist = products.OrderBy(l => l.Instrument.InstrumentType.SortOrder)
                                    .ThenBy(l => l.Instrument.Classification.SortOrder)
                                    .ThenBy(l => l.Instrument.SubClassification.SortOrder)
                                    .ThenBy(l => l.Instrument.Model)
                                    .ThenBy(l => l.Instrument.Sn);

            var totalitems = _products.InstrumentCount(Request.IsAuthenticated);
            vm.TotalItemsCount = totalitems;

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

            //--- add info for each instrument
            foreach (var info in sortedlist.Select(p => new InstrumentInfo(p, _photos.GetDefaultId(p.Id)))
                .Where(info => vm.InstrumentInfo != null))
            {
                vm.InstrumentInfo.Add(info);
            }

            return vm;
        }

        private InstrumentDetailViewModel LoadInstrumentDetailViewModel(int productId)
        {
            var product = _products.Get(productId);
            var vm = new InstrumentDetailViewModel(product);
            var photoIds = _photos.GetIdList(productId);

            vm.DefaultPhotoId = _photos.GetDefaultId(productId);
            vm.PhotoIds = photoIds;

            return vm;
        }

        private InstrumentEditViewModel LoadInstrumentEditViewModel(int productId)
        {
            var product = productId > 0 
                ? _products.Get(productId) 
                : new Product
                  {
                      ProductStatus = new ProductStatus { Id = Constants.ProductStatusTypeId.NotForSale },
                      IsPosted = false,
                      Instrument = new Instrument 
                      { 
                          InstrumentType = new InstrumentType { Id = Constants.InstrumentTypeId.Guitar },
                          Classification = new Classification { Id = Constants.ClassificationTypeId.SteelString },
                          SubClassification = new SubClassification{ Id = Constants.SubClassificationTypeId.Classical }
                      }
                  };   
            
            var vm = new InstrumentEditViewModel(product, _types);
            var photos = _photos.GetList(product.Id);
            var photolist = photos.ToList();

            vm.DefaultPhotoId = _photos.GetDefaultId(product.Id);
            vm.Photos = photolist;

            return vm;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using Charltone.Domain;
using Charltone.ViewModels.Instruments;
using System.Linq;

namespace Charltone.Controllers
{
    using System.Web.Mvc;
    using Repositories;

    public class InstrumentController : Controller
    {
        private readonly IProductRepository _products;
        private readonly IInstrumentRepository _instruments;
        private readonly IPhotoRepository _photos;
        private readonly ITypeRepository _types;
        private readonly IRepository<Product> _repository;

        public InstrumentController(IProductRepository products, IInstrumentRepository instruments, ITypeRepository types, IPhotoRepository photos, IRepository<Product> repository)
        {
            _products = products;
            _instruments = instruments;
            _types = types;
            _photos = photos;
            _repository = repository;
        }

        public ActionResult Index(int? page)
        {
            var products = _products.GetInstrumentList(page.GetValueOrDefault(1), Request.IsAuthenticated);
            return View(LoadInstrumentListViewModel(products));
        }

        public ActionResult Detail(int id)
        {
            var instrument = _instruments.GetSingle(id);

            return View(LoadInstrumentDetailViewModel(instrument));
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var instrument = _instruments.GetSingle(id);

            return View(LoadInstrumentEditViewModel(instrument));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, InstrumentEditViewModel viewModel)
        {
            var instrument = _instruments.GetSingle(id);
            var product = instrument.Product;

            //_products.Delete(product);
            _repository.Delete(product);
            var products = _products.GetInstrumentList(1, Request.IsAuthenticated);

            return RedirectToAction("Index", LoadInstrumentListViewModel(products));
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var instrument = _instruments.GetSingle(id);
            return View(LoadInstrumentEditViewModel(instrument));
        }

        [HttpPost]
        public ActionResult Edit(int id, InstrumentEditViewModel viewModel)
        {
            var instrument = _instruments.GetSingle(id);

            UpdateInstrument(instrument, viewModel);
            var products = _products.GetInstrumentList(1, Request.IsAuthenticated);

            return  RedirectToAction("Index", LoadInstrumentListViewModel(products));
        }

        [Authorize]
        public ActionResult Create()
        {
            var instrument = new Instrument();

            return View(LoadInstrumentEditViewModel(instrument));
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
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
            _repository.Save(product);
            //_products.Save(product);

            var products = _products.GetInstrumentList(1, Request.IsAuthenticated);
            return RedirectToAction("Index", LoadInstrumentListViewModel(products));
        }

        private void UpdateInstrument(Instrument instrument, InstrumentEditViewModel viewModel)
        {
            //--- update the flat data
            UpdateModel(instrument);

            //--- update the referenced objects
            if (instrument.InstrumentType.Id != viewModel.InstrumentTypeId)
                instrument.InstrumentType = _types.GetSingleInstrumentType(viewModel.InstrumentTypeId);
            if (instrument.Classification.Id != viewModel.ClassificationId)
                instrument.Classification = _types.GetSingleClassification(viewModel.ClassificationId);
            if (instrument.SubClassification.Id != viewModel.SubClassificationId)
                instrument.SubClassification = _types.GetSingleSubClassification(viewModel.SubClassificationId);

            //--- update the parent product
            UpdateProduct(instrument.Product, viewModel);

            //--- commit changes
            //_repository.Update(instrument);
            _instruments.Update(instrument);
        }

        private InstrumentListViewModel<InstrumentInfo> LoadInstrumentListViewModel(IEnumerable<Product> products)
        {
            var vm = new InstrumentListViewModel<InstrumentInfo>();
            var sortedlist = products.OrderBy(l => l.Instrument.InstrumentType.SortOrder)
                                    .ThenBy(l => l.Instrument.Classification.SortOrder)
                                    .ThenBy(l => l.Instrument.SubClassification.SortOrder)
                                    .ThenBy(l => l.Instrument.Model)
                                    .ThenBy(l => l.Instrument.Sn);

            var totalitems = _instruments.Count(Request.IsAuthenticated);
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
            foreach (var info in sortedlist.Select(i => new InstrumentInfo(i.Instrument, _photos.GetDefaultId(i.Id))).Where(info => vm.InstrumentInfo != null))
            {
                vm.InstrumentInfo.Add(info);
            }

            return vm;
        }

        private void UpdateProduct(Product product, InstrumentEditViewModel viewModel)
        {
            var posted = viewModel.IsPosted;
            var statusid = viewModel.StatusId;

            if (product.ProductStatus.Id != statusid) product.ProductStatus = _types.GetSingleProductStatus(statusid);
            if (product.IsPosted != posted) product.IsPosted = posted;
            if (product.Price != viewModel.Price) product.Price = viewModel.Price;
            if (product.DisplayPrice != viewModel.DisplayPrice) product.DisplayPrice = viewModel.DisplayPrice;
        }

        private InstrumentDetailViewModel LoadInstrumentDetailViewModel(Instrument instrument)
        {
            var vm = new InstrumentDetailViewModel(instrument);
            var photoIds = _photos.GetIdList(instrument.Id);

            vm.DefaultPhotoId = _photos.GetDefaultId(instrument.Id);
            vm.PhotoIds = photoIds;

            return vm;
        }

        private InstrumentEditViewModel LoadInstrumentEditViewModel(Instrument instrument)
        {
            var vm = new InstrumentEditViewModel(instrument, _types, _repository);
            var photos = _photos.GetList(instrument.Id);
            var photolist = photos.ToList();

            vm.DefaultPhotoId = _photos.GetDefaultId(instrument.Id);
            vm.Photos = photolist;

            return vm;
        }

        public FileResult GetPhoto(int id)
        {
            var photo = _photos.GetData(id);

            return File(photo, "image/jpeg");
        }
    }
}

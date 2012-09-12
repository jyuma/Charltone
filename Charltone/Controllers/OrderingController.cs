using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using Charltone.Domain;
using Charltone.ViewModels.Orderings;
using System.Linq;

namespace Charltone.Controllers
{
    using System.Web.Mvc;
    using Repositories;

    public class OrderingController : Controller
    {
        private readonly IOrderingRepository _orderings;
        private readonly IPhotoRepository _photos;
        private readonly ITypeRepository _types;

        public OrderingController(IOrderingRepository orderings, ITypeRepository types, IPhotoRepository photos)
        {
            _orderings = orderings;
            _photos = photos;
            _types = types;
        }

        public ActionResult Index(int? page)
        {
            var orderings = _orderings.GetList(page.GetValueOrDefault(1));
            return View(LoadOrderingListViewModel(orderings));
        }

        private OrderingListViewModel<OrderingInfo> LoadOrderingListViewModel(IEnumerable<Ordering> orderings)
        {
            var vm = new OrderingListViewModel<OrderingInfo>();
            var sortedlist = orderings.OrderBy(l => l.Classification.SortOrder)
                .ThenBy(l => l.SubClassification.SortOrder)
                .ThenBy(l => l.Model);

            var totalitems = _orderings.Count();
            vm.TotalItemsCount = totalitems;
            vm.RowCount = totalitems;
            vm.Banner = "Ordering";

            //--- calculate the pixel height for the background image
            var rowheight = Convert.ToInt32(ConfigurationManager.AppSettings["OrderingListRowHeight"]);
            var menuheight = Convert.ToInt32(ConfigurationManager.AppSettings["MenuContainerHeight"]);

            vm.BackgroundImageHeight = (rowheight * totalitems) + menuheight + "px;";

            //--- add info for each ordering
            foreach (var info in sortedlist.Select(i => new OrderingInfo(i)))
            {
                vm.OrderingInfo.Add(info);
            }

            return vm;
        }

        private OrderingEditViewModel LoadOrderingEditViewModel(Ordering ordering)
        {
            var vm = new OrderingEditViewModel(ordering, _types);

            return vm;
        }

        //[Authorize]
        //public ActionResult Delete(int id)
        //{
        //    var ordering = _orderings.GetSingle(id);

        //    return View(LoadOrderingEditViewModel(ordering));
        //}

        //[Authorize]
        //[HttpPost]
        //public ActionResult Delete(int id, OrderingEditViewModel viewModel)
        //{
        //    var ordering = _orderings.GetSingle(id);
        //    var product = ordering.Product;

        //    _products.Delete(product);
        //    var products = _products.GetOrderingList(1, Request.IsAuthenticated);

        //    return RedirectToAction("Index", LoadOrderingListViewModel(products));
        //}

        [Authorize]
        public ActionResult Edit(int id)
        {
            var ordering = _orderings.GetSingle(id);
            return View(LoadOrderingEditViewModel(ordering));
        }

        [HttpPost]
        public ActionResult Edit(int id, OrderingEditViewModel viewModel)
        {
            var ordering = _orderings.GetSingle(id);

            UpdateOrdering(ordering, viewModel);
            var orderings = _orderings.GetList(1);

            return RedirectToAction("Index", LoadOrderingListViewModel(orderings));
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadPhoto(int id, HttpPostedFileBase file)
        {
            var ordering = _orderings.GetSingle(id);

            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var b = new BinaryReader(file.InputStream);
                    var f = b.ReadBytes(file.ContentLength);
                    ordering.Photo = f;
                    _orderings.Update(ordering);
                }
            }

            return View("Edit", LoadOrderingEditViewModel(ordering));
        }

        [Authorize]
        public ActionResult Create()
        {
            var ordering = new Ordering();

            return View(LoadOrderingEditViewModel(ordering));
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(OrderingEditViewModel viewModel)
        {
            var ordering = new Ordering
                {
                    InstrumentType = new InstrumentType { Id = viewModel.InstrumentTypeId },
                    Classification = new Classification { Id = viewModel.ClassificationId },
                    SubClassification = new SubClassification { Id = viewModel.SubClassificationId },
                    Model = viewModel.Model,
                    TypicalPrice = viewModel.TypicalPrice,
                    Comments = viewModel.Comments,
                };
            _orderings.Save(ordering);

            var orderings = _orderings.GetList(1);
            return RedirectToAction("Index", LoadOrderingListViewModel(orderings));
        }

        private void UpdateOrdering(Ordering ordering, OrderingEditViewModel viewModel)
        {
            //--- update the flat data
            UpdateModel(ordering);

            //--- update the referenced objects
            if (ordering.InstrumentType.Id != viewModel.InstrumentTypeId)
                ordering.InstrumentType = _types.GetSingleInstrumentType(viewModel.InstrumentTypeId);
            if (ordering.Classification.Id != viewModel.ClassificationId)
                ordering.Classification = _types.GetSingleClassification(viewModel.ClassificationId);
            if (ordering.SubClassification.Id != viewModel.SubClassificationId)
                ordering.SubClassification = _types.GetSingleSubClassification(viewModel.SubClassificationId);

            //--- commit changes
            _orderings.Update(ordering);
        }

        public FileResult GetPhoto(int id)
        {
            byte[] photo = _orderings.GetPhoto(id) ?? _photos.GetData(-1);

            return File(photo, "image/jpeg");
        }
    }
}

using Charltone.Data.Repositories;
using Charltone.Domain.Entities;
using Charltone.UI.Constants;
using Charltone.UI.ViewModels.Ordering;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Charltone.UI.ViewModels.Photo;

namespace Charltone.UI.Controllers
{
    [RoutePrefix("Ordering/{id}")]
    public class OrderingController : Controller
    {
        private readonly IOrderingRepository _orderings;
        private readonly IInstrumentTypeRepository _types;
        private readonly IPhotoRepository _photos;

        public OrderingController(IOrderingRepository orderings, IInstrumentTypeRepository types, IPhotoRepository photos)
        {
            _orderings = orderings;
            _photos = photos;
            _types = types;
        }

        public ActionResult Index()
        {
            return View(LoadOrderingListViewModel());
        }

        [HttpGet]
        [Authorize]
        [Route("Edit")]
        public ActionResult Edit(int id)
        {
            return View(LoadOrderingEditViewModel(id));
        }

        [HttpPost]
        [Authorize]
        [Route("Edit")]
        public ActionResult Edit(int id, OrderingEditViewModel viewModel)
        {
            UpdateOrdering(id, viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        [Route("Create")]
        public ActionResult Create()
        {
            return View(LoadOrderingEditViewModel(-1));
        }

        [HttpPost]
        [Authorize]
        [Route("Create")]
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

            _orderings.Add(ordering);

            return RedirectToAction("Index", LoadOrderingListViewModel());
        }

        [HttpGet]
        [Authorize]
        [Route("Delete")]
        public ActionResult Delete(int id)
        {
            return View(LoadOrderingEditViewModel(id));
        }

        [HttpPost]
        [Authorize]
        [Route("Delete")]
        public ActionResult Delete(int id, OrderingEditViewModel viewModel)
        {
            _orderings.Delete(id);

            return RedirectToAction("Index", LoadOrderingListViewModel());
        }

        [HttpPost]
        [Authorize]
        [Route("Photo")]
        public ActionResult Photo(int id, HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var reader = new BinaryReader(file.InputStream);
                    var data = reader.ReadBytes(file.ContentLength);
                    _orderings.UpdatePhoto(id, data);
                }
            }

            return View("Photo", LoadOrderingPhotoEditViewModel(id));
        }

        [HttpGet]
        [Authorize]
        [Route("Photo")]
        public ActionResult Photo(int id)
        {
            return View(LoadOrderingPhotoEditViewModel(id));
        }

        [HttpGet]
        public FileResult GetPhoto(int id)
        {
            byte[] photo;

            if (id == 0)  // New Ordering
            {
                photo = _photos.GetDefaultInstrumentImage();
            }
            else          // Existing Ordering
            {
                photo = _orderings.GetPhoto(id) ?? _photos.GetDefaultInstrumentImage();
            }

            return File(photo, "image/jpeg");
        }

        private OrderingPhotoEditViewModel LoadOrderingPhotoEditViewModel(int ordingId)
        {
            var ordering = _orderings.Get(ordingId);
            var vm = new OrderingPhotoEditViewModel
                     {
                         OrderingId = ordingId,
                         Photo = _orderings.GetPhoto(ordingId),
                         Model = ordering.Model
                     };

            return vm;
        }

        private OrderingListViewModel LoadOrderingListViewModel()
        {
            var orderings = _orderings.GetAll();
            var totalitems = orderings.Count();

            var vm = new OrderingListViewModel
                     {
                         TotalItemsCount = totalitems,
                         RowCount = totalitems,
                         Banner = "Ordering"
                     };

            //TODO: figure out how to do this in CSS
            var rowheight = Convert.ToInt32(ConfigurationManager.AppSettings["OrderingListRowHeight"]);
            var menuheight = Convert.ToInt32(ConfigurationManager.AppSettings["MenuContainerHeight"]);

            vm.BackgroundImageHeight = (rowheight * totalitems) + menuheight + "px;";

            var sortedOrderings = orderings
                .OrderBy(ordering => ordering.Classification.SortOrder)
                .ThenBy(ordering => ordering.SubClassification.SortOrder)
                .ThenBy(ordering => ordering.Model);

            foreach (var ordering in sortedOrderings)
            {
                vm.OrderingInfo.Add(
                    new OrderingInfo
                    {
                        Id = ordering.Id,
                        InstrumentType = ordering.InstrumentType.InstrumentTypeDesc,
                        Style = ordering.Classification.ClassificationDesc + " / " + ordering.SubClassification.SubClassificationDesc,
                        Model = ordering.Model,
                        TypicalPrice = ordering.TypicalPrice,
                        Comments = ordering.Comments,
                        Photo = ordering.Photo
                    });
            }

            return vm;
        }

        private OrderingEditViewModel LoadOrderingEditViewModel(int orderingId)
        {
            var ordering = orderingId > 0
            ? _orderings.Get(orderingId)
            : new Ordering
              {
                  Classification = new Classification { Id = ClassificationTypeId.SteelString },
                  SubClassification = new SubClassification { Id = SubClassificationTypeId.Classical },
                  InstrumentType = new InstrumentType { Id = InstrumentTypeId.Guitar }
              };

            var vm = new OrderingEditViewModel
            {
                Id = ordering.Id,
                Model = ordering.Model,
                TypicalPrice = ordering.TypicalPrice,
                Comments = ordering.Comments,

                InstrumentTypes = new SelectList(_types.GetInstrumentTypeList(), "Id", "InstrumentTypeDesc", ordering.InstrumentType.Id),
                InstrumentTypeId = ordering.InstrumentType.Id,

                ClassificationTypes = new SelectList(_types.GetClassificationList(), "Id", "ClassificationDesc", ordering.Classification.Id),
                ClassificationId = ordering.Classification.Id,

                SubClassificationTypes = new SelectList(_types.GetSubClassificationList(), "Id", "SubClassificationDesc", ordering.SubClassification.Id),
                SubClassificationId = ordering.SubClassification.Id
            };

            return vm;
        }

        private void UpdateOrdering(int orderingId, OrderingEditViewModel viewModel)
        {
            var ordering = _orderings.Get(orderingId);

            if (ordering.InstrumentType.Id != viewModel.InstrumentTypeId)
                ordering.InstrumentType = _types.GetInstrumentType(viewModel.InstrumentTypeId);

            if (ordering.Classification.Id != viewModel.ClassificationId)
                ordering.Classification = _types.GetClassification(viewModel.ClassificationId);

            if (ordering.SubClassification.Id != viewModel.SubClassificationId)
                ordering.SubClassification = _types.GetSubClassification(viewModel.SubClassificationId);

            ordering.Comments = viewModel.Comments;
            ordering.Model = viewModel.Model;
            ordering.TypicalPrice = viewModel.TypicalPrice;

            _orderings.Update(ordering);
        }
    }
}

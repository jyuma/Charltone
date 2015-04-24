using Charltone.Data.Repositories;
using Charltone.Domain.Entities;
using Charltone.UI.Constants;
using Charltone.UI.Extensions;
using Charltone.UI.ViewModels.Ordering;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Charltone.UI.Controllers
{
    [RoutePrefix("Ordering/{id}")]
    public class OrderingController : Controller
    {
        private readonly IOrderingRepository _orderings;
        private readonly IOrderingHeaderContentRepository _headerContent;
        private readonly IInstrumentTypeRepository _instrumentTypes;
        private readonly IClassificationRepository _classifications;
        private readonly ISubClassificationRepository _subClassifications;
        private readonly IPhotoRepository _photos;

        public OrderingController(IOrderingRepository orderings, 
            IOrderingHeaderContentRepository headerContent, 
            IInstrumentTypeRepository instrumentTypes,
            IClassificationRepository classifications,
            ISubClassificationRepository subClassifications,
            IPhotoRepository photos)
        {
            _orderings = orderings;
            _headerContent = headerContent;
            _photos = photos;
            _instrumentTypes = instrumentTypes;
            _classifications = classifications;
            _subClassifications = subClassifications;
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
        public ActionResult Create()
        {
            return View(LoadOrderingEditViewModel(-1));
        }

        [HttpPost]
        [Authorize]
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

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        [Route("Photo")]
        public ActionResult Photo(int id, OrderingPhotoEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(LoadOrderingPhotoEditViewModel(id));
            if (viewModel == null || viewModel.File == null || viewModel.File.ContentLength <= 0) return View(LoadOrderingPhotoEditViewModel(id));

            var reader = new BinaryReader(viewModel.File.InputStream);
            var data = reader.ReadBytes(viewModel.File.ContentLength)
                .ByteArrayToImage()
                .CropOrdering()
                .ImageToByteArray();

            _orderings.UpdatePhoto(id, data);

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
        [Authorize]
        public ActionResult EditHeader()
        {
            return View(LoadOrderingHeaderEditViewModel());
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditHeader(OrderingHeaderEditViewModel viewModel)
        {
            UpdateOrderingHeaderContent(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public FileResult GetPhoto(int id)
        {
            var photo = LoadPhoto(id);

            return File(photo, "image/jpeg");
        }

        [HttpGet]
        public FileResult GetThumbnail(int id)
        {

            var photo = LoadPhoto(id);

            var thumbnail = photo
                .ByteArrayToImage()
                .GetThumbnailImage(OrderingThumbnail.Width, OrderingThumbnail.Height, () => false, IntPtr.Zero)
                .ImageToByteArray();

            return File(thumbnail, "image/jpeg");
        }

        private byte[] LoadPhoto(int id)
        {
            byte[] photo;

            if (id > 0) // Existing Ordering
            {
                photo = _orderings.GetPhoto(id) ?? _photos.GetDefaultInstrumentImage();
            }
            else        // New Ordering
            {
                photo = _photos.GetDefaultInstrumentImage();
            }

            return photo;
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
            var header = _headerContent.GetAll().Single();
            var orderings = _orderings.GetAll();

            var vm = new OrderingListViewModel
                     {
                         HeaderInfo = new HeaderInfo
                                      {
                                          Summary = header.Summary, 
                                          Pricing = header.Pricing, 
                                          PaymentOptions = header.PaymentOptions, 
                                          PaymentPolicy = header.PaymentPolicy, 
                                          Shipping = header.Shipping
                                      },
                     };

            var sortedList = orderings
                .OrderBy(ordering => ordering.InstrumentType.SortOrder)
                .ThenBy(ordering => ordering.Classification.SortOrder)
                .ThenBy(ordering => ordering.SubClassification.SortOrder)
                .ThenBy(ordering => ordering.Model);

            foreach (var ordering in sortedList)
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

                InstrumentTypes = new SelectList(_instrumentTypes.GetAll(), "Id", "InstrumentTypeDesc", ordering.InstrumentType.Id),
                InstrumentTypeId = ordering.InstrumentType.Id,

                ClassificationTypes = new SelectList(_classifications.GetAll(), "Id", "ClassificationDesc", ordering.Classification.Id),
                ClassificationId = ordering.Classification.Id,

                SubClassificationTypes = new SelectList(_subClassifications.GetAll(), "Id", "SubClassificationDesc", ordering.SubClassification.Id),
                SubClassificationId = ordering.SubClassification.Id
            };

            return vm;
        }

        private OrderingHeaderEditViewModel LoadOrderingHeaderEditViewModel()
        {
            var headerContent = _headerContent.GetAll().Single();

            var vm = new OrderingHeaderEditViewModel
            {
                Summary = headerContent.Summary,
                Pricing = headerContent.Pricing,
                PaymentOptions = headerContent.PaymentOptions,
                PaymentPolicy = headerContent.PaymentPolicy,
                Shipping = headerContent.Shipping
            };

            return vm;
        }

        private void UpdateOrdering(int orderingId, OrderingEditViewModel viewModel)
        {
            var ordering = _orderings.Get(orderingId);

            if (ordering.InstrumentType.Id != viewModel.InstrumentTypeId)
                ordering.InstrumentType = _instrumentTypes.GetAll().Single(x => x.Id == viewModel.InstrumentTypeId);

            if (ordering.Classification.Id != viewModel.ClassificationId)
                ordering.Classification = _classifications.GetAll().Single(x => x.Id == viewModel.ClassificationId);

            if (ordering.SubClassification.Id != viewModel.SubClassificationId)
                ordering.SubClassification = _subClassifications.GetAll().Single(x => x.Id == viewModel.SubClassificationId);

            ordering.Comments = viewModel.Comments;
            ordering.Model = viewModel.Model;
            ordering.TypicalPrice = viewModel.TypicalPrice;

            _orderings.Update(ordering);
        }

        private void UpdateOrderingHeaderContent(OrderingHeaderEditViewModel viewModel)
        {
            var headerContent = _headerContent.GetAll().Single();

            headerContent.Summary = viewModel.Summary;
            headerContent.Pricing = viewModel.Pricing;
            headerContent.PaymentOptions = viewModel.PaymentOptions;
            headerContent.PaymentPolicy = viewModel.PaymentPolicy;
            headerContent.Shipping = viewModel.Shipping;

            _headerContent.Update(headerContent);
        }
    }
}

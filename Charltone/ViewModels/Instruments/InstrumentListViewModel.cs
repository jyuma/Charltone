using Charltone.Domain.Entities;

namespace Charltone.UI.ViewModels.Instruments
{
    using System.Collections.Generic;

    public class InstrumentListViewModel<T>
    {
        public InstrumentListViewModel()
        {
            InstrumentInfo = new List<T>();
        }

        public List<T> InstrumentInfo { get; private set; }
        public int TotalItemsCount { get; set; }
        public int RowCount { get; set; }
        public string BackgroundImageHeight { get; set; }
        public string Banner { get; set; }
    }

    public class InstrumentInfo
    {
        public InstrumentInfo(Product product, int photoid)
        {
            var instrument = product.Instrument;

            Id = instrument.Id;
            ProductId = product.Id;
            InstrumentType = instrument.InstrumentType.InstrumentTypeDesc;
            Classification = instrument.Classification.ClassificationDesc;
            SubClassification = instrument.SubClassification.SubClassificationDesc;
            Model = instrument.Model + ' ' + instrument.Sn;

            InstrumentStatusPrice = (product.ProductStatus.Id == Constants.ProductStatusTypeId.Available
                ? product.DisplayPrice
                : product.ProductStatus.StatusDesc);
            NotPostedMessage = product.IsPosted ? "" : "NOT POSTED";

            DefaultPhotoId = photoid;
        }

        public int Id;
        public int ProductId;
        public string InstrumentType;
        public string Classification;
        public string SubClassification;
        public string Model;
        public string InstrumentStatusPrice;
        public string Price;
        public int DefaultPhotoId;
        public string NotPostedMessage;
    }
}
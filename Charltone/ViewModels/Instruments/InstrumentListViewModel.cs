
using Charltone.Domain;

namespace Charltone.ViewModels.Instruments
{
    using System.Collections.Generic;

    public class InstrumentListViewModel<T>
    {
        public InstrumentListViewModel()
        {
            InstrumentInfo = new List<T>();
            //PageNumber = pageNumber;
            //TotalItemsCount = totalItemsCount;
            //PageSize = pageSize;
        }

        public List<T> InstrumentInfo { get; private set; }
        public int TotalItemsCount { get; set; }
        public int RowCount { get; set; }
        public string BackgroundImageHeight { get; set; }
        public string Banner { get; set; }
        //public int PageNumber { get; private set; }
        //public int PageSize { get; private set }
    }

    public class InstrumentInfo
    {
        public InstrumentInfo(Instrument instrument, int photoid)
        {
            Id = instrument.Id;
            InstrumentType = instrument.InstrumentType.InstrumentTypeDesc;
            Classification = instrument.Classification.ClassificationDesc;
            SubClassification = instrument.SubClassification.SubClassificationDesc;
            Model = instrument.Model + ' ' + instrument.Sn;
            if (instrument.Product != null)
            {
                InstrumentStatus = instrument.Product.ProductStatus.StatusDesc;
                NotPostedMessage = instrument.Product.IsPosted ? "" : "NOT POSTED";
            }
            DefaultPhotoId = photoid;
        }
        public int Id;
        public string InstrumentType;
        public string Classification;
        public string SubClassification;
        public string Model;
        public string InstrumentStatus;
        public int DefaultPhotoId;
        public string NotPostedMessage;
    }
}
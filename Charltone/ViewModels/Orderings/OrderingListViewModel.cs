using Charltone.Domain.Entities;

namespace Charltone.UI.ViewModels.Orderings
{
    using System.Collections.Generic;

    public class OrderingListViewModel<T>
    {
        public OrderingListViewModel()
        {
            OrderingInfo = new List<T>();
            //PageNumber = pageNumber;
            //TotalItemsCount = totalItemsCount;
            //PageSize = pageSize;
        }

        public List<T> OrderingInfo { get; private set; }
        public int TotalItemsCount { get; set; }
        public int RowCount { get; set; }
        public string BackgroundImageHeight { get; set; }
        public string Banner { get; set; }
        public string Instructions { get; set; }
        //public int PageNumber { get; private set; }
        //public int PageSize { get; private set }
    }

    public class OrderingInfo
    {
        public OrderingInfo(Ordering ordering)
        {
            Id = ordering.Id;
            InstrumentType = ordering.InstrumentType.InstrumentTypeDesc;
            Style = Classification = ordering.Classification.ClassificationDesc + " / " + ordering.SubClassification.SubClassificationDesc;
            Model = ordering.Model;
            TypicalPrice = ordering.TypicalPrice;
            Comments = ordering.Comments;
            Photo = ordering.Photo;
        }
        public int Id;
        public string InstrumentType;
        public string Classification;
        public string SubClassification;
        public string Model;
        public string Style;
        public string TypicalPrice;
        public string Comments;
        public byte[] Photo;
    }
}
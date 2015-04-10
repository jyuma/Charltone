using Charltone.Domain.Entities;

namespace Charltone.UI.ViewModels.Ordering
{
    using System.Collections.Generic;

    public class OrderingListViewModel
    {
        public OrderingListViewModel()
        {
            OrderingInfo = new List<OrderingInfo>();
        }

        public List<OrderingInfo> OrderingInfo { get; private set; }
        public int TotalItemsCount { get; set; }
        public int RowCount { get; set; }
        public string BackgroundImageHeight { get; set; }
        public string Banner { get; set; }
        public string Instructions { get; set; }
    }

    public class OrderingInfo
    {
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
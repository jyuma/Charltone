namespace Charltone.UI.ViewModels.Ordering
{
    using System.Collections.Generic;

    public class OrderingListViewModel
    {
        public OrderingListViewModel()
        {
            OrderingInfo = new List<OrderingInfo>();
        }

        public HeaderInfo HeaderInfo { get; set; }
        public List<OrderingInfo> OrderingInfo { get; private set; }
        public int TotalItemsCount { get; set; }
        public int RowCount { get; set; }
        public string BackgroundImageHeight { get; set; }
        public string Banner { get; set; }
        public string Instructions { get; set; }
    }

    public class HeaderInfo
    {
        public string Summary { get; set; }
        public string Pricing { get; set; }
        public string PaymentOptions { get; set; }
        public string PaymentPolicy { get; set; }
        public string Shipping { get; set; }
    }

    public class OrderingInfo
    {
        public int Id { get; set; }
        public string InstrumentType { get; set; }
        public string Classification { get; set; }
        public string SubClassification { get; set; }
        public string Model { get; set; }
        public string Style { get; set; }
        public string TypicalPrice { get; set; }
        public string Comments { get; set; }
        public byte[] Photo { get; set; }
    }
}
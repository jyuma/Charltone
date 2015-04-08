namespace Charltone.UI.ViewModels.Instruments
{
    using System.Collections.Generic;

    public class InstrumentListViewModel
    {
        public InstrumentListViewModel()
        {
            Instruments = new List<InstrumentViewModel>();
        }

        public List<InstrumentViewModel> Instruments { get; set; }
        public int TotalItemsCount { get; set; }
        public int RowCount { get; set; }
        public string BackgroundImageHeight { get; set; }
        public string Banner { get; set; }
    }

    public class InstrumentViewModel
    {
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
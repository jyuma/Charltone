using System.Collections.Generic;

namespace Charltone.UI.ViewModels.Instrument
{
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
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string WrapperClassId { get; set; }
        public string ClassId { get; set; }
        public string PhotoClassId { get; set; }
        public string InstrumentType { get; set; }
        public string Classification { get; set; }
        public string SubClassification { get; set; }
        public string Model { get; set; }
        public Status Status { get; set; }
        public string Price { get; set; }
        public int DefaultPhotoId { get; set; }
        public string NotPostedMessage { get; set; }
    }
}
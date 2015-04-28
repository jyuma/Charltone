using System.Collections.Generic;

namespace Charltone.UI.ViewModels.Instrument
{
    public class InstrumentListViewModel
    {
        public InstrumentListViewModel()
        {
            Instruments = new List<InstrumentViewModel>();
        }

        public IEnumerable<InstrumentViewModel> Instruments { get; set; }
        public int InstrumentsCount { get; set; }
        public string Banner { get; set; }
        public bool IsAuthenticated { get; set; }
        public int MaxImageWidth { get; set; }
        public int MaxImageHeight { get; set; }
    }

    public class InstrumentViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string InstrumentType { get; set; }
        public string Classification { get; set; }
        public string SubClassification { get; set; }
        public string ModelSn { get; set; }
        public string Status { get; set; }
        public string StatusCssClassId { get; set; }
        public string Price { get; set; }
        public bool ShowPrice { get; set; }
        public int DefaultPhotoId { get; set; }
        public string NotPostedMessage { get; set; }
    }

    public class InstrumentPhotoModel
    {
        public int PhotoId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
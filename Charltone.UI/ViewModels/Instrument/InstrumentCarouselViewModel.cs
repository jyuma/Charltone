using System.Collections.Generic;

namespace Charltone.UI.ViewModels.Instrument
{
    public class InstrumentCarouselViewModel
    {
        public IEnumerable<CarouselPhoto> Photos { get; set; }
        public int MaxImageWidth { get; set; }
        public int MaxImageHeight { get; set; }
    }

    public class CarouselPhoto
    {
        public int PhotoId { get; set; }
        public string Caption { get; set; }
        public string Price { get; set; }
    }
}
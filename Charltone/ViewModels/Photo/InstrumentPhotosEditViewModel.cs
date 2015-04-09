using System.Collections.Generic;

namespace Charltone.UI.ViewModels.Photo
{
    public class InstrumentPhotosEditViewModel
    {
        public int ProductId { get; set; }
        public string Model { get; set; }
        public int DefaultPhotoId { get; set; }
        public IList<int> PhotoIds { get; set; }
    }
}

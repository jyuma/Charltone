using System.Collections.Generic;

namespace Charltone.UI.ViewModels.Photos
{
    public class PhotosEditViewModel
    {
        public int ProductId { get; set; }
        public string Model { get; set; }
        public int DefaultPhotoId { get; set; }
        public IList<int> PhotoIds { get; set; }
    }
}

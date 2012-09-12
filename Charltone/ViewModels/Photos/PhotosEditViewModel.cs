using System.Collections.Generic;
using Charltone.Domain;

namespace Charltone.ViewModels.Photos
{
    public class PhotosEditViewModel
    {
        public int ProductId;
        public string Model;
        public int DefaultPhotoId;
        public List<Photo> Photos;
    }
}

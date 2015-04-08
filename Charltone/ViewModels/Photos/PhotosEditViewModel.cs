using System.Collections.Generic;
using Charltone.Domain.Entities;

namespace Charltone.UI.ViewModels.Photos
{
    public class PhotosEditViewModel
    {
        public int ProductId;
        public string Model;
        public int DefaultPhotoId;
        public IList<Photo> Photos;
    }
}

namespace Charltone.ViewModels.Home
{
    using System.Collections.Generic;

    public class HomePageImageEditViewModel<T>
    {
        public List<T> HomePageImages { get; private set; }

        public HomePageImageEditViewModel()
        {
            HomePageImages = new List<T>();
        }
    }

    public class HomePageImages
    {
        public int Id;
        public byte[] Data;
        public int SortOrder;

        public HomePageImages(int imageId, byte[] imageData, int sortOrder)
        {
            Id = imageId;
            Data = imageData;
            SortOrder = sortOrder;
        }
    }
}
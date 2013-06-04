namespace Charltone.ViewModels.Home
{
    using System.Collections.Generic;

    public class HomeViewModel<T>
    {
        public List<T> HomePageImageList { get; private set; }
        public int Complete = 0;
        public int ImageCount = 0;

        public HomeViewModel()
        {
            HomePageImageList = new List<T>();
        }
    }

    public class HomePageImageData
    {
        public int Id;
        public string Data;

        public HomePageImageData(int id, string data)
        {
            Id = id;
            Data = data;
        }
    }
}
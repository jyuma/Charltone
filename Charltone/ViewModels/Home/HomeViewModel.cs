namespace Charltone.UI.ViewModels.Home
{
    public class HomeViewModel<T>
    {
        public string HomePageImage { get; set; }
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
using Charltone.UI.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Charltone.UI.ViewModels.Home
{
    public class HomeViewModel
    {
        public string Introduction { get; set; }
        public string Greeting { get; set; }
        public bool IsAuthenticated { get; set; }
        public int MaxImageWidth { get; set; }
        public int MaxImageHeight { get; set; }

        [FileTypes("jpg,jpeg", ErrorMessage = "Only jpg/jpeg image formats are supported")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }
    }
}
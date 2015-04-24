using System.ComponentModel.DataAnnotations;
using Charltone.UI.Attributes;
using System.Web;

namespace Charltone.UI.ViewModels.Ordering
{
    public class OrderingPhotoEditViewModel
    {
        public int OrderingId { get; set; }
        public string Model { get; set; }
        public byte[] Photo { get; set; }

        [FileTypes("jpg,jpeg", ErrorMessage = "Only jpg/jpeg image formats are supported")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }
    }
}
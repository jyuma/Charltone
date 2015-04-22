using Charltone.UI.Attributes;
using System.Web;

namespace Charltone.UI.ViewModels.Ordering
{
    public class OrderingPhotoEditViewModel
    {
        public int OrderingId { get; set; }
        public string Model { get; set; }
        public byte[] Photo { get; set; }
    }

    public class UploadFileModel
    {
        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase File { get; set; }
    }
}
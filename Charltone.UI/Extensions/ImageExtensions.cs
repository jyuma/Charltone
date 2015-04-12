using Charltone.UI.Constants;
using Charltone.UI.Library;
using System.Drawing;
using System.IO;

namespace Charltone.UI.Extensions
{
    public static class ImageExtensions
    {
        public static byte[] ImageToByteArray(this Image image)
        {
            var ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(this byte[] byteArray)
        {
            var ms = new MemoryStream(byteArray);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static Image CropInstrument(this Image image)
        {
            return Imaging.Crop(image, new Size(InstrumentPhoto.Width, InstrumentPhoto.Height), InstrumentPhoto.Ratio);
        }

        public static Image CropOrdering(this Image image)
        {
            return Imaging.Crop(image, new Size(OrderingPhoto.Width, OrderingPhoto.Height), OrderingPhoto.Ratio);
        }

        public static Image CropHomePageImage(this Image image)
        {
            return Imaging.Crop(image, new Size(HomePagePhoto.Width, HomePagePhoto.Height), HomePagePhoto.Ratio);
        }
    }
}
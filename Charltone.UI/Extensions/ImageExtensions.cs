using System.Drawing;
using System.IO;
using Charltone.UI.Constants;

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
            return Crop(image, new Size(InstrumentPhoto.Width, InstrumentPhoto.Height));
        }

        public static Image CropOrdering(this Image image)
        {
            return Crop(image, new Size(OrderingPhoto.Width, OrderingPhoto.Height));
        }

        private static Image Crop(Image image, Size size)
        {
            var width = image.Size.Width;
            var height = image.Size.Height;

            if (width > height / 1.3)
            {
                width = (int)(height / 1.3);
            }
            else
            {
                height = (int)(width * 1.3);
            }

            var cropRect = new Rectangle(new Point(0, 0), new Size(width, height));
            var croppped = new Bitmap(image).Clone(cropRect, image.PixelFormat);

            var resizeRect = new Rectangle(new Point(0, 0), croppped.Size);
            var resized = new Bitmap(image).Clone(resizeRect, croppped.PixelFormat);

            var result = new Bitmap(resized, new Size(size.Width, size.Height));

            croppped.Dispose();
            resized.Dispose();

            return result;
        }
    }
}


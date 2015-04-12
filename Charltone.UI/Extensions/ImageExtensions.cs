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
            return Crop(image, new Size(InstrumentPhoto.Width, InstrumentPhoto.Height), InstrumentPhoto.Ratio);
        }

        public static Image CropOrdering(this Image image)
        {
            return Crop(image, new Size(OrderingPhoto.Width, OrderingPhoto.Height), OrderingPhoto.Ratio);
        }

        public static Image CropHomePageImage(this Image image)
        {
            return Crop(image, new Size(OrderingPhoto.Width, OrderingPhoto.Height), HomePagePhoto.Ratio);
        }

        private static Image Crop(Image image, Size size, double ratio)
        {
            var width = image.Size.Width;
            var height = image.Size.Height;

            // ------- crop
            Rectangle cropRect;
            if (width > height / ratio)   // too wide
            {
                var newWidth = (int)(height / ratio);
                var x = (width - newWidth) / 2;
                var point = new Point(x, 0);

                cropRect = new Rectangle(point, new Size(newWidth, height));
            }
            else                        // too tall
            {
                var newHeight = (int)(width * ratio);
                var y = (height - newHeight) / 2;

                var point = new Point(0, y);
                cropRect = new Rectangle(point, new Size(width, newHeight));
            }
            var cropped = new Bitmap(image).Clone(cropRect, image.PixelFormat);

            // ------- resize
            var resizeRect = new Rectangle(new Point(0, 0), cropped.Size);
            var resized = new Bitmap(image).Clone(resizeRect, cropped.PixelFormat);

            var result = new Bitmap(cropped, new Size(size.Width, size.Height));

            cropped.Dispose();
            resized.Dispose();

            return result;
        }
    }
}
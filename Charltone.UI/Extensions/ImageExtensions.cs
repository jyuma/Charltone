using Charltone.UI.Constants;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Charltone.UI.Extensions
{
    public static class ImageExtensions
    {
        public static byte[] ImageToByteArray(this Image image)
        {
            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static byte[] Thumbnail(this byte[] data, int width, int height)
        {
            var thumbnail = data.ByteArrayToImage()
                .GetThumbnailImage(width, height, () => false, IntPtr.Zero);
            var result = thumbnail.ImageToByteArray();

            thumbnail.Dispose();

            return result;
        }

        public static byte[] Crop(this byte[] data, Size size)
        {
            var cropped = Crop(data.ByteArrayToImage(), size);
            var result = cropped.ImageToByteArray();

            cropped.Dispose();

            return result;
        }

        public static Image ByteArrayToImage(this byte[] byteArray)
        {
            var ms = new MemoryStream(byteArray);
            var result = Image.FromStream(ms);

            return result;
        }

        public static Image CropInstrument(this Image image)
        {
            return Crop(image, new Size(InstrumentPhotoSize.Width, InstrumentPhotoSize.Height));
        }

        public static Image CropInstrumentList(this Image image)
        {
            return Crop(image, new Size(InstrumentListPhotoSize.Width, InstrumentListPhotoSize.Height));
        }

        public static Image CropOrdering(this Image image)
        {
            return Crop(image, new Size(OrderingPhoto.Width, OrderingPhoto.Height));
        }

        public static Image CropHomePageImage(this Image image)
        {
            return Crop(image, new Size(HomePagePhoto.Width, HomePagePhoto.Height));
        }

        public static void Save(this byte[] data, string path)
        {
            var cropped = data.ByteArrayToImage().CropHomePageImage().ImageToByteArray();

            using (var stream = new MemoryStream(cropped))
            {
                using (var img = Image.FromStream(stream))
                {
                    img.Save(path, ImageFormat.Jpeg);
                    img.Dispose();
                }
            }
        }

        private static Image Crop(Image image, Size size)
        {
            var width = image.Size.Width;
            var height = image.Size.Height;
            var ratio = (double) height / width;

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
            var resized = new Bitmap(cropped).Clone(resizeRect, cropped.PixelFormat);
            var result = new Bitmap(resized, new Size(size.Width, size.Height));

            cropped.Dispose();
            resized.Dispose();

            return result;
        }
    }
}
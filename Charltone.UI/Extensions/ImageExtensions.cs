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

        // Crop only
        public static byte[] Crop(this byte[] data, Size size)
        {
            var cropped = CropImage(data.ByteArrayToImage(), size);
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

        // Crop and re-size
        public static Image CropInstrument(this Image image)
        {
            return CropImage(image, new Size(InstrumentPhotoSize.Width, InstrumentPhotoSize.Height));
        }

        public static Image CropInstrumentList(this Image image)
        {
            return CropImage(image, new Size(InstrumentListPhotoSize.Width, InstrumentListPhotoSize.Height));
        }

        public static Image CropOrdering(this Image image)
        {
            return CropImage(image, new Size(OrderingPhoto.Width, OrderingPhoto.Height));
        }

        public static Image CropHomePageImage(this Image image)
        {
            return CropImage(image, new Size(HomePagePhoto.Width, HomePagePhoto.Height));
        }

        // Save
        public static void SaveHomePageImage(this byte[] data, string path)
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

        // Main logic
        private static Image CropImage(Image image, Size size)
        {
            Bitmap result;
            var width = size.Width;
            var height = size.Height;
            var ratio = (double)height / width;

            // ------- crop (if necessary)
            Rectangle cropRect;
            if (image.Width > image.Height / ratio)   // too wide
            {
                var newWidth = (int)(image.Height / ratio);
                var x = (image.Width - newWidth) / 2;
                var point = new Point(x, 0);

                cropRect = new Rectangle(point, new Size(newWidth, image.Height));
            }
            else                        // too tall
            {
                var newHeight = (int)(image.Width * ratio);
                var y = (image.Height - newHeight) / 2;

                var point = new Point(0, y);
                cropRect = new Rectangle(point, new Size(image.Width, newHeight));
            }
            var cropped = new Bitmap(image).Clone(cropRect, image.PixelFormat);

            // ------- resize (if necessary)
            if (cropped.Width > size.Width)
            {
                var resizeRect = new Rectangle(new Point(0, 0), cropped.Size);
                var resized = new Bitmap(cropped).Clone(resizeRect, cropped.PixelFormat);
                result = new Bitmap(resized, new Size(size.Width, size.Height));
                resized.Dispose();
            }
            else
            {
                result = new Bitmap(cropped, new Size(size.Width, size.Height));    
            }
            
            cropped.Dispose();

            return result;
        }
    }
}
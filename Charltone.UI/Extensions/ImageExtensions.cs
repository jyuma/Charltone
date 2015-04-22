using Charltone.UI.Constants;
using Charltone.UI.Library;
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
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static byte[] Thumbnail(this byte[] data, int width, int height)
        {
            var thumbnail = data.ByteArrayToImage().GetThumbnailImage(width, height, () => false, IntPtr.Zero);
            var result = thumbnail.ImageToByteArray();
            thumbnail.Dispose();
            return result;
        }

        public static byte[] Crop(this byte[] data, Size size)
        {
            var cropped = Imaging.Crop(data.ByteArrayToImage(), size);
            var result = cropped.ImageToByteArray();
            cropped.Dispose();
            return result;
        }

        public static Image ByteArrayToImage(this byte[] byteArray)
        {
            var ms = new MemoryStream(byteArray);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static Image CropInstrument(this Image image)
        {
            return Imaging.Crop(image, new Size(InstrumentPhotoSize.Width, InstrumentPhotoSize.Height));
        }

        public static Image CropInstrumentList(this Image image)
        {
            return Imaging.Crop(image, new Size(InstrumentListPhotoSize.Width, InstrumentListPhotoSize.Height));
        }

        public static Image CropOrdering(this Image image)
        {
            return Imaging.Crop(image, new Size(OrderingPhoto.Width, OrderingPhoto.Height));
        }

        public static Image CropHomePageImage(this Image image)
        {
            return Imaging.Crop(image, new Size(HomePagePhoto.Width, HomePagePhoto.Height));
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
    }
}
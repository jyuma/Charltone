using System.Drawing;

namespace Charltone.UI.Library
{
    public static class Imaging
    {
        public static Image Crop(Image image, Size size)
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
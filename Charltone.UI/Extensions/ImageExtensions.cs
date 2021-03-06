﻿using System;
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

        public static Image ByteArrayToImage(this byte[] byteArray)
        {
            var ms = new MemoryStream(byteArray);
            var result = Image.FromStream(ms);

            return result;
        }

        // Resizing
        public static byte[] Resize(this byte[] data, Size size)
        {
            var image = data.ByteArrayToImage();

            var resizeRect = new Rectangle(new Point(0, 0), image.Size);
            var resized = new Bitmap(image).Clone(resizeRect, image.PixelFormat);
            var newImage = new Bitmap(resized, new Size(size.Width, size.Height));
            var result = newImage.ImageToByteArray();

            image.Dispose();
            resized.Dispose();
            newImage.Dispose();

            return result;
        }
    }
}
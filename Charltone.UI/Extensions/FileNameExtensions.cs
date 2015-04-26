using System;

namespace Charltone.UI.Extensions
{
    public static class FileNameExtensions
    {
        public static string Extension(this string fileName)
        {
            var indexOfDot = fileName.LastIndexOf(".", StringComparison.Ordinal) + 1;
            return fileName.Substring(indexOfDot, fileName.Length - indexOfDot).ToLower();
        }
    }
}
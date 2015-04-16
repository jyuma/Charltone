using System.Collections.Generic;
using Charltone.Domain.Entities;
using System.Linq;

namespace Charltone.UI.Extensions
{
    public static class ProductExtensions
    {
        public static int GetDefaultPhotoId(this Product product)
        {
            if (product.Photos == null) return -1;

            var id = -1;
            if (product.Photos.Any(x => x.IsDefault))
            {
                id = product.Photos.Single(x => x.IsDefault).Id;
            }

            return id;
        }

        public static void ResetSortOrder(this IEnumerable<Photo> photos)
        {
            var sortOrder = 1;

            foreach (var photo in photos)
            {
                photo.SortOrder = sortOrder++;
            }
        }

    }
}
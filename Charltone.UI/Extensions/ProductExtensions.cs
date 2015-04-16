using System.Linq;
using Charltone.Domain.Entities;

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
    }
}
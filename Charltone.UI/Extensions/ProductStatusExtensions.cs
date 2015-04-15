using Charltone.Domain.Entities;
using Charltone.UI.Constants;

namespace Charltone.UI.Extensions
{
    public static class ProductStatusExtensions
    {
        public static string GetClassId(this ProductStatus status)
        {
            var result = "available";

            switch (status.Id)
            {
                case ProductStatusTypeId.Sold:
                    result = "sold";
                    break;
                case ProductStatusTypeId.NotForSale:
                    result = "notforsale";
                    break;
                case ProductStatusTypeId.InProgress: 
                    result = "inprogress";
                    break;
            }
            return result;
        }

        public static string GetDescription(this ProductStatus status, string displayPrice)
        {
            return status.Id != ProductStatusTypeId.Available 
                ? status.StatusDesc 
                : displayPrice;
        }
    }
}
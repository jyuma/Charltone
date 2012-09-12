using System.Collections.Generic;
using Charltone.Domain;

namespace Charltone.Repositories
{
    public interface ITypeRepository
    {
        IList<InstrumentType> GetInstrumentTypeList();
        IList<Classification> GetClassificationList();
        IList<SubClassification> GetSubClassificationList();
        IList<ProductStatus> GetProductStatusList();

        InstrumentType GetSingleInstrumentType(int id);
        Classification GetSingleClassification(int id);
        SubClassification GetSingleSubClassification(int id);
        ProductStatus GetSingleProductStatus(int id);
    }
}
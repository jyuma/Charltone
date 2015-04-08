using System.Collections.Generic;
using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IInstrumentTypeRepository : IRepositoryBase<InstrumentType>
    {
        IList<InstrumentType> GetInstrumentTypeList();
        IList<Classification> GetClassificationList();
        IList<SubClassification> GetSubClassificationList();
        IList<ProductStatus> GetProductStatusList();

        InstrumentType GetSingleInstrumentType(int id);
        Classification GetSingleClassification(int id);
        SubClassification GetSingleSubClassification(int id);
        ProductStatus GetProductStatus(int id);
    }

    public class InstrumentTypeRepository : RepositoryBase<InstrumentType>, IInstrumentTypeRepository
    {
        public InstrumentTypeRepository(ISession session)
            : base(session)
        {
        }

        public IList<InstrumentType> GetInstrumentTypeList()
        {
            return Session.QueryOver<InstrumentType>().List();
        }

        public InstrumentType GetSingleInstrumentType(int id)
        {
            return Session.QueryOver<InstrumentType>().Where(x => x.Id == id).SingleOrDefault();
        }

        public IList<Classification> GetClassificationList()
        {
            return Session.QueryOver<Classification>().List();
        }

        public Classification GetSingleClassification(int id)
        {
            return Session.QueryOver<Classification>().Where(x => x.Id == id).SingleOrDefault();
        }

        public IList<SubClassification> GetSubClassificationList()
        {
            return Session.QueryOver<SubClassification>().List();
        }

        public SubClassification GetSingleSubClassification(int id)
        {
            return Session.QueryOver<SubClassification>().Where(x => x.Id == id).SingleOrDefault();
        }

        public IList<ProductStatus> GetProductStatusList()
        {
            return Session.QueryOver<ProductStatus>().List();
        }

        public ProductStatus GetProductStatus(int id)
        {
            return Session.QueryOver<ProductStatus>().Where(x => x.Id == id).SingleOrDefault();
        }
    }
}

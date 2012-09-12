using System.Collections.Generic;
using Charltone.Domain;
using NHibernate;

namespace Charltone.Repositories
{
    public class TypeRepository : ITypeRepository
    {
		private readonly ISession _session;

        public TypeRepository(ISession session)
		{
            _session = session;
		}

        public IList<InstrumentType> GetInstrumentTypeList()
        {
            return _session.QueryOver<InstrumentType>().List();
        }

        public InstrumentType GetSingleInstrumentType(int id)
        {
            return _session.QueryOver<InstrumentType>().Where(x => x.Id == id).SingleOrDefault();
        }

        public IList<Classification> GetClassificationList()
        {
            return _session.QueryOver<Classification>().List();
        }

        public Classification GetSingleClassification(int id)
        {
            return _session.QueryOver<Classification>().Where(x => x.Id == id).SingleOrDefault();
        }

        public IList<SubClassification> GetSubClassificationList()
        {
            return _session.QueryOver<SubClassification>().List();
        }

        public SubClassification GetSingleSubClassification(int id)
        {
            return _session.QueryOver<SubClassification>().Where(x => x.Id == id).SingleOrDefault();
        }

        public IList<ProductStatus> GetProductStatusList()
        {
            return _session.QueryOver<ProductStatus>().List();
        }

        public ProductStatus GetSingleProductStatus(int id)
        {
            return _session.QueryOver<ProductStatus>().Where(x => x.Id == id).SingleOrDefault();
        }
    }
}
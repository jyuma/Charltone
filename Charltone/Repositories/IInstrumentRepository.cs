using Charltone.Domain;

namespace Charltone.Repositories
{
    using System.Collections.Generic;

	public interface IInstrumentRepository
	{
        IList<Instrument> GetList(int pageNumber, bool includeUnposted);
        Instrument GetSingle(int id);
        int Count(bool includeUnposted);
	    void Update(Instrument instrument);
        //void Save(Instrument instrument);
        //void Delete(Instrument instrument);
	}
}
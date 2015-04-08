using System.Collections.Generic;

namespace Charltone.Domain.Entities
{
    public class InstrumentType : EntityBase
    {
        public virtual string InstrumentTypeDesc { get; set; }
        public virtual int SortOrder { get; set; }
        //public virtual IList<Instrument> Instruments { get; set; }
    }
}
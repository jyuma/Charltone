using System.Collections.Generic;

namespace Charltone.Domain
{
    public class SubClassification : EntityBase
    {
        public virtual string SubClassificationDesc { get; set; }
        public virtual int SortOrder { get; set; }
        //public virtual IList<Instrument> Instruments { get; set; }
    }
}

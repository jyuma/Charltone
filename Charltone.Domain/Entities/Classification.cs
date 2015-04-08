using System.Collections.Generic;

namespace Charltone.Domain.Entities
{
    public class Classification : EntityBase
    {
        public virtual string ClassificationDesc { get; set; }
        public virtual int SortOrder { get; set; }
        //public virtual IList<Instrument> Instruments { get; set; }
    }
}

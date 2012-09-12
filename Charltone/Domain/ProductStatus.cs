using System.Collections.Generic;

namespace Charltone.Domain
{
    public class ProductStatus : EntityBase
    {
        public virtual string StatusDesc { get; set; }
        //public virtual IList<Instrument> Instruments { get; set; }
    }
}
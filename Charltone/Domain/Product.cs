using System.Collections.Generic;

namespace Charltone.Domain
{
    public class Product : EntityBase
    {
        public virtual ProductType ProductType { get; set; }
        public virtual string ProductDesc { get; set; }
        public virtual decimal Price { get; set; }
        public virtual string DisplayPrice { get; set; }
        public virtual Instrument Instrument { get; set; }
        public virtual ProductStatus ProductStatus { get; set; }
        public virtual IList<Photo> Photos { get; set; }
        public virtual bool IsPosted { get; set; }
    }
}
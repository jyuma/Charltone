namespace Charltone.Domain
{
    public class ProductType : EntityBase
    {
        public virtual string ProductTypeDesc { get; set; }
        public virtual int SortOrder { get; set; }
    }
}
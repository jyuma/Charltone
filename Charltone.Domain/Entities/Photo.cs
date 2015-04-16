namespace Charltone.Domain.Entities
{
    public class Photo : EntityBase
    {
        public virtual bool IsDefault { get; set; }
        public virtual byte[] Data { get; set; }
        public virtual int SortOrder { get; set; }
        public virtual Product Product { get; set; }
    }
}

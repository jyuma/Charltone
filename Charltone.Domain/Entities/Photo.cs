namespace Charltone.Domain.Entities
{
    public class Photo : EntityBase
    {
        public virtual int ProductId { get; set; }
        public virtual bool IsDefault { get; set; }
        public virtual byte[] Data { get; set; }
        public virtual Product Product { get; set; }
    }
}

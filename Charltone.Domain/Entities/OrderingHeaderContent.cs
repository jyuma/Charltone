namespace Charltone.Domain.Entities
{
    public class OrderingHeaderContent : EntityBase
    {
        public virtual string Summary { get; set; }
        public virtual string Pricing { get; set; }
        public virtual string PaymentOptions { get; set; }
        public virtual string PaymentPolicy { get; set; }
        public virtual string Shipping { get; set; }
    }
}

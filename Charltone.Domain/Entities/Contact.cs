namespace Charltone.Domain.Entities
{
    public class Contact : EntityBase
    {
        public virtual string From { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Message { get; set; }
    }
}
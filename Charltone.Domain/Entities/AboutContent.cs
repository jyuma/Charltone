namespace Charltone.Domain.Entities
{
    public class AboutContent : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string Origins { get; set; }
        public virtual string Materials { get; set; }
    }
}

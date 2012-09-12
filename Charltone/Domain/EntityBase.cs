namespace Charltone.Domain
{
	/// <summary>
	///   Layer supertype for all entity classes
	/// </summary>
	public abstract class EntityBase
	{
		public virtual int Id { get; set; }
	}
}
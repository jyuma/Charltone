using System;

namespace Charltone.Plumbing
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class DoNotMapAttribute : Attribute
	{
	}
}
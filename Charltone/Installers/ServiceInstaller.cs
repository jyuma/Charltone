using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Charltone.Services;

namespace Charltone.Installers
{
	public class ServiceInstaller : IWindsorInstaller
	{
		#region IWindsorInstaller Members

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(AllTypes.FromThisAssembly().Pick()
			                   	.If(Component.IsInSameNamespaceAs<FormsAuthenticationService>())
			                   	.Configure(c => c.LifeStyle.Transient)
			                   	.WithService.DefaultInterface());
		}

		#endregion
	}
}
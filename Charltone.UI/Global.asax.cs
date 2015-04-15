using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Charltone.Data.Repositories;
using Ninject;
using Ninject.Web.Common;

namespace Charltone.UI
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(Assembly.GetExecutingAssembly());

            kernel.Bind<IAboutContentRepository>().To<AboutContentRepository>();
            kernel.Bind<IAdminRepository>().To<AdminRepository>();
            kernel.Bind<IHomeContentRepository>().To<HomeContentRepository>();
            kernel.Bind<IInstrumentRepository>().To<InstrumentRepository>();
            kernel.Bind<IInstrumentTypeRepository>().To<InstrumentTypeRepository>();
            kernel.Bind<IOrderingRepository>().To<OrderingRepository>();
            kernel.Bind<IOrderingHeaderContentRepository>().To<OrderingHeaderContentRepository>();
            kernel.Bind<IPhotoRepository>().To<PhotoRepository>();
            kernel.Bind<IProductRepository>().To<ProductRepository>();

            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(Kernel));
        }
    }

    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel;

        public NinjectControllerFactory(IKernel kernel)
        {
            _ninjectKernel = kernel;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (controllerType == null) ? null : (IController)_ninjectKernel.Get(controllerType);
        }
    } 
}

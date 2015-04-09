using System.Web.Mvc;

namespace Charltone.UI.Helpers
{
    public static class ActionExtensions
    {
        public static bool ActionAuthorized(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            var controllerBase = string.IsNullOrEmpty(controllerName) ? htmlHelper.ViewContext.Controller : htmlHelper.GetControllerByName(controllerName);
            var controllerContext = new ControllerContext(htmlHelper.ViewContext.RequestContext, controllerBase);
            var controllerDescriptor = new ReflectedControllerDescriptor(controllerContext.Controller.GetType());
            var actionDescriptor = controllerDescriptor.FindAction(controllerContext, actionName);

            if (actionDescriptor == null) return false;

            var filters = new FilterInfo(FilterProviders.Providers.GetFilters(controllerContext, actionDescriptor));

            var authorizationContext = new AuthorizationContext(controllerContext, actionDescriptor);

            foreach (var authorizationFilter in filters.AuthorizationFilters)
            {
                authorizationFilter.OnAuthorization(authorizationContext);
                if (authorizationContext.Result != null)
                    return false;
            }

            return true;
        }
    }

}
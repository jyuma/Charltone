using System;
using System.Globalization;
using System.Web.Mvc;

namespace Charltone.UI.ViewModels.Helpers
{
    internal static class Helpers
    {
        public static ControllerBase GetControllerByName(this HtmlHelper htmlHelper, string controllerName)
        {
            var factory = ControllerBuilder.Current.GetControllerFactory();
            var controller = factory.CreateController(htmlHelper.ViewContext.RequestContext, controllerName);

            if (controller == null)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "The IControllerFactory '{0}' did not return a controller for the name '{1}'.", factory.GetType(), controllerName));
            }
            return (ControllerBase)controller;
        }
    }
}
using System;
using System.Web.Mvc;
using Castle.Core.Logging;
using Charltone.Domain;
using Charltone.Services;
using NHibernate;
using Charltone.ViewModels.Admin;

namespace Charltone.Controllers
{
    [HandleError]
    public class AdminController : Controller
    {

        private readonly ISession _session;

        public AdminController(ISession session)
		{
			_session = session;
		}

        public virtual IFormsAuthenticationService FormsService { get; set; }

        public virtual ILogger Logger { get; set; }

        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AdminLogOnViewModel model, string returnUrl)
        {
            model.UserName = "Administrator";
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.Password);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            //--- if we got this far, something failed, redisplay form
            return View(model);
        }

        private bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            return (_session.QueryOver<AdminUser>().Where(x => x.AdminPassword == password).RowCount() == 1);
        }


        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
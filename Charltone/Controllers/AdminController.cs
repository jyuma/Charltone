using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Charltone.Data.Repositories;

namespace Charltone.UI.Controllers
{
    [HandleError]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
		{
            _adminRepository = adminRepository;
		}

        [HttpGet]
        public JsonResult Login(string password)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            {
                var msg = ValidateAdminLogin(password);

                if (msg == null)
                {
                    CreateLoginAuthenticationTicket("Admin");
                    return Json(new {success = true}, JsonRequestBehavior.AllowGet);
                }
                return Json(new {success = false, messages = msg}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult LogOff()
        {
            FormsAuthentication.SignOut();
            ClearAdminCookie();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        private List<string> ValidateAdminLogin(string password)
        {
            var msgs = new List<string>();

            // check for empty password
            if (password == null)
                msgs.Add("Password is required.");
            else if (password.Length == 0)
                msgs.Add("Password is required.");

            if (msgs.Count > 0) return msgs;

            // if all fields have been entered correctly, check the credentials
            var admin = _adminRepository.AttemptToLoginAdmin(password);
            if (admin == null)
                msgs.Add("Incorrect password.");

            return msgs.Count > 0 ? msgs : null;
        }

        private void CreateLoginAuthenticationTicket(string username)
        {
            var cookieTimeoutMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["CookieTimeoutMinutes"]);
            var ticket = new FormsAuthenticationTicket(1, username, DateTime.Now,
                                                       DateTime.Now.AddMinutes(cookieTimeoutMinutes), false, username);
            var enticket = FormsAuthentication.Encrypt(ticket);
            var cname = FormsAuthentication.FormsCookieName;

            Response.Cookies.Add(new HttpCookie(cname, enticket));
        }

        private void ClearAdminCookie()
        {
            var cookie = Request.Cookies["Admin"];
            if (cookie == null) return;
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
        }
    }
}
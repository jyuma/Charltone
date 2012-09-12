using System.Web.Security;

namespace Charltone.Services
{
	public class FormsAuthenticationService : IFormsAuthenticationService
	{
		public void SignIn(string username, string password)
		{
			FormsAuthentication.SetAuthCookie(username, false);
		}

		public void SignOut()
		{
			FormsAuthentication.SignOut();
		}
	}
}
namespace Charltone.UI.Services
{
	public interface IFormsAuthenticationService
	{
		void SignIn(string username, string password);

		void SignOut();
	}
}
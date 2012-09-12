namespace Charltone.Services
{
	public interface IFormsAuthenticationService
	{
		void SignIn(string username, string password);

		void SignOut();
	}
}
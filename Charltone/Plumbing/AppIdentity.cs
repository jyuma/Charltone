using System;
using System.Security.Principal;
using System.Web.Security;

namespace Charltone.Plumbing
{
	public class AppIdentity : IIdentity
	{
		public AppIdentity(string name)
		{
			//Name = nick;
			//Email = email;
            Name = name;
			IsAuthenticated = true;
		}

		public AppIdentity(FormsAuthenticationTicket ticket)
		{
			if (ticket == null || ticket.Name == null)
				throw new ArgumentNullException("ticket");

			//Email = ticket.Name.Substring(ticket.Name.IndexOf("<"));
			//Name = ticket.Name.Remove(ticket.Name.IndexOf("<") - 1, Email.Length + 1);
		}

		public string Email { get; private set; }

		#region IIdentity Members

		public bool IsAuthenticated { get; private set; }

		public string Name { get; private set; }

        public string AuthenticationType
        {
            get { return "OpenID"; }
        }

		#endregion

		public override string ToString()
		{
			return string.Format("{0} <{1}>", Name, Email);
		}
	}
}
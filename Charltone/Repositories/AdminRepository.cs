using Charltone.Domain;
using NHibernate;


namespace Charltone.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ISession _session;

        public AdminRepository(ISession session)
		{
            _session = session;
		}

        public AdminUser AttemptToLoginAdmin(string password)
        {
            var adminUser = _session.QueryOver<AdminUser>()
                .Where(x => x.AdminPassword == password)
                .SingleOrDefault();
            return adminUser;
        }
    }
}
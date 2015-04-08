using System.Collections.Generic;
using Charltone.Domain.Entities;
using NHibernate;

namespace Charltone.Data.Repositories
{
    public interface IAdminRepository : IRepositoryBase<AdminUser>
    {
        AdminUser AttemptToLoginAdmin(string password);
    }

    public class AdminRepository : RepositoryBase<AdminUser>, IAdminRepository
    {
        public AdminRepository(ISession session)
            : base(session)
        {
        }

        public AdminUser AttemptToLoginAdmin(string password)
        {
            var adminUser = Session.QueryOver<AdminUser>()
                .Where(x => x.AdminPassword == password)
                .SingleOrDefault();
            return adminUser;
        }
    }
}
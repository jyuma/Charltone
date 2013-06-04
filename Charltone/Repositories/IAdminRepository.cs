using Charltone.Domain;

namespace Charltone.Repositories
{
    public interface IAdminRepository
    {
        AdminUser AttemptToLoginAdmin(string password);
    }
}
using ManagementSystem.Data.Models;

namespace ManagementSystem.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IAccountRepository Accounts { get; }
        void Commit();
    }
}
using ManagementSystem.Data.Models;

namespace ManagementSystem.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<User> Users { get; }
        IAccountRepository Accounts { get; }
        void Commit();
    }
}
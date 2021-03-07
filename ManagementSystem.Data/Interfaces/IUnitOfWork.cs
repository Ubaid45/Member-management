using ManagementSystem.Data.Models;

namespace ManagementSystem.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Account> Accounts { get; }
        void Commit();
    }
}
using ManagementSystem.Data.Models;

namespace ManagementSystem.Data
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Account> Accounts { get; }
        void Commit();
    }
}
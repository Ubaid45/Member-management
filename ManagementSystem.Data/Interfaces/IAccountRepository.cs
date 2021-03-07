using ManagementSystem.Data.Models;

namespace ManagementSystem.Data.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        public bool CollectPoints(int accountId, double points);
        public bool RedeemPoints(int accountId, double points);
    }
}
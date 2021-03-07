using ManagementSystem.Data.Models;

namespace ManagementSystem.Data
{
    public interface IAccountRepository
    {
            public bool AddAccount(Account accountDetails);
            public bool DeleteAccount(int accountId);
            public bool UpdateAccount(Account accountDetails);
            public bool GetAccountById(int accountId);
            public bool GetAllAccounts();
    }
}
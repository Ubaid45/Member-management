using System;
using System.Diagnostics;
using System.Linq;
using ManagementSystem.Data.Interfaces;
using ManagementSystem.Data.Models;

namespace ManagementSystem.Data
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(UsersManagementDbContext context) : base(context)
        {
            
        }

        public bool CollectPoints(int accountId, double points)
        {
            try
            {
                var accountDetails = Get(m => m.AccountId == accountId, null, "User").SingleOrDefault();
                if (accountDetails != null) accountDetails.Balance += points;
                Update(accountDetails);
                return true;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            
        }

        public bool RedeemPoints(int accountId, double points)
        {
            try 
            {
                var accountDetails = Get(m => m.AccountId == accountId, null, "User").SingleOrDefault();
                if (accountDetails != null) accountDetails.Balance -= points;
                Update(accountDetails);
                return true;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
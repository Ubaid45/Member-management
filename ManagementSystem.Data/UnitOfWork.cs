using System;
using System.Diagnostics;
using ManagementSystem.Data.Interfaces;
using ManagementSystem.Data.Models;

namespace ManagementSystem.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly UsersManagementDbContext _dbContext;
        private IUserRepository _users;
        private IAccountRepository _accounts;

        public UnitOfWork(UsersManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository Users
        {
            get { return _users ??= new UserRepository(_dbContext); }
        }

        public IAccountRepository Accounts
        {
            get { return _accounts ??= new AccountRepository(_dbContext); }
        }

        public int Commit()
        {
            try
            { 
                return _dbContext.SaveChanges();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return 0;
            }
           
        }
    }
}
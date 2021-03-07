using System;
using System.Diagnostics;
using ManagementSystem.Data.Interfaces;
using ManagementSystem.Data.Models;

namespace ManagementSystem.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly UsersManagementDbContext _dbContext;
        private IBaseRepository<User> _users;
        private IAccountRepository _accounts;

        public UnitOfWork(UsersManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IBaseRepository<User> Users
        {
            get { return _users ??= new BaseRepository<User>(_dbContext); }
        }

        public IAccountRepository Accounts
        {
            get { return _accounts ??= new AccountRepository(_dbContext); }
        }

        public void Commit()
        {
            try
            {
                 _dbContext.SaveChanges();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
           
        }
    }
}
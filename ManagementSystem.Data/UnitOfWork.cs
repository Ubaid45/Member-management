using System;
using System.Diagnostics;
using ManagementSystem.Data.Interfaces;
using ManagementSystem.Data.Models;

namespace ManagementSystem.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly UsersManagementDbContext _dbContext;
        private BaseRepository<User> _users;
        private BaseRepository<Account> _accounts;

        public UnitOfWork(UsersManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<User> Users
        {
            get { return _users ??= new BaseRepository<User>(_dbContext); }
        }

        public IRepository<Account> Accounts
        {
            get { return _accounts ??= new BaseRepository<Account>(_dbContext); }
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
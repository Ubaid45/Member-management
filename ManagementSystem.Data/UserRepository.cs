using System.Collections.Generic;
using System.Linq;
using ManagementSystem.Data.DTOs;
using ManagementSystem.Data.Interfaces;
using ManagementSystem.Data.Models;

namespace ManagementSystem.Data
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(UsersManagementDbContext context) : base(context)
        {
        }

        public List<ExportUserDto> GetFilteredDataToExport()
        {
            var queryableUsers = (from ac in Context.Accounts
                join u in Context.Users 
                on ac.UserId equals u.UserId 
                where ac.Balance >= 20 && ac.Status == Status.Inactive
                select  u).Distinct();
            
            var userList = (from u in queryableUsers
                select new ExportUserDto
                {
                    UserId = u.UserId,
                   Accounts = (from account in u.Accounts
                       where account.Balance >= 20 && account.Status == Status.Inactive
                           select new ExportAccountDto{
                                Balance = account.Balance, 
                                 Status = account.Status, 
                                 AccountId = account.AccountId, 
                                AccountName = account.AccountName,
                           }).ToList(),
                     UserName = u.UserName,
                    Address = u.Address
                }).Distinct().ToList();

            return userList;
        }
    }
}
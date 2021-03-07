using System;
using System.Collections.Generic;
using System.Linq;
using ManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Data
{
    public class UserRepository: IUserRepository
    {
        private readonly UsersManagementDbContext _context;

        public UserRepository(UsersManagementDbContext context)
        {
            _context = context;
        }
        
        public bool AddUser(User userDetails)
        {
            _context.Users.Add(userDetails);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUser(int userId)
        {
            _context.Users.Remove(GetUserById(userId));
            _context.SaveChanges();
            return true;
        }

        public bool UpdateUser(User userDetails)
        {
            try
            {
                _context.Users.Update(userDetails);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Include("Accounts").FirstOrDefault(m => m.UserId == userId);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Include("Accounts").Select(a => a).ToList();
        }
    }
}
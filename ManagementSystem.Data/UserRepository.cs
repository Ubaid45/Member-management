using System.Collections.Generic;
using System.Linq;
using ManagementSystem.Data.Models;

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
            throw new System.NotImplementedException();
        }

        public bool DeleteUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateUser(User userDetails)
        {
            throw new System.NotImplementedException();
        }

        public bool GetUserById(int userId)
        {
            throw new System.NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}
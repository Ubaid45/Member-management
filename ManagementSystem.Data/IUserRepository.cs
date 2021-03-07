using System.Collections.Generic;
using ManagementSystem.Data.Models;

namespace ManagementSystem.Data
{
    public interface IUserRepository
    {
        public User AddUser(User userDetails);
        public bool DeleteUser(int userId);
        public User UpdateUser(User userDetails);
        public User GetUserById(int userId);
        public List<User> GetAllUsers();
    }
}
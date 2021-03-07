using System.Collections.Generic;
using ManagementSystem.Data.Models;

namespace ManagementSystem.Data
{
    public interface IUserRepository
    {
        public bool AddUser(User userDetails);
        public bool DeleteUser(int userId);
        public bool UpdateUser(User userDetails);
        public bool GetUserById(int userId);
        public List<User> GetAllUsers();
    }
}
using System.Collections.Generic;
using ManagementSystem.Data.Models;

namespace MemberManagement.Services
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public User AddUser(User userDetails);
        public bool DeleteUser(int userId);
        public User UpdateUser(User userDetails);
        public User GetUserById(int userId);
    }
}
using System.Collections.Generic;
using ManagementSystem.Data.Models;

namespace MemberManagement.Services
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
    }
}
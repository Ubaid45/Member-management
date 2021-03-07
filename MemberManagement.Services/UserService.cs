using System.Collections.Generic;
using ManagementSystem.Data;
using ManagementSystem.Data.Models;

namespace MemberManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public List<User> GetAllUsers()
        {
            var data = _userRepository.GetAllUsers();
            return data;
        }
    }
}
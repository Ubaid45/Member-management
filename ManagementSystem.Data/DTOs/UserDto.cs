using System.Collections.Generic;
using ManagementSystem.Data.Models;

namespace ManagementSystem.Data.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
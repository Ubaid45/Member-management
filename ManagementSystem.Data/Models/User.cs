using System.Collections.Generic;

namespace ManagementSystem.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Account> AccountList { get; set; }
    }
}
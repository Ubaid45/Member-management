using ManagementSystem.Data.Models;

namespace ManagementSystem.Data.DTOs
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public double Balance { get; set; }
        public Status Status { get; set; } 
        public string AccountOwnerName { get; set; }
        public int UserId { get; set; }
    }
}
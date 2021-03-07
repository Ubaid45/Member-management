using ManagementSystem.Data.Models;

namespace ManagementSystem.Data.DTOs
{
    public class ExportAccountDto
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public double Balance { get; set; }
        public Status Status { get; set; } 
    }
}
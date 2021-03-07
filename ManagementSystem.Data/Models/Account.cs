using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Data.Models
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public double Balance { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Data.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
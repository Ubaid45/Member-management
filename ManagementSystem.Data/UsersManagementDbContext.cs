using ManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Data
{
    public class UsersManagementDbContext : DbContext
    {
        public UsersManagementDbContext(DbContextOptions<UsersManagementDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
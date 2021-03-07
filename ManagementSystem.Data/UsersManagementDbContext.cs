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

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasOne(p => p.User)
                .WithMany(b => b.Accounts);
        }*/
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Account>()
                .HasOne<User>(s => s.User)
                .WithMany(g => g.Accounts)
                .HasForeignKey(s => s.UserId);            
        }
    }
}
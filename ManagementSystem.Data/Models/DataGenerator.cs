using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManagementSystem.Data.Models
{
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new UsersManagementDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<UsersManagementDbContext>>()))
            {
                // Look for any users already in database.
                if (context.Users.Any())
                {
                    return;   // Database has been seeded
                }

                context.Users.AddRange(
                    new User
                    {
                        Id = 1,
                        Name = "Ubaid",
                        AccountList = new List<Account>()
                    },
                    new User
                    {
                        Id = 2,
                        Name = "Sibgha",
                        AccountList = new List<Account>()
                    },
                    new User
                    {
                        Id = 3,
                        Name = "Ali",
                        AccountList = new List<Account>()
                    },
                    new User
                    {
                        Id = 4,
                        Name = "Umer",
                        AccountList = new List<Account>()
                    },
                    new User
                    {
                        Id = 5,
                        Name = "Florian",
                        AccountList = new List<Account> ()
                    });

                context.SaveChanges();
            }
        }
    }
}
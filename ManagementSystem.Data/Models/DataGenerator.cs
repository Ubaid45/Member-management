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
                        Address = "Homburger Landstr. 207, 60435, Frankfurt am Main, Germany",
                    },
                    new User
                    {
                        Id = 2,
                        Name = "Sibgha",
                        Address = "Hombuger str. 290, 60435, Frankfurt am Main, Germany",
                        Accounts = new List<Account>()
                    },
                    new User
                    {
                        Id = 3,
                        Name = "Ali",
                        Address = "Nieder Ramstadter str. 64285, Darmstadt, Germany",
                        Accounts = new List<Account>()
                    },
                    new User
                    {
                        Id = 4,
                        Name = "Umer",
                        Address = "Am Karlshof. 64285, Darmstadt, Germany",
                        Accounts = new List<Account>()
                    },
                    new User
                    {
                        Id = 5,
                        Name = "Florian",
                        Address = "Weisener str. 64287, Darmstadt, Germany",
                        Accounts = new List<Account> ()
                    });
                
                context.Accounts.AddRange( new Account
                {
                    Id = 1,
                    Balance = 15,
                    Name = "flight",
                    Status = Status.Active,
                    UserId = 1
                },
                new Account{
                    Id = 2,
                    Balance = 135,
                    Name = "Mcdonalds",
                    Status = Status.Active,
                    UserId = 1
                },
                new Account{
                    Id = 3,
                    Balance = 115,
                    Name = "flight",
                    Status = Status.Active,
                    UserId = 2
                },
                new Account{
                    Id = 4,
                    Balance = 150,
                    Name = "Burger King",
                    Status = Status.Active,
                    UserId = 3
                },
                new Account{
                    Id = 5,
                    Balance = 155,
                    Name = "New Yorker",
                    Status = Status.Active,
                    UserId = 3
                },
                new Account{
                    Id = 6,
                    Balance = 135,
                    Name = "H&M",
                    Status = Status.Inactive,
                    UserId = 3
                },
                new Account{
                    Id = 7,
                    Balance = 515,
                    Name = "Lufthansa",
                    Status = Status.Active,
                    UserId = 4
                }
                    );

                context.SaveChanges();
            }
        }
    }
}
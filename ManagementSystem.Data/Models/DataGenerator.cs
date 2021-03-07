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
                        UserId = 1,
                        UserName = "Ubaid",
                        Address = "Homburger Landstr. 207, 60435, Frankfurt am Main, Germany",
                    },
                    new User
                    {
                        UserId = 2,
                        UserName = "Sibgha",
                        Address = "Hombuger str. 290, 60435, Frankfurt am Main, Germany",
                        Accounts = new List<Account>()
                    },
                    new User
                    {
                        UserId = 3,
                        UserName = "Ali",
                        Address = "Nieder Ramstadter str. 64285, Darmstadt, Germany",
                        Accounts = new List<Account>()
                    },
                    new User
                    {
                        UserId = 4,
                        UserName = "Umer",
                        Address = "Am Karlshof. 64285, Darmstadt, Germany",
                        Accounts = new List<Account>()
                    },
                    new User
                    {
                        UserId = 5,
                        UserName = "Florian",
                        Address = "Weisener str. 64287, Darmstadt, Germany",
                        Accounts = new List<Account> ()
                    });
                
                context.Accounts.AddRange( new Account
                {
                    AccountId = 1,
                    Balance = 15,
                    AccountName = "flight",
                    Status = Status.Active,
                    UserId = 1
                },
                new Account{
                    AccountId = 2,
                    Balance = 135,
                    AccountName = "Mcdonalds",
                    Status = Status.Active,
                    UserId = 1
                },
                new Account{
                    AccountId = 3,
                    Balance = 115,
                    AccountName = "flight",
                    Status = Status.Active,
                    UserId = 2
                },
                new Account{
                    AccountId = 4,
                    Balance = 150,
                    AccountName = "Burger King",
                    Status = Status.Active,
                    UserId = 3
                },
                new Account{
                    AccountId = 5,
                    Balance = 155,
                    AccountName = "New Yorker",
                    Status = Status.Active,
                    UserId = 3
                },
                new Account{
                    AccountId = 6,
                    Balance = 135,
                    AccountName = "H&M",
                    Status = Status.Inactive,
                    UserId = 3
                },
                new Account{
                    AccountId = 7,
                    Balance = 515,
                    AccountName = "Lufthansa",
                    Status = Status.Active,
                    UserId = 4
                }
                    );

                context.SaveChanges();
            }
        }
    }
}
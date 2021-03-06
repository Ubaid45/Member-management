using ManagementSystem.Data;
using ManagementSystem.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MemberManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            //1. Find the service layer within our scope.
            using (var scope = host.Services.CreateScope())
            {
                //3. Get the instance of UsersManagementDbContext in our services layer
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<UsersManagementDbContext>();

                //3. Call the DataGenerator to create sample data
                DataGenerator.Initialize(services);
            }
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
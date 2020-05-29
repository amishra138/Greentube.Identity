using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Greentube.Identity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                SeedUsers(scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>());
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            var arvind = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "arvind",
                Email = "arvind.mishra138@gmail.com",
            };


            var greentube = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "greentube",
                Email = "greentube@gmail.com",
            };


            userManager.CreateAsync(arvind, PassGenerate(arvind));
            userManager.CreateAsync(greentube, PassGenerate(greentube));
        }

        public static string PassGenerate(IdentityUser user)
        {
            var passHash = new PasswordHasher<IdentityUser>();
            return passHash.HashPassword(user, "password");
        }
    }
}

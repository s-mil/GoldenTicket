using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GoldenTicket.Data;
using GoldenTicket.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GoldenTicket
{
    /// <summary>
    /// Our program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point of program
        /// </summary>
        /// <param name="args">command line arguments</param>
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<GoldenTicketContext>();
                var configuration = services.GetRequiredService<IConfiguration>();
                var userManager = services.GetRequiredService<UserManager<Technician>>();
                var roleManager = services.GetService<RoleManager<IdentityRole>>();

                if (configuration.GetValue<bool>("useSeedData"))
                {
                    SeedData.Initialize(context, userManager, roleManager);
                }
                else
                {
                    context.Database.Migrate();
                    var role = roleManager.FindByNameAsync(DataConstants.AdministratorRole).Result;
                    if (role == null)
                    {
                        roleManager.CreateAsync(new IdentityRole(DataConstants.AdministratorRole));
                    }
                }
                var admin = userManager.FindByNameAsync(DataConstants.RootUsername).Result;
                if (admin == null)
                {
                    admin = new Technician
                    {
                        UserName = DataConstants.RootUsername,
                        FirstName = DataConstants.RootUsername,
                        LastName = DataConstants.RootUsername,
                        DateAdded = DateTime.Now.AddYears(-2),
                        IsAdmin = true
                    };
                    userManager.CreateAsync(admin, configuration["adminPassword"]).Wait();
                }
                if (!userManager.IsInRoleAsync(admin, DataConstants.AdministratorRole).Result)
                {
                    userManager.AddToRoleAsync(admin, DataConstants.AdministratorRole).Wait();
                }
            }

            host.Run();
        }

        /// <summary>
        /// builds the web host
        /// </summary>
        /// <param name="args">command line arguments</param>
        /// <returns>errors</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:5000")
                .UseStartup<Startup>()
                .Build();
    }
}

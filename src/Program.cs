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
                var userManager = services.GetRequiredService<UserManager<Technician>>();
                context.Database.Migrate();

                try
                {
                    SeedData.Initialize(context, userManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                    throw ex;
                }

                var configuration = host.Services.GetRequiredService<IConfiguration>();
                if (configuration.GetValue<bool>("useSeedData"))
                {
                    SeedData.Initialize(context, userManager);
                }
                userManager.CreateAsync(new Technician { UserName = "admin", FirstName = "admin", LastName = "admin" }, configuration["adminPassword"]).Wait();
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
                .UseUrls("http://*.goldenticket.biz:80")
                .UseStartup<Startup>()
                .Build();
    }
}

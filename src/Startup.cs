using GoldenTicket.Data;
using GoldenTicket.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GoldenTicket
{
    /// <summary>
    /// The class for starting the application. Gets the HostingEnvironment and Configuration
    /// </summary>
    public class Startup
    {
        private IHostingEnvironment _hostingEnvironment { get; set; }

        private IConfiguration _configuration { get; set; }

        /// <summary>
        /// Creates a new instance of this class
        /// </summary>
        /// <param name="env">The hosting environment</param>
        /// <param name="config">The configuration settings for the application</param>
        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            _hostingEnvironment = env;
            _configuration = config;
        }

        /// <summary>
        /// Configures the services for this application
        /// </summary>
        /// <param name="services">The service container for this application</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<GoldenTicketContext>(options => options.UseSqlite(_configuration["connectionString"]));

            services.AddIdentity<Technician, IdentityRole>().AddEntityFrameworkStores<GoldenTicketContext>().AddDefaultTokenProviders();
        }

        /// <summary>
        /// Configures the application pipeline and pre-startup operations
        /// </summary>
        /// <param name="app">For configuring the application pipeline</param>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <param name="applicationLifetime"></param>
        public void Configure(IApplicationBuilder app, GoldenTicketContext context, ILogger<Startup> logger, IApplicationLifetime applicationLifetime, UserManager<Technician> userManager)
        {
            context.Database.Migrate();
            logger.LogInformation("Database created and migrated to newest version.");

            if (_hostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedData.Initialize(context, userManager);
            }

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

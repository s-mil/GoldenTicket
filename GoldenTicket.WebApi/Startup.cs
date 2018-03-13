using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoldenTicket.WebApi
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
            services.AddDbContext<Models.GoldenTicketContext>(options => options.UseSqlite(_configuration["connectionString"]));
        }

        /// <summary>
        /// Configures the application pipeline and pre-startup operations
        /// </summary>
        /// <param name="app">For configuring the application pipeline</param>
        public void Configure(IApplicationBuilder app)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello Devin!");
            });
        }
    }
}

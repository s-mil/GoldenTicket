using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoldenTicket.WebApi
{
    public class Startup
    {
        private IHostingEnvironment _hostingEnvironment { get; set; }
        private IConfiguration _configuration { get; set; }

        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            _hostingEnvironment = env;
            _configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Models.GoldenTicketContext>(options => options.UseSqlite(_configuration["connectionString"]));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
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

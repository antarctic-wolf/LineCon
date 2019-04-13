using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LineCon.Models;
using LineCon.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LineCon
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddOptions();
            services.Configure<AppSettings>(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<ITicketWindowService, TicketWindowService>();
            services.AddScoped<ITicketQueueService, TicketQueueService>();

            var connectionString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";
            services.AddDbContext<LineConContext>(options => options.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //app.UseConIdentifierMiddleware();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "home",
                    template: "{controller=Home}/{action=Index}");

                routes.MapRoute(
                    name: "app",
                    template: "{conIdentifier}/{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" },
                    constraints: new { conIdentifier = new ConIdentifierConstraint() });
            });
        }
    }
}

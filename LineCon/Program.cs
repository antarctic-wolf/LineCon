using System;
using LineCon.Data;
using LineCon.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LineCon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    DbInitializer.Initialize(scope.ServiceProvider
                        .GetRequiredService<LineConContext>());
                }
                catch (Exception e)
                {
                    scope.ServiceProvider.GetRequiredService<ILogger<Program>>()
                        .LogError(e.Message, "Error seeding LineConContext");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

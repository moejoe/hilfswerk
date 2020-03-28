using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hilfswerk.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class Program
    {
        private static async Task MigrateDatabase(IHost host)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<HilfswerkDbContext>())
                {
                    await context.Database.MigrateAsync();
                }
            }
        }

        private static async Task InsertTestData(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                using (var context = services.GetRequiredService<HilfswerkDbContext>())
                {
                    if (context.Helfer.Any())
                    {
                        return;
                    }
                    foreach (var helfer in TestData.Helfer)
                    {
                        context.Helfer.Add(helfer);
                    }
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await MigrateDatabase(host);
            await InsertTestData(host);
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

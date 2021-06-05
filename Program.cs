using CandyShop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var applicationHost = CreateHostBuilder(args).Build();
            using(var scope = applicationHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                //Initialize Database
                SeedDb.InitializeDbIfEmpty(services);
            }


            applicationHost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

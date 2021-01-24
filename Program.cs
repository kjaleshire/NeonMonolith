using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NeonMonolith.Contexts;

namespace NeonMonolith
{
    class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return new HostBuilder()
                .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
                {
                    configurationBuilder.AddEnvironmentVariables(prefix: "NEON_");
                })
                .ConfigureServices((hostBuilderContext, serviceCollection) =>
                {
                    serviceCollection.AddOptions();
                    serviceCollection.Configure<AppConfig>(hostBuilderContext.Configuration);

                    serviceCollection.AddEntityFrameworkNpgsql();
                    var databaseString = hostBuilderContext.Configuration["DATABASE_STRING"];
                    serviceCollection.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(databaseString));
                    serviceCollection.AddHostedService<SyncService>();
                })
                .ConfigureLogging((hostContext, loggingBuilder) =>
                {
                    loggingBuilder.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                    loggingBuilder.AddConsole();
                });
        }
        public static async Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args);

            await builder.RunConsoleAsync();
        }
    }
}

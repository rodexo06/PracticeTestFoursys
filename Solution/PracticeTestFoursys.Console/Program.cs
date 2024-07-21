using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PracticeTestFoursys.Console.Contracts;
using PracticeTestFoursys.Infra;
using PracticeTestFoursys.Infra.Context;

class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        ApplyMigrations(host);
        var dataProcessor = host.Services.GetRequiredService<IJsonDataProcessor>();
        await dataProcessor.ProcessDataAsync();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                config.SetBasePath(Directory.GetCurrentDirectory());
                      //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                      //.AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;
                services.AddInfra(configuration);
                string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
                string apiUrl = Environment.GetEnvironmentVariable("ApiUrl");

                services.AddHttpClient<IApiDataFetcher, ApiDataFetcher>(client =>
                {
                    client.BaseAddress = new Uri(apiUrl);
                });
                services.AddScoped<IJsonDataProcessor, JsonDataProcessor>();
                services.AddDbContext<PositionContext>(options =>
                    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("PracticeTestFoursys.Infra")));
            });


    private static void ApplyMigrations(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PositionContext>();
            db.Database.Migrate();
        }
    }
}
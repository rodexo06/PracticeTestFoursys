using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
                    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("PracticeTestFoursys.Infra"))
                    .LogTo(Console.WriteLine, LogLevel.Warning)
                    );
            })
                       .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                        logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                        //logging.SetMinimumLevel(LogLevel.Warning, LogLevel.Error, LogLevel.Information);
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
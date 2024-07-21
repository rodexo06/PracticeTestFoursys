using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PracticeTestFoursys.Console.Contracts;
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
            .ConfigureServices((_, services) =>
            {
                string apiUrl = Environment.GetEnvironmentVariable("ApiUrl");
                string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
                services.AddHttpClient<IApiDataFetcher, ApiDataFetcher>(client =>
                {
                    client.BaseAddress = new Uri(apiUrl);
                });
                services.AddScoped<IJsonDataProcessor, JsonDataProcessor>();
                services.AddDbContext<PositionContext>(options =>
                {
                    options.UseNpgsql(connectionString);
                });
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
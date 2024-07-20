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
        var dataProcessor = host.Services.GetRequiredService<IJsonDataProcessor>();
        await dataProcessor.ProcessDataAsync();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddHttpClient<IApiDataFetcher, ApiDataFetcher>(client =>
                {
                    client.BaseAddress = new Uri("https://api.andbank.com.br");
                });
                services.AddScoped<IJsonDataProcessor, JsonDataProcessor>();
                services.AddDbContext<PracticeTestFoursysContext>(options =>
                {
                    options.UseNpgsql("YourConnectionString");
                });
            });
}
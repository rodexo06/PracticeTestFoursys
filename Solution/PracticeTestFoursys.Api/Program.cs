using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PracticeTestFoursys.Infra.Context;
using PracticeTestFoursys.Infra;
using System.Globalization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");


var configuration = builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .Build();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Practice Test Foursys", Version = "v1" });
    option.DescribeAllParametersInCamelCase();
});
builder.Services.AddDbContext<PositionContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgre")));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


builder.Services.AddInfra(configuration);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<PositionContext>();

    dbContext.Database.Migrate();
}


app.Run();

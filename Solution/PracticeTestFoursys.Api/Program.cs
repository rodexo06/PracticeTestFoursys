using Microsoft.EntityFrameworkCore;
using PracticeTestFoursys.Infra.Context;
using PracticeTestFoursys.Infra;
using PracticeTestFoursys.Application;
using System.Globalization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");


var configuration = builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).Build();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Practice Test Foursys", Version = "v1" });
    option.DescribeAllParametersInCamelCase();
});
builder.Services.AddDbContext<PositionContext>(options =>
                    options.UseNpgsql(connectionString));
builder.Services.AddApplication();
builder.Services.AddInfra(configuration);

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var dbContext = services.GetRequiredService<PositionContext>();
//    dbContext.Database.Migrate();
//}
app.Run();

using AlertManagement.AlertsQueueListener.Configurations;
using AlertManagement.FlightsQueueService.Configurations;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCacheService(builder.Configuration);
builder.Services.Configure<AlertsQueueOptions>(
    builder.Configuration.GetSection("Rabbit:AlertsQueue"));

builder.Services.Configure<FlightsQueueOptions>(
    builder.Configuration.GetSection("Rabbit:FlightsQueue"));


var app = builder.Build(); 
 
app.MapControllers();

app.Run();

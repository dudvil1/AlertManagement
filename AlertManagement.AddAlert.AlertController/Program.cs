using AlertManagement.AddAlert.AlertController.Extensions;
using AlertManagement.AddAlert.AlertController.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBusinessLogic();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseJwtValidation();

app.UseAuthorization();
app.MapControllers();
app.Run();

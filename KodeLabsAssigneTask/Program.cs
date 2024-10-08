using Application;
using Hangfire;
using Infrastructure.Persistence;
using KodeLabsAssigneTask.OData;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureOData();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddServices();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHangfireDashboard(); // Optional: For monitoring jobs
    app.UseSwagger();
    app.UseSwaggerUI();
}
Log.Information("App started!!");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

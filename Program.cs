using DeliveryApp.Services.Interfaces;
using DeliveryApp.Services.Implementations;
using DeliveryApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IOrderProcessor, OrderProcessor>();
builder.Services.AddTransient<IAccessLog, AccessLog>();
builder.Services.AddSingleton<ILoggerService, LoggerService>(provider => new LoggerService("logfile.txt"));

builder.Services.AddDbContext<DeliveryAppDbContext>(options => {
    var connectionString = builder.Configuration.GetConnectionString("DeliveryAppDb");

    options.UseSqlServer(connectionString);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

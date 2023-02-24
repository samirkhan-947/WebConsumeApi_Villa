using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using WebConsumeApi_Villa;
using WebConsumeApi_Villa.Data;
using WebConsumeApi_Villa.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/villas.txt",rollingInterval:RollingInterval.Day).CreateLogger();
builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddTransient<ILogging, Logger>();

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

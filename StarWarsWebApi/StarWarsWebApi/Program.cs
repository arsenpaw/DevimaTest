using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

using StarWarsWebApi.Repositories;
using WebApplication.Context;
using StarWarsApiCSharp;
using StarWarsWebApi.Interaces;
using WebApplication;
using StarWarsWebApi;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);
var configurationFile = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
       .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
       .Build();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Host.UseSerilog((context, config) =>
{
    var logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
    config
     .ReadFrom.Configuration(configurationFile);


});
builder.Services.AddDbContext<StarWarsContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPeopleRepo, PeopleRepo>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
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

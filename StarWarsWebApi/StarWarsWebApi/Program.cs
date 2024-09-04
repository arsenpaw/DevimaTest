using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configurationFile = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
       .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
       .Build();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Host.UseSerilog((context, config) =>
{
    var logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
    config
     .ReadFrom.Configuration(configurationFile);


});
builder.Services.AddScoped<ISwapiPeapleRepo, SwapiPeapleRepo>();
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

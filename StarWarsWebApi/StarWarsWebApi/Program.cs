using System.Reflection;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using StarWarsWebApi.Repositories;
using StarWarsWebApi.Context;
using StarWarsApiCSharp;
using StarWarsWebApi.Common;
using StarWarsWebApi.Extensions;
using StarWarsWebApi.Interaces;

using StarWarsWebApi.Helper;
using StarWarsWebApi.Models;
using StarWarsWebApi.Services;
using StarWarsWebApi.Validation;

var builder = WebApplication.CreateBuilder(args);
var configurationFile = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
       .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
       .Build();


// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Host.UseSerilog((context, config) =>
{
    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
    config
     .ReadFrom.Configuration(configurationFile);
});

builder.Services.AddDbContext<StarWarsContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<StarWarsContext>();

builder.Services.AddScoped<UserManager<IdentityUser>, CustomUserManager<IdentityUser>>();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configurationFile["JWT:ValidAudience"],
            ValidIssuer = configurationFile["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationFile["JWT:Secret"]))
        };
    });
builder.Services.AddOptions<BearerTokenOptions>(IdentityConstants.BearerScheme).Configure(options =>
{
    var time =Double.Parse(configurationFile["JWT:ExpiryMinutes"]);
    options.BearerTokenExpiration = TimeSpan.FromSeconds(time);
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();
var app = builder.Build();

app.InitializeDatabase();
await app.AddRolesToDb();
app.MapGroup("/identity").MapIdentityApi<IdentityUser>();
app.UseSwagger();
app.UseSwaggerUI();



app.UseAuthorization();

app.MapControllers();

app.Run();


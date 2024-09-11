using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StarWarsWebApi.Context;
using StarWarsWebApi.Models;

namespace StarWarsWebApi.Extensions;

public static class ApplicationbuilderExtensions
{
    public static void InitializeDatabase(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<StarWarsContext>().Database.Migrate();
        }
    }

    public static async Task AddRolesToDb(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = new[] { UserRoles.User, UserRoles.Admin };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
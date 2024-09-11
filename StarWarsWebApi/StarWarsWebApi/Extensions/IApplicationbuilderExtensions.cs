using Microsoft.EntityFrameworkCore;
using StarWarsWebApi.Context;

namespace StarWarsWebApi.Extensions;

public static class IApplicationbuilderExtensions
{
    public static void InitializeDatabase(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<StarWarsContext>().Database.Migrate();
        }
    }
}
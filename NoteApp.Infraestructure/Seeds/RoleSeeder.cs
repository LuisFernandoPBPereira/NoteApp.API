using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NoteApp.Infraestructure.Roles;

namespace NoteApp.Infraestructure.Seeds;

public class RoleSeeder
{
    public static async Task Seed(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        var roleAdmin = await roleManager.FindByNameAsync(Role.Admin);
        var roleComum = await roleManager.FindByNameAsync(Role.Comum);

        if (roleAdmin is null)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(Role.Admin));
        }

        if (roleComum is null)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(Role.Comum));
        }
    }
}

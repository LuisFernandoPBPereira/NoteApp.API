using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoteApp.Infraestructure.Data;
using NoteApp.Infraestructure.Models;
using NoteApp.Infraestructure.Roles;

namespace NoteApp.Infraestructure.Seeds;

public class UsuarioSeeder
{
    public static async Task Seed(IServiceProvider services)
    {
        var dbContext = services.GetRequiredService<NoteAppContext>();
        var userManager = services.GetRequiredService<UserManager<Usuarios>>();

        var usuario = await dbContext.Usuarios.Where(x => x.UserName == "admin").FirstOrDefaultAsync();

        if (usuario is null)
        {
            usuario = new Usuarios
            {
                Nome = "Admin",
                UserName = "admin",
                Email = "admin@example.com",
            };

            await userManager.CreateAsync(usuario, "*Admin123");
            await userManager.AddToRoleAsync(usuario, Role.Admin);
        } 
    }
}

using Microsoft.AspNetCore.Identity;

namespace NoteApp.Infraestructure.Models;

public class Usuarios : IdentityUser<Guid>
{
    public Usuarios()
    {
    }

    public string Nome { get; set; }
}

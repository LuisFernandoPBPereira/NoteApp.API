using NoteApp.Domain.Entities;

namespace NoteApp.Test.Domain;

public class UsuarioTest
{
    [Fact]
    public void AoCriarUsuario_RetornarInstancia()
    {
        var nome = "User";
        var userName = "UserName";
        var email = "email@teste.com";

        var usuario = Usuario.Criar(nome, userName, email);

        Assert.NotNull(usuario);
    }
}

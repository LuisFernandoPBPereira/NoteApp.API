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
    
    [Fact]
    public void AoCriarUsuarioParaAtualizar_RetornarInstancia()
    {
        var nome = "User";
        var email = "email@teste.com";

        var usuario = Usuario.Criar(Guid.NewGuid(), nome, email);

        Assert.NotNull(usuario);
    }

    [Theory]
    [InlineData("", "username", "email@teste.com")]
    [InlineData(null, "username", "email@teste.com")]
    [InlineData("Titulo", "", "email@teste.com")]
    [InlineData("Titulo", null, "email@teste.com")]
    public void AoCriarUsuarioIncorretamente_LancarExcecao(string nome, string userName, string email)
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            var usuario = Usuario.Criar(nome, userName, email);
        });
    }
    
    [Theory]
    [InlineData("Titulo", "userName", "")]
    [InlineData("Titulo", "userName", "email")]
    public void AoCriarUsuarioComEmailIncorreto_LancarExcecao(string nome, string userName, string email)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var usuario = Usuario.Criar(nome, userName, email);
        });
    }
    
    [Theory]
    [InlineData("", "email@teste.com")]
    [InlineData(null, "email@teste.com")]
    public void AoCriarUsuarioParaAtualizarIncorretamente_LancarExcecao(string nome, string email)
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            var usuario = Usuario.Criar(Guid.NewGuid(), nome, email);
        });
    }
    
    [Theory]
    [InlineData("Titulo", "")]
    [InlineData("Titulo", "email")]
    public void AoCriarUsuarioParaAtualizarComEmailIncorreto_LancarExcecao(string nome, string email)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var usuario = Usuario.Criar(Guid.NewGuid(), nome, email);
        });
    }
}

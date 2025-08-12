using NoteApp.Domain.Entities;

namespace NoteApp.UnitTest.Domain;

public class LembreteTest
{
    [Fact]
    public void AoCriarLembrete_RetornarInstancia()
    {
        var titulo = "Meu lembrete";
        var descricao = "Tenho que lembrar o que fazer";
        var dataAlerta = DateTime.Now.AddDays(2);
        var cor = "#FFFFFF";

        var lembrete = Lembrete.Criar(titulo, descricao, dataAlerta, cor, Guid.NewGuid());

        Assert.NotNull(lembrete);
    }

    [Theory]
    [MemberData(nameof(DadosIncorretosArgumentNullLembrete))]
    public void AoCriarLembreteComTituloIncorreto_LancarExcecao(string titulo, string descricao, DateTime dataAlerta, string cor, Guid userId)
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            var lembrete = Lembrete.Criar(titulo, descricao, dataAlerta, cor, userId);
        });
    }
    
    [Theory]
    [MemberData(nameof(DadosIncorretosLembrete))]
    public void AoCriarLembreteComDataAlertaNoPassado_E_CorIncorreta_LancarExcecao(string titulo, string descricao, DateTime dataAlerta, string cor, Guid userId)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var lembrete = Lembrete.Criar(titulo, descricao, dataAlerta, cor, userId);
        });
    }

    public static IEnumerable<object[]> DadosIncorretosArgumentNullLembrete()
    {
        yield return new object[] { "", "", DateTime.Now.AddDays(2), "#FFFFFF", Guid.NewGuid() };
        yield return new object[] { null!, "", DateTime.Now.AddDays(2), "#FFFFFF", Guid.NewGuid() };
    }
    
    public static IEnumerable<object[]> DadosIncorretosLembrete()
    {
        yield return new object[] { "Titulo", "", DateTime.Now.AddDays(-2), "#FFFFFF", Guid.NewGuid() };
        yield return new object[] { "Título", "", DateTime.Now.AddDays(2), null!, Guid.NewGuid() };
        yield return new object[] { "Título", "", DateTime.Now.AddDays(2), "cor incorreta", Guid.NewGuid() };
    }
}

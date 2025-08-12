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
}

using System.Text.RegularExpressions;

namespace NoteApp.Domain.Entities;

public class Lembrete
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string? Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataModificacao { get; set; }
    public DateTime? DataAlerta { get; set; }
    public string Cor { get; set; }
    public Guid UserId { get; set; }

    public Lembrete()
    {
        
    }

    public Lembrete(Guid id, string titulo, string? descricao, DateTime dataCriacao, DateTime? dataModificacao, DateTime? dataAlerta, string cor, Guid userId)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        DataCriacao = dataCriacao;
        DataModificacao = dataModificacao;
        DataAlerta = dataAlerta;
        Cor = cor;
        UserId = userId;
    }

    public static Lembrete Criar(string titulo, string? descricao, DateTime? dataAlerta, string cor, Guid userId)
    {
        try
        {
            Validar(titulo, dataAlerta, cor);

            var lembrete = new Lembrete(Guid.NewGuid(), titulo, descricao, DateTime.Now, dataModificacao: null, dataAlerta, cor, userId);

            return lembrete;
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static void Validar(string titulo, DateTime? dataAlerta, string cor)
    {
        if (string.IsNullOrWhiteSpace(titulo))
        {
            throw new ArgumentNullException(nameof(titulo), "Título inválido");
        }

        if (dataAlerta.HasValue && dataAlerta.Value.Date < DateTime.Now.Date)
        {
            throw new ArgumentException("Data de Alerta deve ser no futuro", nameof(dataAlerta));
        }

        if (!Regex.IsMatch(cor, @"^#[0-9A-Fa-f]{6}$"))
        {
            throw new ArgumentException("A cor deve estar em hexadecimal", nameof(cor));
        }
    }
}

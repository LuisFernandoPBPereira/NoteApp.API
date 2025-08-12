namespace NoteApp.Infraestructure.Models;

public class Lembretes
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataModificacao{ get; set; }
    public DateTime? DataAlerta{ get; set; }
    public string Cor { get; set; }
    public Guid UserId { get; set; }

    public Lembretes()
    {
        
    }
}

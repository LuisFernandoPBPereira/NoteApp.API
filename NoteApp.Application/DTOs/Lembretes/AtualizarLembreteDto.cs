namespace NoteApp.Application.DTOs.Lembretes;

public record AtualizarLembreteDto(string Titulo, string Descricao, DateTime? DataAlerta, string Cor, Guid UserId);

namespace NoteApp.API.DTOs.Lembretes;

public record CriarLembreteDto(string Titulo, string Descricao, DateTime? DataAlerta, string Cor, Guid UserId);

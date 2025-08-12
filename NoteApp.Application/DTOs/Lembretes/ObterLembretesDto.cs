namespace NoteApp.Application.DTOs.Lembretes;

public record ObterLembretesDto(int Take, int Skip, Guid? UserId);

using NoteApp.Application.DTOs.Lembretes;
using NoteApp.Domain.Entities;
using NoteApp.Domain.Repositories;

namespace NoteApp.Application.UseCases.Lembretes;

public class ObterLembretePorIdUseCase
{
    private readonly ILembreteRepository _repository;

    public ObterLembretePorIdUseCase(ILembreteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Lembrete?> Executar(Guid lembreteId, ObterLembretePorIdDto lembreteDto, CancellationToken cancellationToken)
    {
        var lembrete = await _repository.ObterLembretePorId(lembreteId, cancellationToken);

        if (lembrete is null || lembrete.UserId != lembreteDto.UserId)
        {
            return null;
        }

        return lembrete;
    }
}

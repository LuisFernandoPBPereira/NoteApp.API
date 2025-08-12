using NoteApp.Application.DTOs.Lembretes;
using NoteApp.Domain.Repositories;

namespace NoteApp.Application.UseCases.Lembretes;

public class RemoverLembreteUseCase
{
    private readonly ILembreteRepository _repository;

    public RemoverLembreteUseCase(ILembreteRepository repository)
    {
        _repository = repository;
    }

    public async Task Executar(Guid lembreteId, RemoverLembreteDto lembreteDto, CancellationToken cancellationToken)
    {
        await _repository.Remover(lembreteId, lembreteDto.UserId, cancellationToken);
    }
}

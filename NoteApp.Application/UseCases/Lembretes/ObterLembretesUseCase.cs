using NoteApp.Application.DTOs.Lembretes;
using NoteApp.Domain.Entities;
using NoteApp.Domain.Repositories;

namespace NoteApp.Application.UseCases.Lembretes;

public class ObterLembretesUseCase
{
    private readonly ILembreteRepository _repository;

    public ObterLembretesUseCase(ILembreteRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Lembrete>> Executar(ObterLembretesDto lembreteDto, CancellationToken cancellationToken)
    {
        var lembretes = await _repository.ObterLembretes(lembreteDto.Take, lembreteDto.Skip, lembreteDto.UserId, cancellationToken);

        return lembretes;
    }
}

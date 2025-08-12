using NoteApp.Application.DTOs.Lembretes;
using NoteApp.Domain.Entities;
using NoteApp.Domain.Repositories;

namespace NoteApp.Application.UseCases.Lembretes;

public class ObterLembretesComVencimentoEmUmaSemanaUseCase
{
    private readonly ILembreteRepository _repository;

    public ObterLembretesComVencimentoEmUmaSemanaUseCase(ILembreteRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Lembrete>> Executar(ObterLembretesDto lembreteDto, CancellationToken cancellationToken)
    {
        var lembretes = await _repository.ObterLembretesComVencimentoEmUmaSemana(lembreteDto.Take, lembreteDto.Skip, lembreteDto.UserId, cancellationToken);

        return lembretes;
    }
}

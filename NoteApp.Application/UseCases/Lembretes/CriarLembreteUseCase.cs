using NoteApp.API.DTOs.Lembretes;
using NoteApp.Domain.Entities;
using NoteApp.Domain.Repositories;

namespace NoteApp.Application.UseCases.Lembretes;

public class CriarLembreteUseCase
{
    private readonly ILembreteRepository _repository;

    public CriarLembreteUseCase(ILembreteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Executar(CriarLembreteDto lembreteDto, CancellationToken cancellationToken)
    {
        var lembrete = Lembrete.Criar(lembreteDto.Titulo, lembreteDto.Descricao, lembreteDto.DataAlerta, lembreteDto.Cor, lembreteDto.UserId);

        await _repository.Criar(lembrete, cancellationToken);

        return lembrete.Id;
    }
}

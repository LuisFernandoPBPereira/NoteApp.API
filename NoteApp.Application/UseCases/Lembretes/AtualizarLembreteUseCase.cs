using NoteApp.Application.DTOs.Lembretes;
using NoteApp.Domain.Entities;
using NoteApp.Domain.Repositories;

namespace NoteApp.Application.UseCases.Lembretes;

public class AtualizarLembreteUseCase
{
    private readonly ILembreteRepository _repository;

    public AtualizarLembreteUseCase(ILembreteRepository repository)
    {
        _repository = repository;
    }

    public async Task Executar(Guid lembreteId, AtualizarLembreteDto lembreteDto, CancellationToken cancellationToken)
    {
        var lembrete = Lembrete.Criar(lembreteId,
                                      lembreteDto.Titulo,
                                      lembreteDto.Descricao,
                                      lembreteDto.DataAlerta,
                                      lembreteDto.Cor,
                                      lembreteDto.UserId);

        await _repository.Atualizar(lembrete, cancellationToken);
    }
}

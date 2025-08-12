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
        var lembrete = new Lembrete()
        {
            Id = lembreteId,
            Titulo = lembreteDto.Titulo,
            Descricao = lembreteDto.Descricao,
            DataAlerta = lembreteDto.DataAlerta,
            Cor = lembreteDto.Cor,
            UserId = lembreteDto.UserId
        };

        await _repository.Atualizar(lembrete, cancellationToken);
    }
}

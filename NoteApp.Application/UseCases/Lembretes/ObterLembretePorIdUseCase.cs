using NoteApp.Application.DTOs.Lembretes;
using NoteApp.Domain.Entities;
using NoteApp.Domain.Repositories;

namespace NoteApp.Application.UseCases.Lembretes;

public class ObterLembretePorIdUseCase
{
    private readonly ILembreteRepository _lembreteRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public ObterLembretePorIdUseCase(ILembreteRepository lembreteRepository, IUsuarioRepository usuarioRepository)
    {
        _lembreteRepository = lembreteRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Lembrete?> Executar(Guid lembreteId, ObterLembretePorIdDto lembreteDto, CancellationToken cancellationToken)
    {
        var lembrete = await _lembreteRepository.ObterLembretePorId(lembreteId, cancellationToken);

        var userRoles = await _usuarioRepository.ObterRoles(lembreteDto.UserId, cancellationToken);

        if ((lembrete is null || lembrete.UserId != lembreteDto.UserId) && !userRoles.Contains("Admin"))
        {
            return null;
        }

        return lembrete;
    }
}

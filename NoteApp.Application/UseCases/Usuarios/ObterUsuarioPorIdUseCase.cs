using NoteApp.Common.Exceptions;
using NoteApp.Domain.Entities;
using NoteApp.Domain.Repositories;

namespace NoteApp.Application.UseCases.Usuarios;

public class ObterUsuarioPorIdUseCase
{
    private readonly IUsuarioRepository _repository;

    public ObterUsuarioPorIdUseCase(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<Usuario> Executar(Guid userId, CancellationToken cancellationToken)
    {
        var usuario = await _repository.ObterUsuarioPorId(userId, cancellationToken);

        if (usuario is null) throw new NotFoundException("Usuário não encontrado");

        return usuario;
    }
}

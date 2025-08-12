using NoteApp.Domain.Repositories;

namespace NoteApp.Application.UseCases.Usuarios;

public class RemoverUsuarioUseCase
{
    private readonly IUsuarioRepository _repository;

    public RemoverUsuarioUseCase(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task Executar(Guid userId, CancellationToken cancellationToken)
    {
        await _repository.Remover(userId, cancellationToken);
    }
}

using NoteApp.Domain.Entities;

namespace NoteApp.Domain.Repositories;

public interface IUsuarioRepository
{
    public Task<Usuario> ObterUsuarioPorId(Guid userId, CancellationToken cancellationToken = default);
    public Task<List<Usuario>> ObterUsuarios(int take, int skip, CancellationToken cancellationToken = default);
    public Task Criar(Usuario usuario, CancellationToken cancellationToken = default);
    public Task Atualizar(Usuario usuario, CancellationToken cancellationToken = default);
    public Task Remover(Guid userId, CancellationToken cancellationToken = default);
}

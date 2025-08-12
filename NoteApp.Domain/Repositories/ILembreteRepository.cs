using NoteApp.Domain.Entities;

namespace NoteApp.Domain.Repositories;

public interface ILembreteRepository
{
    public Task<Lembrete?> ObterLembretePorId(Guid lembreteId, CancellationToken cancellationToken = default);
    public Task<List<Lembrete>> ObterLembretes(int take, int skip, CancellationToken cancellationToken = default);
    public Task<List<Lembrete>> ObterLembretesComVencimentoHoje(int take, int skip, CancellationToken cancellationToken = default);
    public Task<List<Lembrete>> ObterLembretesComVencimentoEmUmaSemana(int take, int skip, CancellationToken cancellationToken = default);
    public Task Criar(Lembrete lembrete, CancellationToken cancellationToken = default);
    public Task Atualizar(Lembrete lembrete, CancellationToken cancellationToken = default);
    public Task Remover(Guid lembreteId, CancellationToken cancellationToken = default);
}

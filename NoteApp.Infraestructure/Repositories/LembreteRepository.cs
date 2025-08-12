using Microsoft.EntityFrameworkCore;
using NoteApp.Common.Exceptions;
using NoteApp.Domain.Entities;
using NoteApp.Domain.Repositories;
using NoteApp.Infraestructure.Data;
using NoteApp.Infraestructure.Models;

namespace NoteApp.Infraestructure.Repositories;

public class LembreteRepository : ILembreteRepository
{
    private readonly NoteAppContext _dbContext;

    public LembreteRepository(NoteAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Atualizar(Lembrete lembrete, CancellationToken cancellationToken = default)
    {
        var lembreteModel = await _dbContext.Lembretes.Where(x => x.Id == lembrete.Id).FirstOrDefaultAsync(cancellationToken);

        if (lembreteModel is null) throw new NotFoundException("Lembrete não encontrado");

        lembreteModel.Titulo = lembrete.Titulo;
        lembreteModel.Descricao = lembrete.Descricao;
        lembreteModel.DataModificacao = DateTime.Now;
        lembreteModel.DataAlerta = lembrete.DataAlerta;
        lembreteModel.Cor = lembrete.Cor;

        _dbContext.Lembretes.Update(lembreteModel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Criar(Lembrete lembrete, CancellationToken cancellationToken = default)
    {
        var lembreteModel = new Lembretes
        {
            Id = lembrete.Id,
            Titulo = lembrete.Titulo,
            Descricao = lembrete.Descricao,
            DataCriacao = lembrete.DataCriacao,
            DataModificacao = lembrete.DataModificacao,
            DataAlerta = lembrete.DataAlerta,
            Cor = lembrete.Cor,
            UserId = lembrete.UserId
        };

        await _dbContext.Lembretes.AddAsync(lembreteModel, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Lembrete?> ObterLembretePorId(Guid lembreteId, CancellationToken cancellationToken = default)
    {
        var lembrete = await _dbContext.Lembretes.Where(x => x.Id == lembreteId).FirstOrDefaultAsync(cancellationToken);

        if (lembrete is null) return null;

        var lembreteEntity = new Lembrete(lembrete.Id,
                                            lembrete.Titulo,
                                            lembrete.Descricao,
                                            lembrete.DataCriacao,
                                            lembrete.DataModificacao,
                                            lembrete.DataAlerta,
                                            lembrete.Cor,
                                            lembrete.UserId);

        return lembreteEntity;
    }

    public async Task<List<Lembrete>> ObterLembretes(int take, int skip, Guid? userId, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Lembretes
            .Select(x => new Lembrete
            {
                Id = x.Id,
                Titulo = x.Titulo,
                Descricao = x.Descricao,
                DataAlerta = x.DataAlerta,
                DataCriacao = x.DataCriacao,
                DataModificacao = x.DataModificacao,
                Cor = x.Cor
            });

        if (userId.HasValue)
        {
            query = query.Where(x => x.UserId == userId);
        }

        var lembretes = await query
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

        return lembretes;
    }

    public async Task<List<Lembrete>> ObterLembretesComVencimentoEmUmaSemana(int take, int skip, Guid? userId, CancellationToken cancellationToken = default)
    {
       var query = _dbContext.Lembretes
            .Where(x => x.DataAlerta.Value.Date == DateTime.Now.Date.AddDays(7))
            .Select(x => new Lembrete
            {
                Id = x.Id,
                Titulo = x.Titulo,
                Descricao = x.Descricao,
                DataAlerta = x.DataAlerta,
                DataCriacao = x.DataCriacao,
                DataModificacao = x.DataModificacao,
                Cor = x.Cor
            });

        if (userId.HasValue)
        {
            query = query.Where(x => x.UserId == userId);
        }

        var lembretes = await query
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

        return lembretes;
    }

    public async Task<List<Lembrete>> ObterLembretesComVencimentoHoje(int take, int skip, Guid? userId, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Lembretes
            .Where(x => x.DataAlerta.Value.Date == DateTime.Now.Date)
            .Select(x => new Lembrete
            {
                Id = x.Id,
                Titulo = x.Titulo,
                Descricao = x.Descricao,
                DataAlerta = x.DataAlerta,
                DataCriacao = x.DataCriacao,
                DataModificacao = x.DataModificacao,
                Cor = x.Cor
            });

        if (userId.HasValue)
        {
            query = query.Where(x => x.UserId == userId);
        }

        var lembretes = await query
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

        return lembretes;
    }

    public async Task Remover(Guid lembreteId, CancellationToken cancellationToken = default)
    {
        var lembrete = await _dbContext.Lembretes.Where(x => x.Id == lembreteId).FirstOrDefaultAsync(cancellationToken);

        if (lembrete is null)
        {
            throw new NotFoundException("Lembrete não encontrado");
        }

        _dbContext.Lembretes.Remove(lembrete);
        await _dbContext.SaveChangesAsync();
    }
}

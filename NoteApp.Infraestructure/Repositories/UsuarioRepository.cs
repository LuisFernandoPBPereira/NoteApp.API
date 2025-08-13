using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteApp.Common.Exceptions;
using NoteApp.Domain.Entities;
using NoteApp.Domain.Repositories;
using NoteApp.Infraestructure.Data;
using NoteApp.Infraestructure.Models;

namespace NoteApp.Infraestructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly NoteAppContext _dbContext;
    private readonly UserManager<Usuarios> _userManger;

    public UsuarioRepository(NoteAppContext dbContext, UserManager<Usuarios> userManger)
    {
        _dbContext = dbContext;
        _userManger = userManger;
    }

    public async Task Atualizar(Usuario usuario, CancellationToken cancellationToken = default)
    {
        var usuarioModel = await _dbContext.Usuarios.Where(x => x.Id == usuario.Id).FirstOrDefaultAsync(cancellationToken);

        if (usuarioModel is null) throw new NotFoundException("Usuário não encontrado");

        usuarioModel.Nome = usuario.Nome;
        usuarioModel.Email = usuario.Email;

        _dbContext.Usuarios.Update(usuarioModel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Criar(Usuario usuario, string senha, CancellationToken cancellationToken = default)
    {
        var usuarioModel = new Usuarios
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            UserName = usuario.UserName,
            Email = usuario.Email
        };

        var result = await _userManger.CreateAsync(usuarioModel, senha);

        if (!result.Succeeded) throw new Exception("Não foi possível criar usuário, tente novamente");
    }

    public async Task<Usuario?> ObterUsuarioPorId(Guid userId, CancellationToken cancellationToken = default)
    {
        var usuarioModel = await _dbContext.Usuarios.Where(x => x.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (usuarioModel is null) return null;

        var usuario = new Usuario
        {
            Id = usuarioModel.Id,
            Nome = usuarioModel.Nome,
            UserName = usuarioModel.UserName!,
            Email = usuarioModel.Email!
        };

        return usuario;
    }

    public async Task<List<Usuario>> ObterUsuarios(int take, int skip, CancellationToken cancellationToken = default)
    {
        var usuarios = await _dbContext.Usuarios
            .Skip(skip)
            .Take(take)
            .Select(x => new Usuario
            {
                Id = x.Id,
                Nome = x.Nome,
                UserName = x.UserName!,
                Email = x.Email!
            })
            .ToListAsync(cancellationToken);

        return usuarios;
    }

    public async Task<List<string>> ObterRoles(Guid userId, CancellationToken cancellationToken = default)
    {
        var roles = await _dbContext.UserRoles
                                    .GroupJoin(_dbContext.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => new { UserRoles = ur, Roles = r })
                                    .SelectMany(x => x.Roles, (ur, roles) => new
                                    {
                                       Nome = roles.Name
                                    })
                                    .Select(x => x.Nome)
                                    .ToListAsync(cancellationToken);

        return roles;

    }

    public async Task Remover(Guid userId, CancellationToken cancellationToken = default)
    {
        var usuario = await _dbContext.Usuarios.Where(x => x.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (usuario is null)
        {
            throw new NotFoundException("Usuário não encontrado");
        }

        _dbContext.Usuarios.Remove(usuario);
        await _dbContext.SaveChangesAsync();
    }
}

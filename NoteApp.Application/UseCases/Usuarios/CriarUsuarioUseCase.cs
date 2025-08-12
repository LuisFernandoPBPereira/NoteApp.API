using NoteApp.Application.DTOs.Usuarios;
using NoteApp.Domain.Entities;
using NoteApp.Domain.Repositories;

namespace NoteApp.Application.UseCases.Usuarios;

public class CriarUsuarioUseCase
{
    private readonly IUsuarioRepository _repository;

    public CriarUsuarioUseCase(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Executar(CriarUsuarioDto usuarioDto)
    {
        var usuario = Usuario.Criar(usuarioDto.Nome, usuarioDto.UserName, usuarioDto.Email);

        await _repository.Criar(usuario, usuarioDto.Senha);

        return usuario.Id;
    }
}

using NoteApp.Application.DTOs.Usuarios;
using NoteApp.Domain.Entities;
using NoteApp.Domain.Repositories;

namespace NoteApp.Application.UseCases.Usuarios;

public class ObterUsuariosUseCase
{
    private readonly IUsuarioRepository _repository;

    public ObterUsuariosUseCase(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Usuario>> Executar(ObterUsuariosDto usuarioDto, CancellationToken cancellationToken)
    {
        var usuarios = await _repository.ObterUsuarios(usuarioDto.Take, usuarioDto.Skip, cancellationToken);

        return usuarios;
    }
}

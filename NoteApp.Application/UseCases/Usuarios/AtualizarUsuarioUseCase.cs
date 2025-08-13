using NoteApp.Application.DTOs.Usuarios;
using NoteApp.Domain.Entities;
using NoteApp.Domain.Repositories;

namespace NoteApp.Application.UseCases.Usuarios;

public class AtualizarUsuarioUseCase
{
    private readonly IUsuarioRepository _repository;

    public AtualizarUsuarioUseCase(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task Executar(Guid userId, AtualizarUsuarioDto usuarioDto, CancellationToken cancellationToken)
    {
        var usuario = Usuario.Criar(userId, usuarioDto.Nome, usuarioDto.Email);

        await _repository.Atualizar(usuario, cancellationToken);
    }
}

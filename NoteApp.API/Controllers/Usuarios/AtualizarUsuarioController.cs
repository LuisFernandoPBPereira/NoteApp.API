using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Application.DTOs.Usuarios;
using NoteApp.Application.UseCases.Usuarios;
using NoteApp.Common.Exceptions;
using NoteApp.Infraestructure.Roles;

namespace NoteApp.API.Controllers.Usuarios;

[Tags("Usuário")]
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Comum)}")]
public class AtualizarUsuarioController : ControllerBase
{
    private readonly AtualizarUsuarioUseCase _atualizarUsuarioUseCase;

    public AtualizarUsuarioController(AtualizarUsuarioUseCase atualizarUsuarioUseCase)
    {
        _atualizarUsuarioUseCase = atualizarUsuarioUseCase;
    }

    [HttpPatch("/{userId}")]
    public async Task<IActionResult> Executar([FromBody] AtualizarUsuarioDto usuarioDto, [FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            await _atualizarUsuarioUseCase.Executar(userId, usuarioDto, cancellationToken);

            return NoContent();
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

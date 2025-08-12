using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Application.UseCases.Usuarios;
using NoteApp.Common.Exceptions;
using NoteApp.Infraestructure.Roles;

namespace NoteApp.API.Controllers.Usuarios;

[Tags("Usuário")]
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = nameof(Role.Admin))]
public class RemoverUsuarioController : ControllerBase
{
    private readonly RemoverUsuarioUseCase _removerUsuarioUseCase;

    public RemoverUsuarioController(RemoverUsuarioUseCase removerUsuarioUseCase)
    {
        _removerUsuarioUseCase = removerUsuarioUseCase;
    }

    [HttpDelete("/{userId}")]
    public async Task<IActionResult> Executar([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            await _removerUsuarioUseCase.Executar(userId, cancellationToken);

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

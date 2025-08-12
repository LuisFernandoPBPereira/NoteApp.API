using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Application.UseCases.Usuarios;
using NoteApp.Common.Exceptions;
using NoteApp.Infraestructure.Roles;

namespace NoteApp.API.Controllers.Usuarios;

[Tags("Usuário")]
[ApiController]
[Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Comum)}")]
public class ObterUsuarioPorIdController : ControllerBase
{
    private readonly ObterUsuarioPorIdUseCase _obterUsuarioPorIdUseCase;

    public ObterUsuarioPorIdController(ObterUsuarioPorIdUseCase obterUsuarioPorIdUseCase)
    {
        _obterUsuarioPorIdUseCase = obterUsuarioPorIdUseCase;
    }

    [HttpGet("usuario/{userId}")]
    public async Task<IActionResult> Executar([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var usuario = await _obterUsuarioPorIdUseCase.Executar(userId, cancellationToken);

            return Ok(usuario);
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

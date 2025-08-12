using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Application.DTOs.Usuarios;
using NoteApp.Application.UseCases.Usuarios;
using NoteApp.Infraestructure.Roles;

namespace NoteApp.API.Controllers.Usuarios;

[Tags("Usuário")]
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = nameof(Role.Admin))]
public class ObterUsuariosController : ControllerBase
{
    private readonly ObterUsuariosUseCase _obterUsuariosUseCase;

    public ObterUsuariosController(ObterUsuariosUseCase obterUsuariosUseCase)
    {
        _obterUsuariosUseCase = obterUsuariosUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Executar([FromQuery] ObterUsuariosDto usuarioDto, CancellationToken cancellationToken)
    {
        try
        {
            var usuarios = await _obterUsuariosUseCase.Executar(usuarioDto, cancellationToken);
            return Ok(usuarios);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

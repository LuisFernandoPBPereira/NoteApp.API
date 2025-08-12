using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Application.DTOs.Usuarios;
using NoteApp.Application.UseCases.Usuarios;

namespace NoteApp.API.Controllers.Usuarios;

[Tags("Usuário")]
[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class CriarUsuarioController : ControllerBase
{
    private readonly CriarUsuarioUseCase _criarUsuarioUseCase;

    public CriarUsuarioController(CriarUsuarioUseCase criarUsuarioUseCase)
    {
        _criarUsuarioUseCase = criarUsuarioUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Executar([FromBody] CriarUsuarioDto usuarioDto, CancellationToken cancellationToken)
    {
        try
        {
            var userId = await _criarUsuarioUseCase.Executar(usuarioDto);

            return CreatedAtAction(nameof(Executar), userId);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

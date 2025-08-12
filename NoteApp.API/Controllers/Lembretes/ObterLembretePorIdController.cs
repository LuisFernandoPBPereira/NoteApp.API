using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Application.DTOs.Lembretes;
using NoteApp.Application.UseCases.Lembretes;
using NoteApp.Common.Exceptions;
using NoteApp.Infraestructure.Roles;

namespace NoteApp.API.Controllers.Lembretes;

[Tags("Lembrete")]
[ApiController]
[Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Comum)}")]
public class ObterLembretePorIdController : ControllerBase
{
    private readonly ObterLembretePorIdUseCase _obterLembretePorIdUseCase;

    public ObterLembretePorIdController(ObterLembretePorIdUseCase obterLembretePorIdUseCase)
    {
        _obterLembretePorIdUseCase = obterLembretePorIdUseCase;
    }

    [HttpGet("lembrete/{lembreteId}")]
    public async Task<IActionResult> Executar([FromRoute] Guid lembreteId, [FromQuery] ObterLembretePorIdDto lembreteDto, CancellationToken cancellationToken)
    {
        try
        {
            var lembrete = await _obterLembretePorIdUseCase.Executar(lembreteId, lembreteDto, cancellationToken);

            return Ok(lembrete);
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

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
public class RemoverLembreteController : ControllerBase
{
    private readonly RemoverLembreteUseCase _removerLembreteUseCase;

    public RemoverLembreteController(RemoverLembreteUseCase removerLembreteUseCase)
    {
        _removerLembreteUseCase = removerLembreteUseCase;
    }

    [HttpDelete("remover/lembrete/{lembreteId}")]
    public async Task<IActionResult> Executar([FromRoute] Guid lembreteId, RemoverLembreteDto lembreteDto, CancellationToken cancellationToken)
    {
        try
        {
            await _removerLembreteUseCase.Executar(lembreteId, lembreteDto, cancellationToken);
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

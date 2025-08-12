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
public class AtualizarLembreteController : ControllerBase
{
    private readonly AtualizarLembreteUseCase _atualizarLembreteUseCase;

    public AtualizarLembreteController(AtualizarLembreteUseCase atualizarLembreteUseCase)
    {
        _atualizarLembreteUseCase = atualizarLembreteUseCase;
    }

    [HttpPatch("atualizar/lembrete/{lembreteId}")]
    public async Task<IActionResult> Executar([FromRoute] Guid lembreteId, AtualizarLembreteDto lembreteDto, CancellationToken cancellationToken)
    {
        try
        {
            await _atualizarLembreteUseCase.Executar(lembreteId, lembreteDto, cancellationToken);

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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Application.DTOs.Lembretes;
using NoteApp.Application.UseCases.Lembretes;
using NoteApp.Infraestructure.Roles;

namespace NoteApp.API.Controllers.Lembretes;

[Tags("Lembrete")]
[Route("lembretes")]
[ApiController]
[Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Comum)}")]
public class ObterLembretesController : ControllerBase
{
    private readonly ObterLembretesUseCase _obterLembretesUseCase;

    public ObterLembretesController(ObterLembretesUseCase obterLembretesUseCase)
    {
        _obterLembretesUseCase = obterLembretesUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Executar([FromQuery] ObterLembretesDto lembreteDto, CancellationToken cancellationToken)
    {
        try
        {
            var lembretes = await _obterLembretesUseCase.Executar(lembreteDto, cancellationToken);

            return Ok(lembretes);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

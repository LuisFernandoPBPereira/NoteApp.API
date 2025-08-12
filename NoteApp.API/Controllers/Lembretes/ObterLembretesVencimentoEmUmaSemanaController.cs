using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Application.DTOs.Lembretes;
using NoteApp.Application.UseCases.Lembretes;
using NoteApp.Infraestructure.Roles;

namespace NoteApp.API.Controllers.Lembretes;

[Tags("Lembrete")]
[Route("lembretes/semana")]
[ApiController]
[Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Comum)}")]
public class ObterLembretesVencimentoEmUmaSemanaController : ControllerBase
{
    private readonly ObterLembretesComVencimentoEmUmaSemanaUseCase _obterLembretesComVencimentoEmUmaSemanaUseCase;

    public ObterLembretesVencimentoEmUmaSemanaController(ObterLembretesComVencimentoEmUmaSemanaUseCase obterLembretesComVencimentoEmUmaSemanaUseCase)
    {
        _obterLembretesComVencimentoEmUmaSemanaUseCase = obterLembretesComVencimentoEmUmaSemanaUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Executar([FromQuery] ObterLembretesDto lembreteDto, CancellationToken cancellationToken)
    {
        try
        {
            var lembretes = await _obterLembretesComVencimentoEmUmaSemanaUseCase.Executar(lembreteDto, cancellationToken);

            return Ok(lembretes);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

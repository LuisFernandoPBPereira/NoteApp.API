using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Application.DTOs.Lembretes;
using NoteApp.Application.UseCases.Lembretes;
using NoteApp.Infraestructure.Roles;

namespace NoteApp.API.Controllers.Lembretes;

[Tags("Lembrete")]
[Route("lembretes/hoje")]
[ApiController]
[Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Comum)}")]
public class ObterLembreteVencimentoHojeController : ControllerBase
{
    private readonly ObterLembretesComVencimentoHojeUseCase _obterLembretesComVencimentoHojeUseCase;

    public ObterLembreteVencimentoHojeController(ObterLembretesComVencimentoHojeUseCase obterLembretesComVencimentoHojeUseCase)
    {
        _obterLembretesComVencimentoHojeUseCase = obterLembretesComVencimentoHojeUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Executar([FromQuery] ObterLembretesDto lembreteDto, CancellationToken cancellationToken)
    {
        try
        {
            var lembretes = await _obterLembretesComVencimentoHojeUseCase.Executar(lembreteDto, cancellationToken);

            return Ok(lembretes);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

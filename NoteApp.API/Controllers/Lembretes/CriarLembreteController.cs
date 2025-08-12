using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.API.DTOs.Lembretes;
using NoteApp.Application.UseCases.Lembretes;
using NoteApp.Domain.Entities;
using NoteApp.Infraestructure.Roles;

namespace NoteApp.API.Controllers.Lembretes
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Comum)}")]
    public class CriarLembreteController : ControllerBase
    {
        private readonly CriarLembreteUseCase _criarLembrete;

        public CriarLembreteController(CriarLembreteUseCase criarLembrete)
        {
            _criarLembrete = criarLembrete;
        }

        [HttpPost]
        public async Task<IActionResult> Executar([FromBody] CriarLembreteDto lembreteDto, CancellationToken cancellationToken)
        {
            try
            {
                var lembreteId = await _criarLembrete.Executar(lembreteDto, cancellationToken);

                return Ok(lembreteId);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}

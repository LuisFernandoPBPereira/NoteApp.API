using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Application.DTOs.Usuarios;
using NoteApp.Application.Services;

namespace NoteApp.API.Controllers.Usuarios;

[Tags("Usuário")]
[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class LoginController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public LoginController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var token = await _authenticationService.Login(loginDto.UserName, loginDto.Senha);

            return Ok(token);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NoteApp.Application.Services;
using NoteApp.Infraestructure.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NoteApp.Infraestructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<Usuarios> _userManager;
    private readonly IConfiguration _configuration;

    public AuthenticationService(UserManager<Usuarios> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> Login(string userName, string senha)
    {
        try
        {
            var usuario = await _userManager.FindByNameAsync(userName);

            if (usuario is null) throw new Exception("Usuário e/ou senha incorretos, tente novamente.");

            var credenciaisCorretas = await _userManager.CheckPasswordAsync(usuario, senha);

            if (!credenciaisCorretas) throw new Exception("Usuário e/ou senha incorretos, tente novamente.");

            var roles = await _userManager.GetRolesAsync(usuario);

            var token = GenerateToken(usuario, roles);

            return token;
        }
        catch (Exception)
        {
            throw;
        }

    }

    private string GenerateToken(Usuarios usuario, IList<string> roles)
    {
        var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

        var authClaims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        authClaims.AddRange(roleClaims);

        var authSigingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigingKey, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

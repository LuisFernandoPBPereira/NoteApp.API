namespace NoteApp.Application.Services;

public interface IAuthenticationService
{
    Task<string> Login(string userName, string senha);
}

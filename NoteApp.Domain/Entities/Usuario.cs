using System.Text.RegularExpressions;

namespace NoteApp.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }

    public Usuario()
    {
        
    }

    public Usuario(Guid id, string nome, string userName, string email)
    {
        Id = id;
        Nome = nome;
        UserName = userName;
        Email = email;
    }

    public static Usuario Criar(string nome, string userName, string email)
    {
        try
        {
            Validar(nome, userName, email);

            var usuario = new Usuario(Guid.NewGuid(), nome, userName, email);

            return usuario;
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static void Validar(string nome, string userName, string email)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentNullException(nameof(nome), "Nome inválido");
        }
        
        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentNullException(nameof(userName), "UserName inválido");
        }

        if (!Regex.IsMatch(email, "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Za-z0-9-]+(?:\\.[A-Za-z0-9-]+)*$"))
        {
            throw new ArgumentException("Email inválido", nameof(email));
        }
    }
}

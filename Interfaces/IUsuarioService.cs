using SIR.Models;

public interface IUsuarioService
{
    Task<Usuario> CadastrarAsync(Usuario usuario);
}
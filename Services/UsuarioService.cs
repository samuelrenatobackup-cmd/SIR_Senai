using SIR.Models;
using SIR.Contexts;
using Microsoft.EntityFrameworkCore;

public class UsuarioService : IUsuarioService
{
    private readonly ContextoBancoDados _context;

    public UsuarioService(ContextoBancoDados  context)
    {
        _context = context;
    }

    public async Task<Usuario> CadastrarAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }
}
using SIR.Models;
using SIR.Contexts;
using Microsoft.EntityFrameworkCore;

public class UsuarioService : IUsuarioService
{
    private readonly ApplicationDbContext  _context;

    public UsuarioService(ApplicationDbContext  context)
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
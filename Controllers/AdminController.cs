using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIR.Contexts;

namespace SIR.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ContextoBancoDados _context;

        public AdminController(ContextoBancoDados context)
        {
            _context = context;
        }

        // HOME DO ADMIN
        public IActionResult Index()
        {
            var reservas = _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Equipamento)
                .OrderByDescending(r => r.DataReserva)
                .Take(20)
                .ToList();

            return View(reservas);
        }

        // HISTÓRICO COMPLETO
        public IActionResult Historico()
        {
            var reservas = _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Equipamento)
                .OrderByDescending(r => r.DataReserva)
                .ToList();

            return View(reservas);
        }

        // LISTA DE USUÁRIOS
        public IActionResult Usuarios()
        {
            var usuarios = _context.Usuarios.ToList();
            return View(usuarios);
        }

        // LISTA DE EQUIPAMENTOS
        public IActionResult Equipamentos()
        {
            var equipamentos = _context.Equipamentos.ToList();
            return View(equipamentos);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIR.Contexts;
using SIR.Models;

namespace SIR.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var equipamentos = await _context.Equipamentos.ToListAsync();

            return View(equipamentos);
        }

        [HttpGet]
        public IActionResult Emprestar(int id)
        {
            var equipamento = _context.Equipamentos
                .FirstOrDefault(e => e.Id == id);

            if (equipamento == null)
                return NotFound();

            if (equipamento.QuantidadeDisponivel <= 0)
            {
                TempData["Erro"] = "Equipamento indisponível.";
                return RedirectToAction(nameof(Index));
            }

            return View(equipamento);
        }

        [HttpPost]
        public IActionResult ConfirmarEmprestimo(int equipamentoId)
        {
            var equipamento = _context.Equipamentos
                .FirstOrDefault(e => e.Id == equipamentoId);

            if (equipamento == null)
                return NotFound();

            if (equipamento.QuantidadeDisponivel <= 0)
            {
                TempData["Erro"] = "Não há unidades disponíveis.";
                return RedirectToAction(nameof(Index));
            }

            equipamento.QuantidadeDisponivel--;

            var reserva = new Reserva
            {
                EquipamentoId = equipamento.Id,
                UsuarioId = 1, // depois pegar do login
                DataReserva = DateTime.Now,
                HoraReserva = DateTime.Now.TimeOfDay,
                HoraDevolucao = DateTime.Now.AddHours(2).TimeOfDay,
                Status = "Ativa"
            };

            _context.Reservas.Add(reserva);
            _context.SaveChanges();

            TempData["Sucesso"] = "Equipamento emprestado com sucesso.";

            return RedirectToAction(nameof(Index));
        }
    }
}
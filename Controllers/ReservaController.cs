using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIR.Contexts;
using SIR.Models;

namespace SIR.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class ReservaController : Controller
    {
        private readonly ContextoBancoDados _context;

        public ReservaController(ContextoBancoDados context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListarEquipamentos()
        {
            var equipamentos = await _context.Equipamentos.ToListAsync();

            return View("Index", equipamentos);
        }

        [HttpGet]
        public IActionResult ExibirEmprestimo(int id)
        {
            var equipamento = _context.Equipamentos
                .FirstOrDefault(e => e.Id == id);

            if (equipamento == null)
                return NotFound();

            if (equipamento.QuantidadeDisponivel <= 0)
            {
                TempData["Erro"] = "Equipamento indisponível.";
                return RedirectToAction(nameof(ListarEquipamentos));
            }

            return View("Emprestar", equipamento);
        }

        [HttpPost]
        public IActionResult RealizarEmprestimo(int equipamentoId)
        {
            var equipamento = _context.Equipamentos
                .FirstOrDefault(e => e.Id == equipamentoId);

            if (equipamento == null)
                return NotFound();

            if (equipamento.QuantidadeDisponivel <= 0)
            {
                TempData["Erro"] = "Não há unidades disponíveis.";
                return RedirectToAction(nameof(ListarEquipamentos));
            }

            equipamento.QuantidadeDisponivel--;

            var reserva = new Reserva
            {
                EquipamentoId = equipamento.Id,
                UsuarioId = 1,
                DataReserva = DateTime.Now,
                HoraReserva = DateTime.Now.TimeOfDay,
                HoraDevolucao = DateTime.Now.AddHours(2).TimeOfDay,
                Status = "Ativa"
            };

            _context.Reservas.Add(reserva);
            _context.SaveChanges();

            TempData["Sucesso"] = "Equipamento emprestado com sucesso.";

            return RedirectToAction(nameof(ListarEquipamentos));
        }
    }
}
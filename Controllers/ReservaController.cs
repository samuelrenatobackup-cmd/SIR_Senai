using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIR.Contexts;
using SIR.Models;
using System.Security.Claims;

namespace SIR.Controllers
{
    [Authorize]
    public class ReservaController : Controller
    {
        private readonly ContextoBancoDados _context;

        public ReservaController(ContextoBancoDados context)
        {
            _context = context;
        }

        // ===================== LISTAR EQUIPAMENTOS =====================

        [HttpGet]
        public async Task<IActionResult> ListarEquipamentos()
        {
            var equipamentos = await _context.Equipamentos.ToListAsync();
            return View("Index", equipamentos);
        }

        // ===================== CONFIRMAR EMPRÉSTIMO =====================

        [HttpGet]
        public IActionResult ConfirmarEmprestimo(int equipamentoId)
        {
            var equipamento = _context.Equipamentos
                .FirstOrDefault(e => e.Id == equipamentoId);

            if (equipamento == null)
                return NotFound();

            if (equipamento.QuantidadeDisponivel <= 0)
            {
                TempData["Erro"] = "Equipamento indisponível.";
                return RedirectToAction(nameof(ListarEquipamentos));
            }

            return View(equipamento);
        }

        // ===================== REALIZAR EMPRÉSTIMO =====================

        [HttpPost]
        public IActionResult RealizarEmprestimo(int equipamentoId)
        {
                    int usuarioId = int.Parse(
    User.FindFirst(ClaimTypes.NameIdentifier)!.Value
);



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

            var agora = DateTime.Now;

            var reserva = new Reserva
            {
                EquipamentoId = equipamento.Id,
                UsuarioId = usuarioId,
                DataReserva = agora,
                HoraReserva = agora.TimeOfDay,
                HoraDevolucaoPrevista = agora.AddHours(2).TimeOfDay,
                Status = "Ativa"
            };
    
            _context.Reservas.Add(reserva);
            _context.SaveChanges();

            TempData["Sucesso"] = "Equipamento emprestado com sucesso.";

            return RedirectToAction(nameof(MinhasReservas));
        }

        // ===================== MINHAS RESERVAS =====================

        [HttpGet]
public IActionResult MinhasReservas()
{
    int usuarioId = int.Parse(
        User.FindFirst(ClaimTypes.NameIdentifier)!.Value
    );

    var reservas = _context.Reservas
        .Include(r => r.Equipamento)
        .Where(r => r.UsuarioId == usuarioId &&
                    r.Status == "Ativa")
        .OrderByDescending(r => r.DataReserva)
        .ToList();

    return View(reservas);
}

        // ===================== DEVOLUÇÃO =====================

        [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult ConfirmarDevolucao(int reservaId)
{
    int usuarioId = int.Parse(
        User.FindFirst(ClaimTypes.NameIdentifier)!.Value
    );

    var reserva = _context.Reservas
        .FirstOrDefault(r =>
            r.Id == reservaId &&
            r.UsuarioId == usuarioId);

    if (reserva == null)
        return NotFound();

    if (reserva.Status == "Finalizado")
        return RedirectToAction(nameof(MinhasReservas));

    var equipamento = _context.Equipamentos
        .FirstOrDefault(e => e.Id == reserva.EquipamentoId);

    if (equipamento != null)
    {
        equipamento.QuantidadeDisponivel++;
    }

    var agora = DateTime.Now;

    reserva.Status = "Finalizado";
    reserva.DataDevolucao = agora;
    reserva.HoraDevolucaoReal = agora.TimeOfDay;

    _context.SaveChanges();

    TempData["Sucesso"] = "Devolução realizada com sucesso.";

    return RedirectToAction(nameof(MinhasReservas));
}
        // ===================== HISTÓRICO ADMIN =====================

        [HttpGet]
        public IActionResult HistoricoReservas()
        {
            var reservas = _context.Reservas
                .Include(r => r.Equipamento)
                .Include(r => r.Usuario)
                .Where(r => r.Status == "Finalizado")
                .OrderByDescending(r => r.DataDevolucao)
                .ToList();

            return View(reservas);
        }
    }
}
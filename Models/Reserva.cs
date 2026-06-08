using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SIR.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        [ForeignKey("Usuario")] public int UsuarioId { get; set; }
        [ForeignKey("Equipamento")] public int EquipamentoId { get; set; }
        [Required] public DateTime DataReserva { get; set; }
        [Required] public TimeSpan HoraReserva { get; set; }
        [Required] public TimeSpan HoraDevolucao { get; set; }
        [Required] public string Status { get; set; }
    }
}
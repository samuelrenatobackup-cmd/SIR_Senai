using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIR.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int EquipamentoId { get; set; }
        public Equipamento? Equipamento { get; set; }

        [Required]
        public DateTime DataReserva { get; set; }

        [Required]
        public TimeSpan HoraReserva { get; set; }

        [Required]
        public TimeSpan HoraDevolucaoPrevista { get; set; }

        public DateTime? DataDevolucao { get; set; }

        public TimeSpan? HoraDevolucaoReal { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
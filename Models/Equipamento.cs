using System.ComponentModel.DataAnnotations;

namespace SIR.Models
{
    public class Equipamento
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int QuantidadeTotal { get; set; }

        [Required]
        public int QuantidadeDisponivel { get; set; }

        // Relacionamento
        public ICollection<Reserva>? Reservas { get; set; }
    }
}

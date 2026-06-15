using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string? Icone { get; set; }

        public ICollection<Reserva>? Reservas { get; set; }

        [NotMapped]
        public int QuantidadeEmprestada
        {
            get
            {
                return QuantidadeTotal - QuantidadeDisponivel;
            }
        }

        [NotMapped]
        public string Status
        {
            get
            {
                if (QuantidadeTotal == 0)
                    return "Indisponível";

                if (QuantidadeDisponivel == 0)
                    return "Em Uso";

                if (QuantidadeDisponivel < QuantidadeTotal)
                    return "Parcial";

                return "Disponível";
            }
        }
    }
}
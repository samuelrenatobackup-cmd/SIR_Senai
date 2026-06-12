using System.ComponentModel.DataAnnotations;
using SIR.Models;
public class Usuario
{
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    public string Sobrenome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Senha { get; set; }

    public bool TipoUsuario { get; set; }

    public ICollection<Reserva>? Reservas { get; set; }
}
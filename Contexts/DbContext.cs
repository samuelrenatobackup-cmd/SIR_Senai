using Microsoft.EntityFrameworkCore;
using SIR.Models;

namespace SIR.Contexts
{
    public class ContextoBancoDados : DbContext
    {
        public ContextoBancoDados(
            DbContextOptions<ContextoBancoDados> opcoes)
            : base(opcoes)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Equipamento> Equipamentos { get; set; }

        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder construtorModelo)
        {
            construtorModelo.Entity<Usuario>()
                .ToTable("Usuarios");

            construtorModelo.Entity<Equipamento>()
                .ToTable("Equipamentos");

            construtorModelo.Entity<Reserva>()
                .ToTable("Reservas");

            base.OnModelCreating(construtorModelo);
        }
    }
}
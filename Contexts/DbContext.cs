using Microsoft.EntityFrameworkCore;
using SIR.Models;

namespace SIR.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext
        (
            DbContextOptions<ApplicationDbContext> options
        ) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Equipamento>().ToTable("Equipamento");
            modelBuilder.Entity<Reserva>().ToTable("Reserva");

            base.OnModelCreating(modelBuilder);
        }
    }
}
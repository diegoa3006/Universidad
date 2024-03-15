using MiApiUniversidad.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MiApiUniversidad.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Materia> Materias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departamento>().HasData(
                new Departamento()
                {
                    Id = 1,
                    Nombre = "Derecho",
                    Descripcion = "Facultad Derecho"
                },
                new Departamento()
                {
                    Id = 2,
                    Nombre = "Trabajo Social",
                    Descripcion = "San Camilo"
                }

            );

            modelBuilder.Entity<Materia>().HasData(
                new Materia()
                {
                    Id = 1,
                    Nombre = "Creditos 1",
                    Creditos = 3
                },
                new Materia()
                {
                    Id = 2,
                    Nombre = "Creditos 2",
                    Creditos = 4
                }
           );
        }

    }
}

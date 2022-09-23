using Microsoft.EntityFrameworkCore;

using Dominio.Viaticos.Entidades;
using System.Linq;

namespace Infraestructura.Datos.Viaticos.UnidadDeTrabajo
{
    public class ViaticosUnidadDeTrabajo : Infraestructura.Datos.Nucleo.UnidadDeTrabajo, IViaticosUnidadDeTrabajo
    {
        public ViaticosUnidadDeTrabajo() : base() { }

        public ViaticosUnidadDeTrabajo(DbContextOptions<ViaticosUnidadDeTrabajo> options) : base(options) { }


        public DbSet<PasoViatico> Usuarios { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Solo para crear las migraciones
            //optionsBuilder.UseSqlServer("Server=localhost; Database=BD; user id=sa; password=Admin;");

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {

                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            if (this.Database.IsSqlServer())
            {
                modelBuilder.Ignore<Dominio.Nucleo.Entidades.RolDirectoVista>();
                modelBuilder.Ignore<Dominio.Nucleo.Entidades.RolUsuarioVista>();
                modelBuilder.Ignore<Dominio.Nucleo.Entidades.RolParticularVista>();
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}

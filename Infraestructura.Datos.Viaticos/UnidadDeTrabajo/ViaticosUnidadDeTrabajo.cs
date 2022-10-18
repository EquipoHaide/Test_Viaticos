using Microsoft.EntityFrameworkCore;

using Dominio.Viaticos.Entidades;
using System.Linq;

namespace Infraestructura.Datos.Viaticos.UnidadDeTrabajo
{
    public class ViaticosUnidadDeTrabajo : Infraestructura.Datos.Nucleo.UnidadDeTrabajo, IViaticosUnidadDeTrabajo
    {
        public ViaticosUnidadDeTrabajo() : base() { }

        public ViaticosUnidadDeTrabajo(DbContextOptions<ViaticosUnidadDeTrabajo> options) : base(options) { }


        public DbSet<PasoViatico> Pasos { get; set; }
        public DbSet<FlujoViatico> Flujos { get; set; }
        public DbSet<NivelEmpleado> NivelEmpleados { get; set; }
        public DbSet<TipoEntePublico> TipoEntePublicos { get; set; }
        public DbSet<SolicitudCondensada> SolicitudCondensada { get; set; }
        public DbSet<Autorizacion> Autorizaciones { get; set; }
        public DbSet<HistorialFlujoViatico> HistorialFlujoViaticos { get; set; }
        public DbSet<HistorialPasoViatico> HistorialPasoViaticos { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Solo para crear las migraciones
            optionsBuilder.UseSqlServer("Server=localhost; Database=BD; user id=sa; password=Admin;");

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

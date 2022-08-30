using Dominio.Nucleo.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Datos.Nucleo
{
    /// <summary>
    /// Esta unidad de trabajo es la base para todas las unidades de trabajo que vayan a requerir tener acceso a los grupos de
    /// roles compilados(rolesdirecto, roles de usuario y roles particulares) ya que hace la inclusion de las tablas necesarias para poder
    /// utilizarlo en filtrados de datos en base a roles.
    /// </summary>
    public class UnidadDeTrabajo : DbContext
    {
        public UnidadDeTrabajo() : base() { }
        public UnidadDeTrabajo(DbContextOptions options) : base(options) { }

        public DbSet<RolDirectoVista> RolesDirectos { get; set; }
        public DbSet<RolUsuarioVista> RolesUsuarios { get; set; }
        public DbSet<RolParticularVista> RolesParticulares { get; set; }
    }
}
using Dominio.Seguridad.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Infraestructura.Datos.Seguridad.UnidadDeTrabajo
{
    public class SeguridadUnidadDeTrabajo : Infraestructura.Datos.Nucleo.UnidadDeTrabajo, ISeguridadUnidadDeTrabajo
    {
        public SeguridadUnidadDeTrabajo() : base() { }
        public SeguridadUnidadDeTrabajo(DbContextOptions<SeguridadUnidadDeTrabajo> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RolUsuario> RolesUsuario { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<RecursoRol> RecursosRoles { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<OpcionModulo> OpcionesModulos { get; set; }
        public DbSet<Accion> Acciones { get; set; }
        public DbSet<RecursoAccion> RecursosAcciones { get; set; }
        public DbSet<Acceso> Accesos { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<RecursoGrupo> RecursosGrupos { get; set; }
        public DbSet<RolGrupo> RolesGrupos { get; set; }
        public DbSet<UsuarioGrupo> UsuariosGrupos { get; set; }


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

            modelBuilder.Entity<Grupo>()
            .HasMany(rr => rr.Usuarios)
            .WithOne(r => r.Grupo).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Grupo>()
           .HasMany(rr => rr.Recursos)
           .WithOne(r => r.Recurso).IsRequired().OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<Grupo>()
            //.HasMany(rr => rr.Usuarios)
            //.WithOne(r => r.Grupo)
            //.HasForeignKey(rr => rr.IdUsuario).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Grupo>()
            .HasMany(rr => rr.Roles)
            .WithOne(r => r.Grupo)
            .IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecursoRol>()
               .HasOne(rr => rr.Rol)
               .WithMany(r => r.RecursosDeRol)
               .HasForeignKey(rr => rr.IdRol).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecursoRol>()
                .HasOne(rr => rr.Recurso)
                .WithMany(r => r.Recursos)
                .HasForeignKey(rr => rr.IdRecurso);

            base.OnModelCreating(modelBuilder);

            ////modelBuilder.Entity<RecursoRol>()
            ////    .HasKey(rr => new { rr.IdRol, rr.IdRecurso });
            //modelBuilder.Entity<RecursoRol>()
            //    .HasOne(rr => rr.Rol)
            //    .WithMany(r => r.RecursosDeRol)
            //    .HasForeignKey(rr => rr.IdRol).OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<RecursoRol>()
            //    .HasOne(rr => rr.Recurso)
            //    .WithMany(r => r.Recursos)
            //    .HasForeignKey(rr => rr.IdRecurso);
        }
    }
}

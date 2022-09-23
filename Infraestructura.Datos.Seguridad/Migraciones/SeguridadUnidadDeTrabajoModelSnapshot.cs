﻿// <auto-generated />
using System;
using Infraestructura.Datos.Seguridad.UnidadDeTrabajo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infraestructura.Datos.Seguridad.Migraciones
{
    [DbContext(typeof(SeguridadUnidadDeTrabajo))]
    partial class SeguridadUnidadDeTrabajoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dominio.Nucleo.Entidades.RolDirectoVista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RolesDirectos","Seguridad");
                });

            modelBuilder.Entity("Dominio.Nucleo.Entidades.RolParticularVista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RolesParticulares","Seguridad");
                });

            modelBuilder.Entity("Dominio.Nucleo.Entidades.RolUsuarioVista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RolesUsuario","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.Acceso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCaducidad")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdAccion")
                        .HasColumnType("int");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<string>("IdUsuarioCreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsuarioElimino")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsuarioModifico")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdAccion");

                    b.HasIndex("IdRol");

                    b.ToTable("Accesos","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.Accion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<bool>("EsPrincipal")
                        .HasColumnType("bit");

                    b.Property<bool>("EsVisible")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdOpcionModulo")
                        .HasColumnType("int");

                    b.Property<string>("IdUsuarioCreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsuarioElimino")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsuarioModifico")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ruta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdOpcionModulo");

                    b.ToTable("Acciones","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.Grupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdUsuarioCreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsuarioElimino")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsuarioModifico")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Grupos","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.Modulo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Modulos","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.OpcionModulo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<int>("IdModulo")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdModulo");

                    b.ToTable("OpcionesModulo","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.RecursoAccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EsEjecucion")
                        .HasColumnType("bit");

                    b.Property<bool>("EsEscritura")
                        .HasColumnType("bit");

                    b.Property<bool>("EsLectura")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdRecurso")
                        .HasColumnType("int");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<string>("IdUsuarioCreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsuarioModifico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdRecurso");

                    b.HasIndex("IdRol");

                    b.ToTable("RecursosAccion","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.RecursoGrupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EsEjecucion")
                        .HasColumnType("bit");

                    b.Property<bool>("EsEscritura")
                        .HasColumnType("bit");

                    b.Property<bool>("EsLectura")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdRecurso")
                        .HasColumnType("int");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<string>("IdUsuarioCreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsuarioModifico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdRecurso");

                    b.HasIndex("IdRol");

                    b.ToTable("RecursosGrupo","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.RecursoRol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EsEjecucion")
                        .HasColumnType("bit");

                    b.Property<bool>("EsEscritura")
                        .HasColumnType("bit");

                    b.Property<bool>("EsLectura")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdRecurso")
                        .HasColumnType("int");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<string>("IdUsuarioCreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsuarioModifico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdRecurso");

                    b.HasIndex("IdRol");

                    b.ToTable("RecursosRol","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdUsuarioCreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsuarioElimino")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsuarioModifico")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Roles","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.RolGrupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdGrupo")
                        .HasColumnType("int");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<string>("IdUsuarioCreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdGrupo");

                    b.HasIndex("IdRol");

                    b.ToTable("RolesGrupo","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.RolUsuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("IdUsuarioCreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdRol");

                    b.HasIndex("IdUsuario");

                    b.ToTable("RolUsuarios","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.Sesion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Expira")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TokenCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Sesiones","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AreaAdscripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoElectronicoLaboral")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoElectronicoPersonal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dependencia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EsCliente")
                        .HasColumnType("bit");

                    b.Property<bool>("EsHabilitado")
                        .HasColumnType("bit");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroEmpleado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SesionesPermitidas")
                        .HasColumnType("int");

                    b.Property<string>("SubjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelefonoLaboral")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelefonoPersonal")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.UsuarioGrupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdGrupo")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("IdUsuarioCreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdGrupo");

                    b.HasIndex("IdUsuario");

                    b.ToTable("UsuariosGrupo","Seguridad");
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.Acceso", b =>
                {
                    b.HasOne("Dominio.Seguridad.Entidades.Accion", "Accion")
                        .WithMany("Accesos")
                        .HasForeignKey("IdAccion")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Dominio.Seguridad.Entidades.Rol", "Rol")
                        .WithMany("Accesos")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.Accion", b =>
                {
                    b.HasOne("Dominio.Seguridad.Entidades.OpcionModulo", "OpcionModulo")
                        .WithMany("Acciones")
                        .HasForeignKey("IdOpcionModulo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.OpcionModulo", b =>
                {
                    b.HasOne("Dominio.Seguridad.Entidades.Modulo", "Modulo")
                        .WithMany("Opciones")
                        .HasForeignKey("IdModulo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.RecursoAccion", b =>
                {
                    b.HasOne("Dominio.Seguridad.Entidades.Accion", "Recurso")
                        .WithMany("Recursos")
                        .HasForeignKey("IdRecurso")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Dominio.Seguridad.Entidades.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.RecursoGrupo", b =>
                {
                    b.HasOne("Dominio.Seguridad.Entidades.Grupo", "Recurso")
                        .WithMany("Recursos")
                        .HasForeignKey("IdRecurso")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Seguridad.Entidades.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.RecursoRol", b =>
                {
                    b.HasOne("Dominio.Seguridad.Entidades.Rol", "Recurso")
                        .WithMany("Recursos")
                        .HasForeignKey("IdRecurso")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Seguridad.Entidades.Rol", "Rol")
                        .WithMany("RecursosDeRol")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.RolGrupo", b =>
                {
                    b.HasOne("Dominio.Seguridad.Entidades.Grupo", "Grupo")
                        .WithMany("Roles")
                        .HasForeignKey("IdGrupo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Seguridad.Entidades.Rol", "Rol")
                        .WithMany("Grupos")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.RolUsuario", b =>
                {
                    b.HasOne("Dominio.Seguridad.Entidades.Rol", "Rol")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Dominio.Seguridad.Entidades.Usuario", "Usuario")
                        .WithMany("Roles")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.Sesion", b =>
                {
                    b.HasOne("Dominio.Seguridad.Entidades.Usuario", "Usuario")
                        .WithMany("Sesiones")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Seguridad.Entidades.UsuarioGrupo", b =>
                {
                    b.HasOne("Dominio.Seguridad.Entidades.Grupo", "Grupo")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdGrupo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Seguridad.Entidades.Usuario", "Usuario")
                        .WithMany("Grupos")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

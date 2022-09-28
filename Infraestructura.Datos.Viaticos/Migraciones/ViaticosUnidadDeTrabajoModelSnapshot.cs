﻿// <auto-generated />
using System;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infraestructura.Datos.Viaticos.Migraciones
{
    [DbContext(typeof(ViaticosUnidadDeTrabajo))]
    partial class ViaticosUnidadDeTrabajoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dominio.Viaticos.Entidades.ConfiguracionFlujo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<int>("Clasificacion")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdEntePublico")
                        .HasColumnType("int");

                    b.Property<int>("IdNivelEmpleado")
                        .HasColumnType("int");

                    b.Property<string>("IdUsuarioCreo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsuarioElimino")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsuarioModifico")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreFlujo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoFlujo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdEntePublico");

                    b.HasIndex("IdNivelEmpleado");

                    b.ToTable("Flujos","Viaticos");
                });

            modelBuilder.Entity("Dominio.Viaticos.Entidades.NivelEmpleado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nivel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NivelEmpleados","Viaticos");
                });

            modelBuilder.Entity("Dominio.Viaticos.Entidades.PasoViatico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EsFirma")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdConfiguracionFlujo")
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

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.Property<int>("TipoRol")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdConfiguracionFlujo");

                    b.ToTable("Pasos","Viaticos");
                });

            modelBuilder.Entity("Dominio.Viaticos.Entidades.TipoEntePublico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EntePublicos","Viaticos");
                });

            modelBuilder.Entity("Dominio.Viaticos.Entidades.ConfiguracionFlujo", b =>
                {
                    b.HasOne("Dominio.Viaticos.Entidades.TipoEntePublico", "EntePublico")
                        .WithMany()
                        .HasForeignKey("IdEntePublico")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Dominio.Viaticos.Entidades.NivelEmpleado", "Nivel")
                        .WithMany()
                        .HasForeignKey("IdNivelEmpleado")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Viaticos.Entidades.PasoViatico", b =>
                {
                    b.HasOne("Dominio.Viaticos.Entidades.ConfiguracionFlujo", "Flujo")
                        .WithMany("Pasos")
                        .HasForeignKey("IdConfiguracionFlujo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Datos.Viaticos.Migraciones
{
    public partial class viaticos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Viaticos");

            migrationBuilder.CreateTable(
                name: "EntePublicos",
                schema: "Viaticos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntePublicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NivelEmpleados",
                schema: "Viaticos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nivel = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelEmpleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flujos",
                schema: "Viaticos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioCreo = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    IdUsuarioModifico = table.Column<string>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    IdUsuarioElimino = table.Column<string>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    IdTipoEnte = table.Column<int>(nullable: false),
                    IdNivelEmpleado = table.Column<int>(nullable: false),
                    TipoFlujo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flujos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flujos_EntePublicos_IdTipoEnte",
                        column: x => x.IdTipoEnte,
                        principalSchema: "Viaticos",
                        principalTable: "EntePublicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pasos",
                schema: "Viaticos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioCreo = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    IdUsuarioModifico = table.Column<string>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    IdUsuarioElimino = table.Column<string>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    IdFlujo = table.Column<int>(nullable: false),
                    IdRolAutoriza = table.Column<int>(nullable: false),
                    TipoRol = table.Column<int>(nullable: false),
                    Orden = table.Column<int>(nullable: false),
                    AplicaFirma = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pasos_Flujos_IdFlujo",
                        column: x => x.IdFlujo,
                        principalSchema: "Viaticos",
                        principalTable: "Flujos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flujos_IdTipoEnte",
                schema: "Viaticos",
                table: "Flujos",
                column: "IdTipoEnte");

            migrationBuilder.CreateIndex(
                name: "IX_Pasos_IdFlujo",
                schema: "Viaticos",
                table: "Pasos",
                column: "IdFlujo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NivelEmpleados",
                schema: "Viaticos");

            migrationBuilder.DropTable(
                name: "Pasos",
                schema: "Viaticos");

            migrationBuilder.DropTable(
                name: "Flujos",
                schema: "Viaticos");

            migrationBuilder.DropTable(
                name: "EntePublicos",
                schema: "Viaticos");
        }
    }
}

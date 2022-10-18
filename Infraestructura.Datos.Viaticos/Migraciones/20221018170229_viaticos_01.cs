using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Datos.Viaticos.Migraciones
{
    public partial class viaticos_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdNivelEmpleado",
                schema: "Viaticos",
                table: "Flujos",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Autorizaciones",
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
                    Orden = table.Column<int>(nullable: false),
                    IdRol = table.Column<int>(nullable: false),
                    Sello = table.Column<string>(nullable: true),
                    Estado = table.Column<int>(nullable: false),
                    IdUsuarioAutorizacion = table.Column<string>(nullable: true),
                    FechaAutorizacion = table.Column<DateTime>(nullable: false),
                    IdUsuarioCancelacion = table.Column<string>(nullable: true),
                    FechaCancelacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autorizaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudCondensada",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioCreo = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    IdAutorizacion = table.Column<int>(nullable: false),
                    Folio = table.Column<string>(nullable: true),
                    Concepto = table.Column<string>(nullable: true),
                    Estado = table.Column<int>(nullable: false),
                    Orden = table.Column<int>(nullable: false),
                    IdRol = table.Column<int>(nullable: false),
                    IdUsuarioAutorizacion = table.Column<string>(nullable: true),
                    FechaAutorizacion = table.Column<DateTime>(nullable: false),
                    IdUsuarioCancelacion = table.Column<string>(nullable: true),
                    FechaCancelacion = table.Column<DateTime>(nullable: false),
                    FechaAfectacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudCondensada", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistorialesFlujos",
                schema: "Viaticos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFlujo = table.Column<int>(nullable: false),
                    IdTipoEnte = table.Column<int>(nullable: false),
                    IdNivelEmpleado = table.Column<int>(nullable: true),
                    TipoFlujo = table.Column<int>(nullable: false),
                    IdUsuarioModifico = table.Column<string>(nullable: false),
                    OperacionInicio = table.Column<DateTime>(nullable: false),
                    OperacionFin = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialesFlujos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialesFlujos_Flujos_IdFlujo",
                        column: x => x.IdFlujo,
                        principalSchema: "Viaticos",
                        principalTable: "Flujos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HitorialesPasos",
                schema: "Viaticos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPaso = table.Column<int>(nullable: false),
                    IdFlujo = table.Column<int>(nullable: false),
                    IdRolAutoriza = table.Column<int>(nullable: false),
                    TipoRol = table.Column<int>(nullable: false),
                    Orden = table.Column<int>(nullable: false),
                    AplicaFirma = table.Column<bool>(nullable: false),
                    IdUsuarioModifico = table.Column<string>(nullable: false),
                    OperacionInicio = table.Column<DateTime>(nullable: false),
                    OperacionFin = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HitorialesPasos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HitorialesPasos_Pasos_IdPaso",
                        column: x => x.IdPaso,
                        principalSchema: "Viaticos",
                        principalTable: "Pasos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistorialesFlujos_IdFlujo",
                schema: "Viaticos",
                table: "HistorialesFlujos",
                column: "IdFlujo");

            migrationBuilder.CreateIndex(
                name: "IX_HitorialesPasos_IdPaso",
                schema: "Viaticos",
                table: "HitorialesPasos",
                column: "IdPaso");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Autorizaciones");

            migrationBuilder.DropTable(
                name: "SolicitudCondensada");

            migrationBuilder.DropTable(
                name: "HistorialesFlujos",
                schema: "Viaticos");

            migrationBuilder.DropTable(
                name: "HitorialesPasos",
                schema: "Viaticos");

            migrationBuilder.AlterColumn<int>(
                name: "IdNivelEmpleado",
                schema: "Viaticos",
                table: "Flujos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

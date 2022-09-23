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
                name: "EntePublico",
                schema: "Viaticos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntePublico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NivelEmpleado",
                schema: "Viaticos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nivel = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelEmpleado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flujo",
                schema: "Viaticos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNivelEmpleado = table.Column<int>(nullable: false),
                    IdEntePublico = table.Column<int>(nullable: false),
                    TipoFlujo = table.Column<int>(nullable: false),
                    NombreFlujo = table.Column<string>(nullable: false),
                    Activo = table.Column<bool>(nullable: false),
                    DescripcionEntePublico = table.Column<string>(nullable: true),
                    Nivel = table.Column<string>(nullable: true),
                    idEntePublico = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flujo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flujo_NivelEmpleado_IdNivelEmpleado",
                        column: x => x.IdNivelEmpleado,
                        principalSchema: "Viaticos",
                        principalTable: "NivelEmpleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flujo_EntePublico_idEntePublico",
                        column: x => x.idEntePublico,
                        principalSchema: "Viaticos",
                        principalTable: "EntePublico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paso",
                schema: "Viaticos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRol = table.Column<int>(nullable: false),
                    IdConfiguracionFlujo = table.Column<int>(nullable: false),
                    Orden = table.Column<int>(nullable: false),
                    TipoRol = table.Column<int>(nullable: false),
                    EsFirma = table.Column<bool>(nullable: false),
                    Descripcion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paso_Flujo_IdConfiguracionFlujo",
                        column: x => x.IdConfiguracionFlujo,
                        principalSchema: "Viaticos",
                        principalTable: "Flujo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flujo_IdNivelEmpleado",
                schema: "Viaticos",
                table: "Flujo",
                column: "IdNivelEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Flujo_idEntePublico",
                schema: "Viaticos",
                table: "Flujo",
                column: "idEntePublico");

            migrationBuilder.CreateIndex(
                name: "IX_Paso_IdConfiguracionFlujo",
                schema: "Viaticos",
                table: "Paso",
                column: "IdConfiguracionFlujo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paso",
                schema: "Viaticos");

            migrationBuilder.DropTable(
                name: "Flujo",
                schema: "Viaticos");

            migrationBuilder.DropTable(
                name: "NivelEmpleado",
                schema: "Viaticos");

            migrationBuilder.DropTable(
                name: "EntePublico",
                schema: "Viaticos");
        }
    }
}

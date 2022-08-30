using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Datos.Seguridad.Migraciones
{
    public partial class Seguridad_0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Seguridad");

            migrationBuilder.CreateTable(
                name: "Grupos",
                schema: "Seguridad",
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
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Seguridad",
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
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesDirectos",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<string>(nullable: true),
                    IdRol = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesDirectos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesParticulares",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<string>(nullable: true),
                    IdRol = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesParticulares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesUsuario",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<string>(nullable: true),
                    IdRol = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Apellidos = table.Column<string>(nullable: false),
                    CorreoElectronicoPersonal = table.Column<string>(nullable: true),
                    CorreoElectronicoLaboral = table.Column<string>(nullable: false),
                    TelefonoPersonal = table.Column<string>(nullable: true),
                    TelefonoLaboral = table.Column<string>(nullable: false),
                    Extension = table.Column<string>(nullable: false),
                    NombreUsuario = table.Column<string>(nullable: false),
                    NumeroEmpleado = table.Column<string>(nullable: false),
                    AreaAdscripcion = table.Column<string>(nullable: false),
                    Dependencia = table.Column<string>(nullable: false),
                    EsCliente = table.Column<bool>(nullable: false),
                    EsHabilitado = table.Column<bool>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    SesionesPermitidas = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpcionesModulo",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdModulo = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcionesModulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcionesModulo_Modulos_IdModulo",
                        column: x => x.IdModulo,
                        principalSchema: "Seguridad",
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecursosGrupo",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioCreo = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    IdUsuarioModifico = table.Column<string>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    IdRol = table.Column<int>(nullable: false),
                    IdRecurso = table.Column<int>(nullable: false),
                    EsLectura = table.Column<bool>(nullable: false),
                    EsEscritura = table.Column<bool>(nullable: false),
                    EsEjecucion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecursosGrupo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecursosGrupo_Grupos_IdRecurso",
                        column: x => x.IdRecurso,
                        principalSchema: "Seguridad",
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecursosGrupo_Roles_IdRol",
                        column: x => x.IdRol,
                        principalSchema: "Seguridad",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecursosRol",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioCreo = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    IdUsuarioModifico = table.Column<string>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    IdRol = table.Column<int>(nullable: false),
                    IdRecurso = table.Column<int>(nullable: false),
                    EsLectura = table.Column<bool>(nullable: false),
                    EsEscritura = table.Column<bool>(nullable: false),
                    EsEjecucion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecursosRol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecursosRol_Roles_IdRecurso",
                        column: x => x.IdRecurso,
                        principalSchema: "Seguridad",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecursosRol_Roles_IdRol",
                        column: x => x.IdRol,
                        principalSchema: "Seguridad",
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolesGrupo",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioCreo = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    IdRol = table.Column<int>(nullable: false),
                    IdGrupo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesGrupo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolesGrupo_Grupos_IdGrupo",
                        column: x => x.IdGrupo,
                        principalSchema: "Seguridad",
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesGrupo_Roles_IdRol",
                        column: x => x.IdRol,
                        principalSchema: "Seguridad",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolUsuarios",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioCreo = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    IdUsuario = table.Column<int>(nullable: false),
                    IdRol = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolUsuarios_Roles_IdRol",
                        column: x => x.IdRol,
                        principalSchema: "Seguridad",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolUsuarios_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sesiones",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(nullable: false),
                    TokenCount = table.Column<int>(nullable: false),
                    Token = table.Column<string>(nullable: false),
                    Inicio = table.Column<DateTime>(nullable: false),
                    Expira = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sesiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sesiones_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosGrupo",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioCreo = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    IdUsuario = table.Column<int>(nullable: false),
                    IdGrupo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosGrupo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosGrupo_Grupos_IdGrupo",
                        column: x => x.IdGrupo,
                        principalSchema: "Seguridad",
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosGrupo_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Acciones",
                schema: "Seguridad",
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
                    IdOpcionModulo = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    EsVisible = table.Column<bool>(nullable: false),
                    EsPrincipal = table.Column<bool>(nullable: false),
                    Ruta = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acciones_OpcionesModulo_IdOpcionModulo",
                        column: x => x.IdOpcionModulo,
                        principalSchema: "Seguridad",
                        principalTable: "OpcionesModulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accesos",
                schema: "Seguridad",
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
                    IdAccion = table.Column<int>(nullable: false),
                    IdRol = table.Column<int>(nullable: false),
                    FechaCaducidad = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accesos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accesos_Acciones_IdAccion",
                        column: x => x.IdAccion,
                        principalSchema: "Seguridad",
                        principalTable: "Acciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accesos_Roles_IdRol",
                        column: x => x.IdRol,
                        principalSchema: "Seguridad",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecursosAccion",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioCreo = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    IdUsuarioModifico = table.Column<string>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    IdRol = table.Column<int>(nullable: false),
                    IdRecurso = table.Column<int>(nullable: false),
                    EsLectura = table.Column<bool>(nullable: false),
                    EsEscritura = table.Column<bool>(nullable: false),
                    EsEjecucion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecursosAccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecursosAccion_Acciones_IdRecurso",
                        column: x => x.IdRecurso,
                        principalSchema: "Seguridad",
                        principalTable: "Acciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecursosAccion_Roles_IdRol",
                        column: x => x.IdRol,
                        principalSchema: "Seguridad",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accesos_IdAccion",
                schema: "Seguridad",
                table: "Accesos",
                column: "IdAccion");

            migrationBuilder.CreateIndex(
                name: "IX_Accesos_IdRol",
                schema: "Seguridad",
                table: "Accesos",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Acciones_IdOpcionModulo",
                schema: "Seguridad",
                table: "Acciones",
                column: "IdOpcionModulo");

            migrationBuilder.CreateIndex(
                name: "IX_OpcionesModulo_IdModulo",
                schema: "Seguridad",
                table: "OpcionesModulo",
                column: "IdModulo");

            migrationBuilder.CreateIndex(
                name: "IX_RecursosAccion_IdRecurso",
                schema: "Seguridad",
                table: "RecursosAccion",
                column: "IdRecurso");

            migrationBuilder.CreateIndex(
                name: "IX_RecursosAccion_IdRol",
                schema: "Seguridad",
                table: "RecursosAccion",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_RecursosGrupo_IdRecurso",
                schema: "Seguridad",
                table: "RecursosGrupo",
                column: "IdRecurso");

            migrationBuilder.CreateIndex(
                name: "IX_RecursosGrupo_IdRol",
                schema: "Seguridad",
                table: "RecursosGrupo",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_RecursosRol_IdRecurso",
                schema: "Seguridad",
                table: "RecursosRol",
                column: "IdRecurso");

            migrationBuilder.CreateIndex(
                name: "IX_RecursosRol_IdRol",
                schema: "Seguridad",
                table: "RecursosRol",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_RolesGrupo_IdGrupo",
                schema: "Seguridad",
                table: "RolesGrupo",
                column: "IdGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_RolesGrupo_IdRol",
                schema: "Seguridad",
                table: "RolesGrupo",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_RolUsuarios_IdRol",
                schema: "Seguridad",
                table: "RolUsuarios",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_RolUsuarios_IdUsuario",
                schema: "Seguridad",
                table: "RolUsuarios",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Sesiones_IdUsuario",
                schema: "Seguridad",
                table: "Sesiones",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosGrupo_IdGrupo",
                schema: "Seguridad",
                table: "UsuariosGrupo",
                column: "IdGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosGrupo_IdUsuario",
                schema: "Seguridad",
                table: "UsuariosGrupo",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accesos",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "RecursosAccion",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "RecursosGrupo",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "RecursosRol",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "RolesDirectos",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "RolesGrupo",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "RolesParticulares",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "RolesUsuario",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "RolUsuarios",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Sesiones",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "UsuariosGrupo",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Acciones",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Grupos",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "OpcionesModulo",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Modulos",
                schema: "Seguridad");
        }
    }
}

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'Seguridad') IS NULL EXEC(N'CREATE SCHEMA [Seguridad];');

GO

CREATE TABLE [Seguridad].[Grupos] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdUsuarioModifico] nvarchar(max) NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [IdUsuarioElimino] nvarchar(max) NULL,
    [FechaEliminacion] datetime2 NULL,
    [Activo] bit NOT NULL,
    [Nombre] nvarchar(100) NOT NULL,
    [Descripcion] nvarchar(150) NULL,
    CONSTRAINT [PK_Grupos] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Seguridad].[Modulos] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    [Activo] bit NOT NULL,
    CONSTRAINT [PK_Modulos] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Seguridad].[Roles] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdUsuarioModifico] nvarchar(max) NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [IdUsuarioElimino] nvarchar(max) NULL,
    [FechaEliminacion] datetime2 NULL,
    [Activo] bit NOT NULL,
    [Nombre] nvarchar(100) NOT NULL,
    [Descripcion] nvarchar(150) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Seguridad].[RolesDirectos] (
    [Id] int NOT NULL IDENTITY,
    [SubjectId] nvarchar(max) NULL,
    [IdRol] int NOT NULL,
    CONSTRAINT [PK_RolesDirectos] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Seguridad].[RolesParticulares] (
    [Id] int NOT NULL IDENTITY,
    [SubjectId] nvarchar(max) NULL,
    [IdRol] int NOT NULL,
    CONSTRAINT [PK_RolesParticulares] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Seguridad].[RolesUsuario] (
    [Id] int NOT NULL IDENTITY,
    [SubjectId] nvarchar(max) NULL,
    [IdRol] int NOT NULL,
    CONSTRAINT [PK_RolesUsuario] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Seguridad].[Usuarios] (
    [Id] int NOT NULL IDENTITY,
    [SubjectId] nvarchar(max) NOT NULL,
    [Nombre] nvarchar(max) NOT NULL,
    [Apellidos] nvarchar(max) NOT NULL,
    [CorreoElectronicoPersonal] nvarchar(max) NULL,
    [CorreoElectronicoLaboral] nvarchar(max) NOT NULL,
    [TelefonoPersonal] nvarchar(max) NULL,
    [TelefonoLaboral] nvarchar(max) NOT NULL,
    [Extension] nvarchar(max) NOT NULL,
    [NombreUsuario] nvarchar(max) NOT NULL,
    [NumeroEmpleado] nvarchar(max) NOT NULL,
    [AreaAdscripcion] nvarchar(max) NOT NULL,
    [Dependencia] nvarchar(max) NOT NULL,
    [EsCliente] bit NOT NULL,
    [EsHabilitado] bit NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [SesionesPermitidas] int NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Seguridad].[OpcionesModulo] (
    [Id] int NOT NULL IDENTITY,
    [IdModulo] int NOT NULL,
    [Nombre] nvarchar(max) NOT NULL,
    [Activo] bit NOT NULL,
    CONSTRAINT [PK_OpcionesModulo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OpcionesModulo_Modulos_IdModulo] FOREIGN KEY ([IdModulo]) REFERENCES [Seguridad].[Modulos] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Seguridad].[RecursosGrupo] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdUsuarioModifico] nvarchar(max) NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [IdRol] int NOT NULL,
    [IdRecurso] int NOT NULL,
    [EsLectura] bit NOT NULL,
    [EsEscritura] bit NOT NULL,
    [EsEjecucion] bit NOT NULL,
    CONSTRAINT [PK_RecursosGrupo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RecursosGrupo_Grupos_IdRecurso] FOREIGN KEY ([IdRecurso]) REFERENCES [Seguridad].[Grupos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RecursosGrupo_Roles_IdRol] FOREIGN KEY ([IdRol]) REFERENCES [Seguridad].[Roles] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Seguridad].[RecursosRol] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdUsuarioModifico] nvarchar(max) NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [IdRol] int NOT NULL,
    [IdRecurso] int NOT NULL,
    [EsLectura] bit NOT NULL,
    [EsEscritura] bit NOT NULL,
    [EsEjecucion] bit NOT NULL,
    CONSTRAINT [PK_RecursosRol] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RecursosRol_Roles_IdRecurso] FOREIGN KEY ([IdRecurso]) REFERENCES [Seguridad].[Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RecursosRol_Roles_IdRol] FOREIGN KEY ([IdRol]) REFERENCES [Seguridad].[Roles] ([Id])
);

GO

CREATE TABLE [Seguridad].[RolesGrupo] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdRol] int NOT NULL,
    [IdGrupo] int NOT NULL,
    CONSTRAINT [PK_RolesGrupo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RolesGrupo_Grupos_IdGrupo] FOREIGN KEY ([IdGrupo]) REFERENCES [Seguridad].[Grupos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RolesGrupo_Roles_IdRol] FOREIGN KEY ([IdRol]) REFERENCES [Seguridad].[Roles] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Seguridad].[RolUsuarios] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdUsuario] int NOT NULL,
    [IdRol] int NOT NULL,
    CONSTRAINT [PK_RolUsuarios] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RolUsuarios_Roles_IdRol] FOREIGN KEY ([IdRol]) REFERENCES [Seguridad].[Roles] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_RolUsuarios_Usuarios_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Seguridad].[Usuarios] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Seguridad].[Sesiones] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuario] int NOT NULL,
    [TokenCount] int NOT NULL,
    [Token] nvarchar(max) NOT NULL,
    [Inicio] datetime2 NOT NULL,
    [Expira] datetime2 NOT NULL,
    CONSTRAINT [PK_Sesiones] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Sesiones_Usuarios_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Seguridad].[Usuarios] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Seguridad].[UsuariosGrupo] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdUsuario] int NOT NULL,
    [IdGrupo] int NOT NULL,
    CONSTRAINT [PK_UsuariosGrupo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UsuariosGrupo_Grupos_IdGrupo] FOREIGN KEY ([IdGrupo]) REFERENCES [Seguridad].[Grupos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UsuariosGrupo_Usuarios_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Seguridad].[Usuarios] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Seguridad].[Acciones] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdUsuarioModifico] nvarchar(max) NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [IdUsuarioElimino] nvarchar(max) NULL,
    [FechaEliminacion] datetime2 NULL,
    [Activo] bit NOT NULL,
    [IdOpcionModulo] int NOT NULL,
    [Nombre] nvarchar(max) NOT NULL,
    [EsVisible] bit NOT NULL,
    [EsPrincipal] bit NOT NULL,
    [Ruta] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Acciones] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Acciones_OpcionesModulo_IdOpcionModulo] FOREIGN KEY ([IdOpcionModulo]) REFERENCES [Seguridad].[OpcionesModulo] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Seguridad].[Accesos] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdUsuarioModifico] nvarchar(max) NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [IdUsuarioElimino] nvarchar(max) NULL,
    [FechaEliminacion] datetime2 NULL,
    [Activo] bit NOT NULL,
    [IdAccion] int NOT NULL,
    [IdRol] int NOT NULL,
    [FechaCaducidad] datetime2 NOT NULL,
    CONSTRAINT [PK_Accesos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Accesos_Acciones_IdAccion] FOREIGN KEY ([IdAccion]) REFERENCES [Seguridad].[Acciones] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Accesos_Roles_IdRol] FOREIGN KEY ([IdRol]) REFERENCES [Seguridad].[Roles] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Seguridad].[RecursosAccion] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdUsuarioModifico] nvarchar(max) NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [IdRol] int NOT NULL,
    [IdRecurso] int NOT NULL,
    [EsLectura] bit NOT NULL,
    [EsEscritura] bit NOT NULL,
    [EsEjecucion] bit NOT NULL,
    CONSTRAINT [PK_RecursosAccion] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RecursosAccion_Acciones_IdRecurso] FOREIGN KEY ([IdRecurso]) REFERENCES [Seguridad].[Acciones] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_RecursosAccion_Roles_IdRol] FOREIGN KEY ([IdRol]) REFERENCES [Seguridad].[Roles] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Accesos_IdAccion] ON [Seguridad].[Accesos] ([IdAccion]);

GO

CREATE INDEX [IX_Accesos_IdRol] ON [Seguridad].[Accesos] ([IdRol]);

GO

CREATE INDEX [IX_Acciones_IdOpcionModulo] ON [Seguridad].[Acciones] ([IdOpcionModulo]);

GO

CREATE INDEX [IX_OpcionesModulo_IdModulo] ON [Seguridad].[OpcionesModulo] ([IdModulo]);

GO

CREATE INDEX [IX_RecursosAccion_IdRecurso] ON [Seguridad].[RecursosAccion] ([IdRecurso]);

GO

CREATE INDEX [IX_RecursosAccion_IdRol] ON [Seguridad].[RecursosAccion] ([IdRol]);

GO

CREATE INDEX [IX_RecursosGrupo_IdRecurso] ON [Seguridad].[RecursosGrupo] ([IdRecurso]);

GO

CREATE INDEX [IX_RecursosGrupo_IdRol] ON [Seguridad].[RecursosGrupo] ([IdRol]);

GO

CREATE INDEX [IX_RecursosRol_IdRecurso] ON [Seguridad].[RecursosRol] ([IdRecurso]);

GO

CREATE INDEX [IX_RecursosRol_IdRol] ON [Seguridad].[RecursosRol] ([IdRol]);

GO

CREATE INDEX [IX_RolesGrupo_IdGrupo] ON [Seguridad].[RolesGrupo] ([IdGrupo]);

GO

CREATE INDEX [IX_RolesGrupo_IdRol] ON [Seguridad].[RolesGrupo] ([IdRol]);

GO

CREATE INDEX [IX_RolUsuarios_IdRol] ON [Seguridad].[RolUsuarios] ([IdRol]);

GO

CREATE INDEX [IX_RolUsuarios_IdUsuario] ON [Seguridad].[RolUsuarios] ([IdUsuario]);

GO

CREATE INDEX [IX_Sesiones_IdUsuario] ON [Seguridad].[Sesiones] ([IdUsuario]);

GO

CREATE INDEX [IX_UsuariosGrupo_IdGrupo] ON [Seguridad].[UsuariosGrupo] ([IdGrupo]);

GO

CREATE INDEX [IX_UsuariosGrupo_IdUsuario] ON [Seguridad].[UsuariosGrupo] ([IdUsuario]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220603181000_Seguridad_0001', N'3.1.22');

GO


IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'Viaticos') IS NULL EXEC(N'CREATE SCHEMA [Viaticos];');

GO

CREATE TABLE [Viaticos].[EntePublicos] (
    [Id] int NOT NULL IDENTITY,
    [Descripcion] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_EntePublicos] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Viaticos].[NivelEmpleados] (
    [Id] int NOT NULL IDENTITY,
    [Nivel] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_NivelEmpleados] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Viaticos].[Flujos] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdUsuarioModifico] nvarchar(max) NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [IdUsuarioElimino] nvarchar(max) NULL,
    [FechaEliminacion] datetime2 NULL,
    [Activo] bit NOT NULL,
    [IdNivelEmpleado] int NOT NULL,
    [IdEntePublico] int NOT NULL,
    [TipoFlujo] int NOT NULL,
    [NombreFlujo] nvarchar(max) NOT NULL,
    [Clasificacion] int NOT NULL,
    CONSTRAINT [PK_Flujos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Flujos_EntePublicos_IdEntePublico] FOREIGN KEY ([IdEntePublico]) REFERENCES [Viaticos].[EntePublicos] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Flujos_NivelEmpleados_IdNivelEmpleado] FOREIGN KEY ([IdNivelEmpleado]) REFERENCES [Viaticos].[NivelEmpleados] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Viaticos].[Pasos] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdUsuarioModifico] nvarchar(max) NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [IdUsuarioElimino] nvarchar(max) NULL,
    [FechaEliminacion] datetime2 NULL,
    [Activo] bit NOT NULL,
    [IdRol] int NOT NULL,
    [IdConfiguracionFlujo] int NOT NULL,
    [Orden] int NOT NULL,
    [TipoRol] int NOT NULL,
    [EsFirma] bit NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Pasos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pasos_Flujos_IdConfiguracionFlujo] FOREIGN KEY ([IdConfiguracionFlujo]) REFERENCES [Viaticos].[Flujos] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Flujos_IdEntePublico] ON [Viaticos].[Flujos] ([IdEntePublico]);

GO

CREATE INDEX [IX_Flujos_IdNivelEmpleado] ON [Viaticos].[Flujos] ([IdNivelEmpleado]);

GO

CREATE INDEX [IX_Pasos_IdConfiguracionFlujo] ON [Viaticos].[Pasos] ([IdConfiguracionFlujo]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220928185816_viaticos', N'3.1.2');

GO


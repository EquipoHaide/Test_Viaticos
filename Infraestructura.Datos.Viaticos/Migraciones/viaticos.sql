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
    [IdTipoEnte] int NOT NULL,
    [IdNivelEmpleado] int NOT NULL,
    [TipoFlujo] int NOT NULL,
    CONSTRAINT [PK_Flujos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Flujos_EntePublicos_IdTipoEnte] FOREIGN KEY ([IdTipoEnte]) REFERENCES [Viaticos].[EntePublicos] ([Id]) ON DELETE NO ACTION
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
    [IdFlujo] int NOT NULL,
    [IdRolAutoriza] int NOT NULL,
    [TipoRol] int NOT NULL,
    [Orden] int NOT NULL,
    [AplicaFirma] bit NOT NULL,
    CONSTRAINT [PK_Pasos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pasos_Flujos_IdFlujo] FOREIGN KEY ([IdFlujo]) REFERENCES [Viaticos].[Flujos] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Flujos_IdTipoEnte] ON [Viaticos].[Flujos] ([IdTipoEnte]);

GO

CREATE INDEX [IX_Pasos_IdFlujo] ON [Viaticos].[Pasos] ([IdFlujo]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221005191823_viaticos', N'3.1.2');

GO


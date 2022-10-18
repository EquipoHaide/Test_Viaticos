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

CREATE TABLE [Viaticos].[Autorizaciones] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdUsuarioModifico] nvarchar(max) NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [IdUsuarioElimino] nvarchar(max) NULL,
    [FechaEliminacion] datetime2 NULL,
    [Activo] bit NOT NULL,
    [IdFlujo] int NOT NULL,
    [Orden] int NOT NULL,
    [IdRol] int NOT NULL,
    [Sello] nvarchar(max) NULL,
    [Estado] int NOT NULL,
    [IdUsuarioAutorizacion] nvarchar(max) NULL,
    [FechaAutorizacion] datetime2 NULL,
    [IdUsuarioCancelacion] nvarchar(max) NULL,
    [FechaCancelacion] datetime2 NULL,
    CONSTRAINT [PK_Autorizaciones] PRIMARY KEY ([Id])
);

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

CREATE TABLE [Viaticos].[SolicitudesCondensadas] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdAutorizacion] int NOT NULL,
    [Folio] nvarchar(max) NOT NULL,
    [Concepto] nvarchar(max) NULL,
    [Estado] int NOT NULL,
    [Orden] int NOT NULL,
    [IdRol] int NOT NULL,
    [IdUsuarioAutorizacion] nvarchar(max) NULL,
    [FechaAutorizacion] datetime2 NULL,
    [IdUsuarioCancelacion] nvarchar(max) NULL,
    [FechaCancelacion] datetime2 NULL,
    [FechaAfectacion] datetime2 NOT NULL,
    CONSTRAINT [PK_SolicitudesCondensadas] PRIMARY KEY ([Id])
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
    [IdNivelEmpleado] int NULL,
    [TipoFlujo] int NOT NULL,
    CONSTRAINT [PK_Flujos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Flujos_EntePublicos_IdTipoEnte] FOREIGN KEY ([IdTipoEnte]) REFERENCES [Viaticos].[EntePublicos] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Viaticos].[HistorialesFlujos] (
    [Id] int NOT NULL IDENTITY,
    [IdFlujo] int NOT NULL,
    [IdTipoEnte] int NOT NULL,
    [IdNivelEmpleado] int NULL,
    [TipoFlujo] int NOT NULL,
    [IdUsuarioModifico] nvarchar(max) NOT NULL,
    [OperacionInicio] datetime2 NOT NULL,
    [OperacionFin] datetime2 NOT NULL,
    CONSTRAINT [PK_HistorialesFlujos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_HistorialesFlujos_Flujos_IdFlujo] FOREIGN KEY ([IdFlujo]) REFERENCES [Viaticos].[Flujos] ([Id]) ON DELETE NO ACTION
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

CREATE TABLE [Viaticos].[HitorialesPasos] (
    [Id] int NOT NULL IDENTITY,
    [IdPaso] int NOT NULL,
    [IdFlujo] int NOT NULL,
    [IdRolAutoriza] int NOT NULL,
    [TipoRol] int NOT NULL,
    [Orden] int NOT NULL,
    [AplicaFirma] bit NOT NULL,
    [IdUsuarioModifico] nvarchar(max) NOT NULL,
    [OperacionInicio] datetime2 NOT NULL,
    [OperacionFin] datetime2 NOT NULL,
    CONSTRAINT [PK_HitorialesPasos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_HitorialesPasos_Pasos_IdPaso] FOREIGN KEY ([IdPaso]) REFERENCES [Viaticos].[Pasos] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Flujos_IdTipoEnte] ON [Viaticos].[Flujos] ([IdTipoEnte]);

GO

CREATE INDEX [IX_HistorialesFlujos_IdFlujo] ON [Viaticos].[HistorialesFlujos] ([IdFlujo]);

GO

CREATE INDEX [IX_HitorialesPasos_IdPaso] ON [Viaticos].[HitorialesPasos] ([IdPaso]);

GO

CREATE INDEX [IX_Pasos_IdFlujo] ON [Viaticos].[Pasos] ([IdFlujo]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221018174223_viaticos_01', N'3.1.2');

GO


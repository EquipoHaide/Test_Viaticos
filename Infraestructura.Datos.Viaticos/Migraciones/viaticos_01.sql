DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Viaticos].[Flujos]') AND [c].[name] = N'IdNivelEmpleado');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Viaticos].[Flujos] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Viaticos].[Flujos] ALTER COLUMN [IdNivelEmpleado] int NULL;

GO

CREATE TABLE [Autorizaciones] (
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
    [FechaAutorizacion] datetime2 NOT NULL,
    [IdUsuarioCancelacion] nvarchar(max) NULL,
    [FechaCancelacion] datetime2 NOT NULL,
    CONSTRAINT [PK_Autorizaciones] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [SolicitudCondensada] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuarioCreo] nvarchar(max) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [IdAutorizacion] int NOT NULL,
    [Folio] nvarchar(max) NULL,
    [Concepto] nvarchar(max) NULL,
    [Estado] int NOT NULL,
    [Orden] int NOT NULL,
    [IdRol] int NOT NULL,
    [IdUsuarioAutorizacion] nvarchar(max) NULL,
    [FechaAutorizacion] datetime2 NOT NULL,
    [IdUsuarioCancelacion] nvarchar(max) NULL,
    [FechaCancelacion] datetime2 NOT NULL,
    [FechaAfectacion] datetime2 NOT NULL,
    CONSTRAINT [PK_SolicitudCondensada] PRIMARY KEY ([Id])
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

CREATE INDEX [IX_HistorialesFlujos_IdFlujo] ON [Viaticos].[HistorialesFlujos] ([IdFlujo]);

GO

CREATE INDEX [IX_HitorialesPasos_IdPaso] ON [Viaticos].[HitorialesPasos] ([IdPaso]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221018170229_viaticos_01', N'3.1.2');

GO


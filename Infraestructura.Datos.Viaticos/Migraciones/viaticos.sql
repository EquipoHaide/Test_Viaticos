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

CREATE TABLE [Viaticos].[EntePublico] (
    [Id] int NOT NULL IDENTITY,
    [Descripcion] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_EntePublico] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Viaticos].[NivelEmpleado] (
    [Id] int NOT NULL IDENTITY,
    [Nivel] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_NivelEmpleado] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Viaticos].[Flujo] (
    [Id] int NOT NULL IDENTITY,
    [IdNivelEmpleado] int NOT NULL,
    [IdEntePublico] int NOT NULL,
    [TipoFlujo] int NOT NULL,
    [NombreFlujo] nvarchar(max) NOT NULL,
    [Activo] bit NOT NULL,
    [DescripcionEntePublico] nvarchar(max) NULL,
    [Nivel] nvarchar(max) NULL,
    [idEntePublico] int NULL,
    CONSTRAINT [PK_Flujo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Flujo_NivelEmpleado_IdNivelEmpleado] FOREIGN KEY ([IdNivelEmpleado]) REFERENCES [Viaticos].[NivelEmpleado] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Flujo_EntePublico_idEntePublico] FOREIGN KEY ([idEntePublico]) REFERENCES [Viaticos].[EntePublico] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Viaticos].[Paso] (
    [Id] int NOT NULL IDENTITY,
    [IdRol] int NOT NULL,
    [IdConfiguracionFlujo] int NOT NULL,
    [Orden] int NOT NULL,
    [TipoRol] int NOT NULL,
    [EsFirma] bit NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Paso] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Paso_Flujo_IdConfiguracionFlujo] FOREIGN KEY ([IdConfiguracionFlujo]) REFERENCES [Viaticos].[Flujo] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Flujo_IdNivelEmpleado] ON [Viaticos].[Flujo] ([IdNivelEmpleado]);

GO

CREATE INDEX [IX_Flujo_idEntePublico] ON [Viaticos].[Flujo] ([idEntePublico]);

GO

CREATE INDEX [IX_Paso_IdConfiguracionFlujo] ON [Viaticos].[Paso] ([IdConfiguracionFlujo]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220923185919_viaticos', N'3.1.2');

GO


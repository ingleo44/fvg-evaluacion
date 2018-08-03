IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Promociones] (
    [IdPromotion] int NOT NULL IDENTITY,
    [MaxCantidadDeCuotas] int NULL,
    [FechaInicio] datetime2 NOT NULL,
    [FechaFin] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [PorcentajeDecuento] decimal(18, 2) NULL,
    [Activo] bit NOT NULL,
    CONSTRAINT [PK_Promociones] PRIMARY KEY ([IdPromotion])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180802190926_InitialMigration', N'2.1.1-rtm-30846');

GO


-- Crear base de datos
CREATE DATABASE ProyectoDAWA;
GO

USE ProyectoDAWA;
GO

-- Crear tablas
CREATE TABLE Usuarios (
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL UNIQUE, -- Nombre único
    Correo VARCHAR(50) NOT NULL UNIQUE, -- Correo único
    Contrasena VARCHAR(50) NOT NULL,
    Rol TINYINT NOT NULL CHECK (Rol IN (1,2,3,4,5)) -- 1 = Gestor, 2 = MiembroComision, 3 = Revisor, 4 = Secretaria, 5 = Estudiante
);
GO

CREATE TABLE Periodos(
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Estado BIT DEFAULT NULL, -- 1=Activo, 0=Inactivo, NULL=En Planificación(Indeterminado)
    FechaInicio DATE NOT NULL UNIQUE,
    FechaFin DATE NOT NULL UNIQUE,
    CicloActual VARCHAR(20) UNIQUE
);
GO

CREATE TABLE Propuestas(
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Titulo VARCHAR(50) NOT NULL,
    Descripcion TEXT NOT NULL,
    FechaCreación DATETIME NOT NULL DEFAULT GETDATE(),
    Calificacion FLOAT DEFAULT NULL, -- Inicia sin calificación
    RevisorId INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id) -- Relación con el revisor
);
GO

-- Crear tabla intermedia para asociar estudiantes y propuestas
CREATE TABLE EstudiantesPropuestas (
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    EstudianteId INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    PropuestaId INT NOT NULL FOREIGN KEY REFERENCES Propuestas(Id),
    UNIQUE (EstudianteId), -- Un estudiante solo puede estar en una propuesta
    UNIQUE (PropuestaId, EstudianteId) -- Relación única por propuesta y estudiante
);
GO

-- Crear triggers y validaciones

-- Garantizar que cada propuesta tenga exactamente uno o dos estudiantes
CREATE TRIGGER VerificarCantidadEstudiantes
ON EstudiantesPropuestas
AFTER INSERT, DELETE
AS
BEGIN
    IF EXISTS (
        SELECT p.Id
        FROM Propuestas p
        LEFT JOIN (
            SELECT PropuestaId, COUNT(*) AS TotalEstudiantes
            FROM EstudiantesPropuestas
            GROUP BY PropuestaId
        ) ep ON p.Id = ep.PropuestaId
        WHERE ISNULL(ep.TotalEstudiantes, 0) NOT BETWEEN 1 AND 2
    )
    BEGIN
        RAISERROR('Cada propuesta debe tener exactamente uno o dos estudiantes.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- Garantizar que solo estudiantes (rol = 5) puedan asociarse a propuestas
CREATE TRIGGER VerificarRolEstudiante
ON EstudiantesPropuestas
AFTER INSERT
AS
BEGIN
    IF EXISTS (
        SELECT e.EstudianteId
        FROM EstudiantesPropuestas e
        JOIN Usuarios u ON e.EstudianteId = u.Id
        WHERE u.Rol <> 5
    )
    BEGIN
        RAISERROR('Solo usuarios con rol de estudiante (5) pueden asociarse a propuestas.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

CREATE TABLE HistorialPropuestas (
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    DireccionArchivo VARCHAR(255) NOT NULL,
    FechaEnvio DATETIME NOT NULL DEFAULT GETDATE(),
    EstadoAprobacion BIT DEFAULT NULL, -- 1=Aprobado, 0=Desaprobado, NULL=Indeterminado(Aún sin calificación)
    ObservacionRevisor TEXT,
    ComentarioEstudiante TEXT,
    PropuestaId INT NOT NULL FOREIGN KEY REFERENCES Propuestas(Id) -- Relación con la propuesta
);
GO

CREATE TABLE Comisiones(
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    Nombre VARCHAR(100) UNIQUE NOT NULL,
    Proposito TEXT NOT NULL, -- Motivo de su existencia
    Estado BIT NOT NULL, -- 1=Activo, 0=Inactivo
    FechaCreación DATETIME NOT NULL DEFAULT GETDATE(),
    GestorId INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id) -- Gestor que creó la comisión
);
GO

CREATE TABLE MiembrosComision(
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    MiembrosComisionId INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    CoordinadorComisionId INT FOREIGN KEY REFERENCES MiembrosComision(Id) -- Autorreferencia
);
GO

-- Crear triggers adicionales
CREATE TRIGGER VerificarRevisorId
ON Propuestas
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @RevisorId INT;
    SELECT @RevisorId = RevisorId FROM inserted;
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Id = @RevisorId AND Rol = 3)
    BEGIN
        RAISERROR('El RevisorId debe ser un usuario con rol 3 (Revisor).', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

CREATE TRIGGER VerificarGestorId
ON Comisiones
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @GestorId INT;
    SELECT @GestorId = GestorId FROM inserted;
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Id = @GestorId AND Rol = 1)
    BEGIN
        RAISERROR('El GestorId debe ser un usuario con rol 1 (Gestor).', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

CREATE TRIGGER VerificarMiembrosComisionId
ON MiembrosComision
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @MiembrosComisionId INT;
    SELECT @MiembrosComisionId = MiembrosComisionId FROM inserted;

    -- Verificar si el MiembrosComisionId es un usuario con rol 2 (MiembroComision)
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Id = @MiembrosComisionId AND Rol = 2)
    BEGIN
        RAISERROR('El MiembrosComisionId debe ser un usuario con rol 2 (MiembroComision).', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO


GO
USE ProyectoDAWA;
GO

-- Insertar usuarios
INSERT INTO Usuarios (Nombre, Correo, Contrasena, Rol) 
VALUES
('CARRILLO SANCHEZ NARCISA MARIA', 'narcisa.carrillo@ug.edu.ec', 'Contrasena123', 1), -- Gestor
('DUCHI RIVERA WALTER ALEJANDRO', 'walter.duchiriv@ug.edu.ec', 'Contrasena123', 5), -- Estudiante
('MENDOZA CHACON EMILIO JULIAN', 'emilio.mendoza@ug.edu.ec', 'Contrasena123', 5), -- Estudiante
('PACHECO ESPEJO MANUEL FERNANDO', 'manuel.pacheco@ug.edu.ec', 'Contrasena123', 5), -- Estudiante
('LINDAO ALEJANDRO JOSEPH GUILLERMO', 'alejandro.lindao@ug.edu.ec', 'Contrasena123', 3), -- Revisor
('TOALA MERCHAN MADELINE CAROLINA', 'madeline.toala@ug.edu.ec', 'Contrasena123', 2), -- Miembro de comisión
('DUCHI VELEZ MADELINE NARCISA', 'madeline.duchi@ug.edu.ec', 'Contrasena123', 2), -- Miembro de comisión
('VELEZ PULIDO CHRISTOPHER JEREMY', 'christopher.velez@ug.edu.ec', 'Contrasena123', 4); -- Secretaria
GO

-- Insertar periodos
INSERT INTO Periodos (Estado, FechaInicio, FechaFin, CicloActual) 
VALUES 
(1, '2025-01-01', '2025-06-30', 'CI 2025-2026'),
(0, '2025-07-01', '2025-12-31', 'CII 2025-2026');
GO

-- Insertar propuestas
INSERT INTO Propuestas (Titulo, Descripcion, RevisorId) 
VALUES
('Desarrollo de Sistema de Gestión Académica', 'Propuesta para desarrollar un sistema de gestión de estudiantes y profesores en la facultad de ingeniería.', 5),
('Aplicación de Gestión de Proyectos de Software', 'Aplicación para gestionar proyectos de software en equipos de trabajo de la carrera de Ingeniería en Software.', 5);
GO

-- Insertar estudiantes en las propuestas
INSERT INTO EstudiantesPropuestas (EstudianteId, PropuestaId) 
VALUES
(2, 1),-- Walter Alejandro Duchi Rivera en propuesta 1
(3, 1),-- Emilio Mendoza con propuesta 1
(4, 2); --Manuel Pacheco con propuesta 2
GO

-- Insertar historial de propuestas (con ejemplo de PDF como ruta)
INSERT INTO HistorialPropuestas (DireccionArchivo, EstadoAprobacion, PropuestaId) 
VALUES 
('UploadedFiles\\Propuesta1_Walter.pdf', 1, 1), -- Aprobada
('UploadedFiles\\Propuesta2_Walter.pdf', 0, 2); -- Desaprobada
GO

-- Insertar comisiones
INSERT INTO Comisiones (Nombre, Proposito, Estado, GestorId) 
VALUES 
('Comisión de Sistemas Distribuidos', 'Encargada del desarrollo de sistemas distribuidos en la carrera de Ingeniería en Software.', 1, 1),
('Comisión de Desarrollo Web', 'Responsable de la creación de aplicaciones web para la facultad de ingeniería.', 1, 1);
GO

-- Insertar al coordinador de la comisión (Madeline Toala)
INSERT INTO MiembrosComision (MiembrosComisionId, CoordinadorComisionId)
VALUES (6, NULL); -- Madeline Toala como miembro y aún no tiene coordinador
GO


-- Ahora insertar a los miembros de la comisión, referenciando al coordinador
INSERT INTO MiembrosComision (MiembrosComisionId, CoordinadorComisionId)
VALUES (7, 1); -- Madeline Duchi es miembro de la comisión y el coordinador es Madeline Toala.
GO

SELECT * FROM Usuarios;
SELECT * FROM Periodos;
SELECT * FROM Propuestas;
SELECT * FROM EstudiantesPropuestas;
SELECT * FROM HistorialPropuestas;
SELECT * FROM Comisiones;
SELECT * FROM MiembrosComision;
GO

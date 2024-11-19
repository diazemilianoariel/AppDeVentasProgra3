ALTER TABLE Productos
DROP CONSTRAINT FK__Productos__idMar__3D5E1FD2;

ALTER TABLE Productos
ADD CONSTRAINT FK__Productos__idMar__3D5E1FD2
FOREIGN KEY (idMarca)
REFERENCES Marcas(id)
ON DELETE CASCADE;

ALTER TABLE Productos
DROP CONSTRAINT FK__Productos__idMar__3D5E1FD2;


ALTER TABLE Productos
ADD CONSTRAINT FK__Productos__idMar__3D5E1FD2
FOREIGN KEY (idMarca)
REFERENCES Marcas(id);

ALTER TABLE Productos
ALTER COLUMN idMarca INT NULL;

select * from Productos

-- Actualizar los primeros 5 registros con valores aleatorios
WITH Top5Productos AS (
    SELECT TOP 5 id
    FROM Productos
    ORDER BY id
)
UPDATE Productos
SET 
    idMarca = ABS(CHECKSUM(NEWID()) % 100) + 1, -- Genera un valor aleatorio entre 1 y 100
    idTipo = ABS(CHECKSUM(NEWID()) % 100) + 1,  -- Genera un valor aleatorio entre 1 y 100
    idCategoria = ABS(CHECKSUM(NEWID()) % 100) + 1 -- Genera un valor aleatorio entre 1 y 100
FROM Productos
INNER JOIN Top5Productos ON Productos.id = Top5Productos.id;


select * from Proveedores
SELECT * FROM Productos
SELECT * from Marcas
SELECT * from Tipos
SELECT * from Categorias

select * from proveedores_productos

--  agrearle estado a la tabla categorias
ALTER TABLE Tipos
ADD estado BIT NOT NULL DEFAULT 1;
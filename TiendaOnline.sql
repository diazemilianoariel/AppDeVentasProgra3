CREATE DATABASE TiendaOnline;
GO
USE TiendaOnline;
GO

-- Tabla para Marca
CREATE TABLE Marcas (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(50),
    estado BIT  DEFAULT 1 NOT NULL
);

-- Tabla para Tipo
CREATE TABLE Tipos (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(50),
    estado BIT  DEFAULT 1 NOT NULL
);

-- Tabla para Categoria
CREATE TABLE Categorias (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(50),
    estado BIT  DEFAULT 1 NOT NULL
);

-- Tabla para Producto
CREATE TABLE Productos (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(50),
    descripcion NVARCHAR(100),
    precio DECIMAL(10, 2),
    imagen NVARCHAR(255),
    idMarca INT FOREIGN KEY REFERENCES Marcas(id),
    idTipo INT FOREIGN KEY REFERENCES Tipos(id),
    idCategoria INT FOREIGN KEY REFERENCES Categorias(id),
    margenGanancia DECIMAL(10, 2) NOT NULL  ,
    estado BIT  DEFAULT 1 NOT NULL 

);

-- Tabla para Proveedor
CREATE TABLE Proveedores (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(50),
    direccion NVARCHAR(100),
    telefono NVARCHAR(15),
    email NVARCHAR(50),
    estado BIT  DEFAULT 1 NOT NULL
);

-- Tabla intermedia para Proveedor_Producto
CREATE TABLE Proveedores_Productos (
    idProveedor INT FOREIGN KEY REFERENCES Proveedores(id),
    idProducto INT FOREIGN KEY REFERENCES Productos(id),
    PRIMARY KEY (idProveedor, idProducto)
);

-- Tabla para Perfil
CREATE TABLE Perfiles (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(50),
    estado BIT  DEFAULT 1 NOT NULL
    
);

-- Tabla para Usuario
CREATE TABLE Usuarios (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(50),
    apellido NVARCHAR(50),
    dni NVARCHAR(20),
    direccion NVARCHAR(100),
    telefono NVARCHAR(15),
    email NVARCHAR(50),
    clave NVARCHAR(50),
    idPerfil INT FOREIGN KEY REFERENCES Perfiles(id) DEFAULT 1 NOT NULL,
    estado BIT  DEFAULT 1 NOT NULL
);

--- tabla estado de venta
CREATE TABLE EstadoVenta (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(50),
);


-- Tabla para Venta a clientes
CREATE TABLE Ventas (
    id INT PRIMARY KEY IDENTITY(1,1),
    fecha DATE,
    monto DECIMAL(10, 2),
    idUsuario INT FOREIGN KEY REFERENCES Usuarios(id),
    EnLocal bit DEFAULT 0 NOT NULL, -- 0 = Venta Online, 1 = Venta En local
    idEstadoVenta int FOREIGN KEY REFERENCES EstadoVenta(id) DEFAULT 1 NOT NULL
    
);

-- Tabla para DetalleVenta a clientes
CREATE TABLE DetalleVentas (
    id INT PRIMARY KEY IDENTITY(1,1),
    idVenta INT FOREIGN KEY REFERENCES Ventas(id),
    idProducto INT FOREIGN KEY REFERENCES Productos(id),
    cantidad INT,
    precioVenta DECIMAL(10, 2),
    estado BIT  DEFAULT 0 NOT NULL
);

-- Tabla para Compra a proveedores
CREATE TABLE Compras (
    id INT PRIMARY KEY IDENTITY(1,1),
    idProveedor INT FOREIGN KEY REFERENCES Proveedores(id),
    fecha DATE,
    total DECIMAL(10, 2)
);

-- Tabla para DetalleCompras a proveedores
CREATE TABLE DetalleCompras (
    id INT PRIMARY KEY IDENTITY(1,1),
    idCompra INT FOREIGN KEY REFERENCES Compras(id),
    idProducto INT FOREIGN KEY REFERENCES Productos(id),
    cantidad INT,
    precioCompra DECIMAL(10, 2)
);

-- Tabla para Factura
CREATE TABLE Facturas (
    id INT PRIMARY KEY IDENTITY(1,1),
    idVenta INT FOREIGN KEY REFERENCES Ventas(id),
    fecha DATE,
    total DECIMAL(10, 2),
    subTotal DECIMAL(10, 2)
);



-- Tabla para Stock de productos
CREATE TABLE Stock (
    idProducto INT FOREIGN KEY REFERENCES Productos(id),
    cantidad INT,
    stockMinimo INT,
    fechaActualizacion DATE,
    PRIMARY KEY (idProducto)
);


USE TiendaOnline;
GO

-- Insertar datos en la tabla Marcas
INSERT INTO Marcas (nombre) VALUES ('Disney');
INSERT INTO Marcas (nombre) VALUES ('Hasbro');

-- Insertar datos en la tabla Tipos
INSERT INTO Tipos (nombre) VALUES ('Juguetes');
INSERT INTO Tipos (nombre) VALUES ('Ropa');

-- Insertar datos en la tabla Categorias
INSERT INTO Categorias (nombre) VALUES ('Niños');
INSERT INTO Categorias (nombre) VALUES ('Niñas');

-- Insertar datos en la tabla Productos
INSERT INTO Productos (nombre, descripcion, precio, imagen, idMarca, idTipo, idCategoria, margenGanancia) VALUES 
('Peluche Mickey', 'Peluche de Mickey Mouse', 100.00, 'https://images.unsplash.com/photo-1533450718592-29d45635f0a9?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8anBnfGVufDB8fDB8fHww', 1, 1, 1, 20), 
('Peluche Minnie', 'Peluche de Minnie Mouse', 100.00, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSuSw-OdEWAVkZN58ydZ_Mz8CwNzFAEHRHn5g&s', 1, 1, 2, 20),
( 'pañales pampers', 'pañales pampers', 100.00, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSuSw-OdEWAVkZN58ydZ_Mz8CwNzFAEHRHn5g&s', 1, 1, 1, 20),
( 'pañales huggies', 'pañales huggies', 100.00, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSuSw-OdEWAVkZN58ydZ_Mz8CwNzFAEHRHn5g&s', 1, 1, 1, 20);

-- Insertar datos en la tabla Proveedores
INSERT INTO Proveedores (nombre, direccion, telefono, email) VALUES 
('Proveedor 1', 'Direccion 1', '123456789', 'proveedor1@gmail.com'),
('Proveedor 2', 'Direccion 2', '123456789', 'proveedor2@gmail.com');


-- Insertar datos en la tabla Proveedores_Productos
INSERT INTO Proveedores_Productos (idProveedor, idProducto) VALUES (1, 1);
INSERT INTO Proveedores_Productos (idProveedor, idProducto) VALUES (2, 2);

-- Insertar datos en la tabla Perfiles
INSERT INTO Perfiles (nombre) VALUES ('Cliente'); --1
INSERT INTO Perfiles (nombre) VALUES ('Administrador'); --2
INSERT INTO Perfiles (nombre) VALUES ('Vendedor'); --3

-- Insertar datos en la tabla Usuarios
INSERT INTO Usuarios (nombre, apellido, dni, direccion, telefono, email, clave, idPerfil) VALUES 
('Mariel', 'Torres', '6666666', 'Direccion 5', '345678112', 'Soporte@Soporte.com', '12345677' , 1)
('administrador', 'Admin', '12345678', 'Direccion 1', '123456789', 'Administrador@admin.com', '111111', 2),
('Larry', 'Cricione', '12345678', 'Direccion 2', '123456789', 'cliente1@gmial.com', '123456' , 1),
('Nazareno', 'Ligero', '90123456', 'Direccion 3', '012345678', 'cliente2@gmail.com', '123456' , 1),
('vendedor', 'Apellido 4', '78901234', 'Direccion 4', '901234567', 'vendedor@gmail.com', '123456' , 3);

-- Insertar datos en la tabla EstadoVenta
INSERT INTO EstadoVenta (nombre) VALUES ('Pendiente');
INSERT INTO EstadoVenta (nombre) VALUES ('Aprobado');
INSERT INTO EstadoVenta (nombre) VALUES ('Cancelado');

-- Insertar datos en la tabla Ventas
INSERT INTO Ventas (fecha, monto, idUsuario, EnLocal, idEstadoVenta) VALUES ('2021-01-01', 100.00, 1, 0, 1);
INSERT INTO Ventas (fecha, monto, idUsuario, EnLocal, idEstadoVenta) VALUES ('2021-01-02', 200.00, 2, 1, 1);

-- Insertar datos en la tabla DetalleVentas
INSERT INTO DetalleVentas (idVenta, idProducto, cantidad, precioVenta) VALUES (1, 1, 1, 100.00);
INSERT INTO DetalleVentas (idVenta, idProducto, cantidad, precioVenta) VALUES (2, 2, 2, 100.00);

-- Insertar datos en la tabla Compras
INSERT INTO Compras (idProveedor, fecha, total) VALUES (1, '2021-01-01', 100.00);
INSERT INTO Compras (idProveedor, fecha, total) VALUES (2, '2021-01-02', 200.00);

-- Insertar datos en la tabla DetalleCompras
INSERT INTO DetalleCompras (idCompra, idProducto, cantidad, precioCompra) VALUES (1, 1, 1, 100.00);
INSERT INTO DetalleCompras (idCompra, idProducto, cantidad, precioCompra) VALUES (2, 2, 2, 100.00);


-- Insertar datos en la tabla Facturas
INSERT INTO Facturas (idVenta, fecha, total, subTotal) VALUES (1, '2021-01-01', 100.00, 100.00);
INSERT INTO Facturas (idVenta, fecha, total, subTotal) VALUES (2, '2021-01-02', 200.00, 200.00);


-- Insertar datos en la tabla Stock
INSERT INTO Stock (idProducto, cantidad, stockMinimo, fechaActualizacion) VALUES (1, 10, 5, '2021-01-01');
INSERT INTO Stock (idProducto, cantidad, stockMinimo, fechaActualizacion) VALUES (2, 20, 10, '2021-01-02');











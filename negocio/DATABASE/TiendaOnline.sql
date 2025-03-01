/*CREATE DATABASE TiendaOnline;
GO
USE TiendaOnline;
GO

-- Tabla para Marca
CREATE TABLE Marcas (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50)
);

-- Tabla para Tipo
CREATE TABLE Tipos (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50)
);

-- Tabla para Categoria
CREATE TABLE Categorias (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50)
);

-- Tabla para Producto
CREATE TABLE Productos (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50),
    descripcion TEXT,
    precio DECIMAL(10, 2),
    imagen VARCHAR(255),
    idMarca INT FOREIGN KEY REFERENCES Marcas(id),
    idTipo INT FOREIGN KEY REFERENCES Tipos(id),
    idCategoria INT FOREIGN KEY REFERENCES Categorias(id),
    estado VARCHAR(20)
);

-- Tabla para Proveedor
CREATE TABLE Proveedores (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50),
    direccion VARCHAR(100),
    telefono VARCHAR(15),
    email VARCHAR(50)
);

-- Tabla intermedia para Proveedor_Producto
CREATE TABLE Proveedores_Productos (
    idProveedor INT FOREIGN KEY REFERENCES Proveedores(id),
    idProducto INT FOREIGN KEY REFERENCES Productos(id),
    PRIMARY KEY (idProveedor, idProducto)
);

-- Tabla para Cliente
CREATE TABLE Clientes (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50),
    apellido VARCHAR(50),
    dni VARCHAR(20),
    direccion VARCHAR(100),
    telefono VARCHAR(15),
    email VARCHAR(50)
);

-- Tabla para Venta
CREATE TABLE Ventas (
    id INT PRIMARY KEY IDENTITY(1,1),
    fecha DATE,
    monto DECIMAL(10, 2),
    idCliente INT FOREIGN KEY REFERENCES Clientes(id)
);

-- Tabla para DetalleVenta
CREATE TABLE DetalleVentas (
    id INT PRIMARY KEY IDENTITY(1,1),
    idVenta INT FOREIGN KEY REFERENCES Ventas(id),
    idProducto INT FOREIGN KEY REFERENCES Productos(id),
    cantidad INT,
    precioVenta DECIMAL(10, 2)
);

-- Tabla para Compra
CREATE TABLE Compras (
    id INT PRIMARY KEY IDENTITY(1,1),
    idProveedor INT FOREIGN KEY REFERENCES Proveedores(id),
    fecha DATE,
    total DECIMAL(10, 2)
);

-- Tabla para DetalleCompra
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
    subTotal DECIMAL(10, 2),
    iva DECIMAL(10, 2),
    descuento DECIMAL(10, 2)
);

-- Tabla para Perfil
CREATE TABLE Perfiles (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50),
    descripcion TEXT,
    estado VARCHAR(20)
);

-- Tabla para Stock
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
INSERT INTO Marcas (nombre) VALUES 
('Pampers'),
('Avent'),
('Nuk'),
('Nestle'),
('Braun');

-- Insertar datos en la tabla Tipos
INSERT INTO Tipos (nombre) VALUES 
('Infantil'),
('Accesorios'),
('Salud'),
('Alimentación'),
('Baño');

-- Insertar datos en la tabla Categorias
INSERT INTO Categorias (nombre) VALUES 
('Higiene'),
('Alimentación'),
('Accesorios'),
('Salud'),
('Baño');

-- Insertar datos en la tabla Productos
INSERT INTO Productos (nombre, descripcion, precio, imagen, idMarca, idTipo, idCategoria, estado) VALUES 
('Pañales Premium', 'Pañales de alta calidad', 150.00, 'https://example.com/images/paniales.jpg', 1, 1, 1, 'Disponible'),
('Mamadera 250ml', 'Mamadera con válvula anti-cólico', 50.00, 'https://example.com/images/mamadera.jpg', 2, 1, 2, 'Disponible'),
('Chupete Suave', 'Chupete de silicona suave', 20.00, 'https://example.com/images/chupete.jpg', 3, 2, 3, 'Disponible'),
('Leche en Polvo', 'Leche en polvo para bebés', 80.00, 'https://example.com/images/leche.jpg', 4, 1, 2, 'Disponible'),
('Termómetro Digital', 'Termómetro digital de alta precisión', 120.00, 'https://example.com/images/termometro.jpg', 5, 3, 4, 'Disponible');

-- Insertar datos en la tabla Proveedores
INSERT INTO Proveedores (nombre, direccion, telefono, email) VALUES 
('Proveedor 1', 'Calle Falsa 123', '123456789', 'proveedor1@example.com'),
('Proveedor 2', 'Av. Siempre Viva 456', '987654321', 'proveedor2@example.com'),
('Proveedor 3', 'Paseo de la Reforma 789', '456123789', 'proveedor3@example.com'),
('Proveedor 4', 'Gran Vía 101', '321654987', 'proveedor4@example.com'),
('Proveedor 5', 'Av. Libertador 202', '654987321', 'proveedor5@example.com');

-- Insertar datos en la tabla Proveedores_Productos
INSERT INTO Proveedores_Productos (idProveedor, idProducto) VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(1, 5),
(2, 3),
(3, 2),
(4, 1),
(5, 4);

-- Insertar datos en la tabla Clientes
INSERT INTO Clientes (nombre, apellido, dni, direccion, telefono, email) VALUES 
('Juan', 'Pérez', '12345678', 'Calle 1', '111111111', 'juan.perez@example.com'),
('María', 'García', '23456789', 'Calle 2', '222222222', 'maria.garcia@example.com'),
('Carlos', 'Sánchez', '34567890', 'Calle 3', '333333333', 'carlos.sanchez@example.com'),
('Lucía', 'Rodríguez', '45678901', 'Calle 4', '444444444', 'lucia.rodriguez@example.com'),
('Pedro', 'Fernández', '56789012', 'Calle 5', '555555555', 'pedro.fernandez@example.com');

-- Insertar datos en la tabla Ventas
INSERT INTO Ventas (fecha, monto, idCliente) VALUES 
('2024-08-01', 150.00, 1),
('2024-08-02', 300.00, 2),
('2024-08-03', 100.00, 3),
('2024-08-04', 240.00, 4),
('2024-08-05', 120.00, 5);

-- Insertar datos en la tabla DetalleVentas
INSERT INTO DetalleVentas (idVenta, idProducto, cantidad, precioVenta) VALUES 
(1, 1, 1, 150.00),
(2, 2, 2, 100.00),
(3, 3, 3, 30.00),
(4, 4, 1, 240.00),
(5, 5, 1, 120.00);

-- Insertar datos en la tabla Compras
INSERT INTO Compras (idProveedor, fecha, total) VALUES 
(1, '2024-07-15', 1000.00),
(2, '2024-07-16', 2000.00),
(3, '2024-07-17', 1500.00),
(4, '2024-07-18', 1800.00),
(5, '2024-07-19', 1200.00);

-- Insertar datos en la tabla DetalleCompras
INSERT INTO DetalleCompras (idCompra, idProducto, cantidad, precioCompra) VALUES 
(1, 1, 50, 20.00),
(2, 2, 100, 15.00),
(3, 3, 150, 10.00),
(4, 4, 200, 8.00),
(5, 5, 250, 5.00);

-- Insertar datos en la tabla Facturas
INSERT INTO Facturas (idVenta, fecha, total, subTotal, iva, descuento) VALUES 
(1, '2024-08-01', 165.00, 150.00, 15.00, 0.00),
(2, '2024-08-02', 330.00, 300.00, 30.00, 0.00),
(3, '2024-08-03', 110.00, 100.00, 10.00, 0.00),
(4, '2024-08-04', 264.00, 240.00, 24.00, 0.00),
(5, '2024-08-05', 132.00, 120.00, 12.00, 0.00);

-- Insertar datos en la tabla Perfiles
INSERT INTO Perfiles (nombre, descripcion, estado) VALUES 
('Administrador', 'Perfil con acceso total al sistema', 'Activo'),
('Vendedor', 'Perfil con acceso limitado a ventas', 'Activo'),
('Gerente', 'Perfil con acceso a reportes y gestión', 'Activo'),
('Soporte', 'Perfil con acceso a soporte técnico', 'Inactivo'),
('Cliente', 'Perfil con acceso a la tienda en línea', 'Activo');

-- Insertar datos en la tabla Stock
INSERT INTO Stock (idProducto, cantidad, stockMinimo, fechaActualizacion) VALUES 
(1, 100, 20, '2024-08-01'),
(2, 200, 30, '2024-08-01'),
(3, 150, 15, '2024-08-01'),
(4, 80, 10, '2024-08-01'),
(5, 50, 5, '2024-08-01');
*/

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
    fecha datetime,
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
('Peluche Mickey', 'Peluche de Mickey Mouse', 100.00, 'https://http2.mlstatic.com/D_NQ_NP_766265-MLU74210721135_012024-O.webp', 1, 1, 1, 20), 
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
('Mariel', 'Torres', '6666666', 'Direccion 5', '345678112', 'Soporte', '555' , 1),
('Miguel', 'Rodriguez', '12345678', 'Direccion 1', '123456789', 'Admin', '111', 2),
('Larry', 'Lopez', '12345678', 'Direccion 2', '123456789', 'cliente', '222' , 1),
('Nazareno', 'Alvarez', '90123456', 'Direccion 3', '012345678', 'cliente2', '333' , 1),
('Pablo', 'sanchez', '78901234', 'Direccion 4', '901234567', 'vendedor', '444' , 3);

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
--INSERT INTO Compras (idProveedor, fecha, total) VALUES (1, '2021-01-01', 100.00);
--INSERT INTO Compras (idProveedor, fecha, total) VALUES (2, '2021-01-02', 200.00);

-- Insertar datos en la tabla DetalleCompras
INSERT INTO DetalleCompras (idCompra, idProducto, cantidad, precioCompra) VALUES (1, 1, 1, 100.00);
INSERT INTO DetalleCompras (idCompra, idProducto, cantidad, precioCompra) VALUES (2, 2, 2, 100.00);


-- Insertar datos en la tabla Facturas
INSERT INTO Facturas (idVenta, fecha, total, subTotal) VALUES (1, '2021-01-01', 100.00, 100.00);
INSERT INTO Facturas (idVenta, fecha, total, subTotal) VALUES (2, '2021-01-02', 200.00, 200.00);


-- Insertar datos en la tabla Stock
INSERT INTO Stock (idProducto, cantidad, stockMinimo, fechaActualizacion) VALUES (1, 10, 5, '2021-01-01');
INSERT INTO Stock (idProducto, cantidad, stockMinimo, fechaActualizacion) VALUES (2, 20, 10, '2021-01-02');

CREATE DATABASE TiendaOnline;
GO
USE TiendaOnline;
GO

-- Tabla para Producto
CREATE TABLE Productos (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50),
    descripcion TEXT,
    precio DECIMAL(10, 2),
    imagen VARCHAR(255),
    stock INT,
    marca VARCHAR(50),
    tipo VARCHAR(50),
    categoria VARCHAR(50),
    proveedor VARCHAR(50),
    estado VARCHAR(20)
);

-- Insertar datos en la tabla Productos
INSERT INTO Productos(nombre, descripcion, precio, imagen, stock, marca, tipo, categoria, proveedor, estado)
VALUES 
('Pañales Premium', 'Pañales de alta calidad', 150.00, 'imagen1.jpg', 100, 'Pampers', 'Infantil', 'Higiene', 'Proveedor 1', 'Disponible'),
('Mamadera 250ml', 'Mamadera con válvula anti-cólico', 50.00, 'imagen2.jpg', 200, 'Avent', 'Infantil', 'Alimentación', 'Proveedor 2', 'Disponible'),
('Chupete Suave', 'Chupete de silicona suave', 20.00, 'imagen3.jpg', 150, 'Nuk', 'Infantil', 'Accesorios', 'Proveedor 3', 'Disponible'),
('Leche en Polvo', 'Leche en polvo para bebés', 80.00, 'imagen4.jpg', 80, 'Nestle', 'Infantil', 'Alimentación', 'Proveedor 4', 'Disponible'),
('Termómetro Digital', 'Termómetro digital de alta precisión', 120.00, 'imagen5.jpg', 50, 'Braun', 'Salud', 'Accesorios', 'Proveedor 5', 'Disponible'),
('Bañera Bebé', 'Bañera ergonómica para bebé', 200.00, 'imagen6.jpg', 30, 'Fisher-Price', 'Infantil', 'Baño', 'Proveedor 1', 'Disponible');

-- Tabla para Proveedor
CREATE TABLE Proveedores (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50),
    direccion VARCHAR(100),
    telefono VARCHAR(15),
    email VARCHAR(50)
);

-- Insertar datos en la tabla Proveedores
INSERT INTO Proveedores(nombre, direccion, telefono, email)
VALUES 
('Proveedor 1', 'Calle Falsa 123', '123456789', 'proveedor1@example.com'),
('Proveedor 2', 'Av. Siempre Viva 456', '987654321', 'proveedor2@example.com'),
('Proveedor 3', 'Paseo de la Reforma 789', '456123789', 'proveedor3@example.com'),
('Proveedor 4', 'Gran Vía 101', '321654987', 'proveedor4@example.com'),
('Proveedor 5', 'Av. Libertador 202', '654987321', 'proveedor5@example.com');

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

-- Insertar datos en la tabla Clientes
INSERT INTO Clientes(nombre, apellido, dni, direccion, telefono, email)
VALUES 
('Juan', 'Pérez', '12345678', 'Calle 1', '111111111', 'juan.perez@example.com'),
('María', 'García', '23456789', 'Calle 2', '222222222', 'maria.garcia@example.com'),
('Carlos', 'Sánchez', '34567890', 'Calle 3', '333333333', 'carlos.sanchez@example.com'),
('Lucía', 'Rodríguez', '45678901', 'Calle 4', '444444444', 'lucia.rodriguez@example.com'),
('Pedro', 'Fernández', '56789012', 'Calle 5', '555555555', 'pedro.fernandez@example.com');

-- Tabla para Venta
CREATE TABLE Ventas (
    id INT PRIMARY KEY IDENTITY(1,1),
    fecha DATE,
    monto DECIMAL(10, 2),
    idCliente INT FOREIGN KEY REFERENCES Clientes(id)
);

-- Insertar datos en la tabla Ventas
INSERT INTO Ventas(fecha, monto, idCliente)
VALUES 
('2023-08-01', 1500.00, 1),
('2023-08-02', 300.00, 2),
('2023-08-03', 100.00, 3),
('2023-08-04', 2400.00, 4),
('2023-08-05', 1200.00, 5);

-- Tabla para Compra
CREATE TABLE Compras (
    id INT PRIMARY KEY IDENTITY(1,1),
    idProveedor INT FOREIGN KEY REFERENCES Proveedores(id),
    fecha DATE,
    total DECIMAL(10, 2)
);

-- Insertar datos en la tabla Compras
INSERT INTO Compras(idProveedor, fecha, total)
VALUES 
(1, '2023-07-15', 1000.00),
(2, '2023-07-16', 2000.00),
(3, '2023-07-17', 1500.00),
(4, '2023-07-18', 1800.00),
(5, '2023-07-19', 1200.00);

-- Tabla para Factura
CREATE TABLE Facturas (
    id INT PRIMARY KEY IDENTITY(1,1),
    idVenta INT FOREIGN KEY REFERENCES Ventas(id),
    idCliente INT FOREIGN KEY REFERENCES Clientes(id),
    fecha DATE,
    total DECIMAL(10, 2),
    subTotal DECIMAL(10, 2),
    iva DECIMAL(10, 2),
    descuento DECIMAL(10, 2)
);

-- Insertar datos en la tabla Facturas
INSERT INTO Facturas(idVenta, idCliente, fecha, total, subTotal, iva, descuento)
VALUES 
(1, 1, '2023-08-01', 1500.00, 1300.00, 150.00, 50.00),
(2, 2, '2023-08-02', 300.00, 260.00, 30.00, 10.00),
(3, 3, '2023-08-03', 100.00, 85.00, 10.00, 5.00),
(4, 4, '2023-08-04', 2400.00, 2100.00, 240.00, 60.00),
(5, 5, '2023-08-05', 1200.00, 1050.00, 120.00, 30.00);

-- Tabla para Perfil
CREATE TABLE Perfiles (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50),
    descripcion TEXT,
    estado VARCHAR(20)
);

-- Insertar datos en la tabla Perfiles
INSERT INTO Perfiles(nombre, descripcion, estado)
VALUES 
('Administrador', 'Perfil con acceso total al sistema', 'Activo'),
('Vendedor', 'Perfil con acceso limitado a ventas', 'Activo'),
('Gerente', 'Perfil con acceso a reportes y gestión', 'Activo'),
('Soporte', 'Perfil con acceso a soporte técnico', 'Inactivo'),
('Cliente', 'Perfil con acceso a la tienda en línea', 'Activo');

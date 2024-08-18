 CREATE DATABASE TiendaOnline;
GO
USE TiendaOnline;
GO


-- Tabla para Producto
CREATE TABLE Productos (
    idProducto INT PRIMARY KEY IDENTITY(1,1),
    nombreProducto VARCHAR(50),
    descripcionProducto TEXT,
    precioProducto DECIMAL(10, 2),
    imagenProducto VARCHAR(255),
    stockProducto INT,
    marcaProducto VARCHAR(50),
    tipoProducto VARCHAR(50),
    categoriaProducto VARCHAR(50),
    proveedorProducto VARCHAR(50),
    estadoProducto VARCHAR(20)
);

-- Insertar datos en la tabla Productos
INSERT INTO Productos(nombreProducto, descripcionProducto, precioProducto, imagenProducto, stockProducto, marcaProducto, tipoProducto, categoriaProducto, proveedorProducto, estadoProducto)
VALUES 
('Pañales Premium', 'Pañales de alta calidad', 150.00, 'imagen1.jpg', 100, 'Pampers', 'Infantil', 'Higiene', 'Proveedor 1', 'Disponible'),
('Mamadera 250ml', 'Mamadera con válvula anti-cólico', 50.00, 'imagen2.jpg', 200, 'Avent', 'Infantil', 'Alimentación', 'Proveedor 2', 'Disponible'),
('Chupete Suave', 'Chupete de silicona suave', 20.00, 'imagen3.jpg', 150, 'Nuk', 'Infantil', 'Accesorios', 'Proveedor 3', 'Disponible'),
('Leche en Polvo', 'Leche en polvo para bebés', 80.00, 'imagen4.jpg', 80, 'Nestle', 'Infantil', 'Alimentación', 'Proveedor 4', 'Disponible'),
('Termómetro Digital', 'Termómetro digital de alta precisión', 120.00, 'imagen5.jpg', 50, 'Braun', 'Salud', 'Accesorios', 'Proveedor 5', 'Disponible'),
('Bañera Bebé', 'Bañera ergonómica para bebé', 200.00, 'imagen6.jpg', 30, 'Fisher-Price', 'Infantil', 'Baño', 'Proveedor 1', 'Disponible');

-- Tabla para Proveedor
CREATE TABLE Proveedores (
    idProveedor INT PRIMARY KEY IDENTITY(1,1),
    nombreProveedor VARCHAR(50),
    direccionProveedor VARCHAR(100),
    telefonoProveedor VARCHAR(15),
    emailProveedor VARCHAR(50)
);

-- Insertar datos en la tabla Proveedores
INSERT INTO Proveedores(nombreProveedor, direccionProveedor, telefonoProveedor, emailProveedor)
VALUES 
('Proveedor 1', 'Calle Falsa 123', '123456789', 'proveedor1@example.com'),
('Proveedor 2', 'Av. Siempre Viva 456', '987654321', 'proveedor2@example.com'),
('Proveedor 3', 'Paseo de la Reforma 789', '456123789', 'proveedor3@example.com'),
('Proveedor 4', 'Gran Vía 101', '321654987', 'proveedor4@example.com'),
('Proveedor 5', 'Av. Libertador 202', '654987321', 'proveedor5@example.com');

-- Tabla para Cliente
CREATE TABLE Clientes (
    idCliente INT PRIMARY KEY IDENTITY(1,1),
    nombreCliente VARCHAR(50),
    apellidoCliente VARCHAR(50),
    dniCliente VARCHAR(20),
    direccionCliente VARCHAR(100),
    telefonoCliente VARCHAR(15),
    emailCliente VARCHAR(50)
);

-- Insertar datos en la tabla Clientes
INSERT INTO Clientes(nombreCliente, apellidoCliente, dniCliente, direccionCliente, telefonoCliente, emailCliente)
VALUES 
('Juan', 'Pérez', '12345678', 'Calle 1', '111111111', 'juan.perez@example.com'),
('María', 'García', '23456789', 'Calle 2', '222222222', 'maria.garcia@example.com'),
('Carlos', 'Sánchez', '34567890', 'Calle 3', '333333333', 'carlos.sanchez@example.com'),
('Lucía', 'Rodríguez', '45678901', 'Calle 4', '444444444', 'lucia.rodriguez@example.com'),
('Pedro', 'Fernández', '56789012', 'Calle 5', '555555555', 'pedro.fernandez@example.com');

-- Tabla para Venta
CREATE TABLE Ventas (
    idVenta INT PRIMARY KEY IDENTITY(1,1),
    fechaVenta DATE,
    montoVenta DECIMAL(10, 2),
    idCliente INT FOREIGN KEY REFERENCES Clientes(idCliente)
);

-- Insertar datos en la tabla Ventas
INSERT INTO Ventas(fechaVenta, montoVenta, idCliente)
VALUES 
('2023-08-01', 1500.00, 1),
('2023-08-02', 300.00, 2),
('2023-08-03', 100.00, 3),
('2023-08-04', 2400.00, 4),
('2023-08-05', 1200.00, 5);

-- Tabla para Compra
CREATE TABLE Compras (
    idCompra INT PRIMARY KEY IDENTITY(1,1),
    idProveedor INT FOREIGN KEY REFERENCES Proveedores(idProveedor),
    fechaCompra DATE,
    totalCompra DECIMAL(10, 2)
);

-- Insertar datos en la tabla Compras
INSERT INTO Compras(idProveedor, fechaCompra, totalCompra)
VALUES 
(1, '2023-07-15', 1000.00),
(2, '2023-07-16', 2000.00),
(3, '2023-07-17', 1500.00),
(4, '2023-07-18', 1800.00),
(5, '2023-07-19', 1200.00);

-- Tabla para Factura
CREATE TABLE Facturas (
    idFactura INT PRIMARY KEY IDENTITY(1,1),
    idVenta INT FOREIGN KEY REFERENCES Ventas(idVenta),
    idCliente INT FOREIGN KEY REFERENCES Clientes(idCliente),
    fechaFactura DATE,
    totalFactura DECIMAL(10, 2),
    subTotalFactura DECIMAL(10, 2),
    ivaFactura DECIMAL(10, 2),
    descuentoFactura DECIMAL(10, 2)
);

-- Insertar datos en la tabla Facturas
INSERT INTO Facturas(idVenta, idCliente, fechaFactura, totalFactura, subTotalFactura, ivaFactura, descuentoFactura)
VALUES 
(1, 1, '2023-08-01', 1500.00, 1300.00, 150.00, 50.00),
(2, 2, '2023-08-02', 300.00, 260.00, 30.00, 10.00),
(3, 3, '2023-08-03', 100.00, 85.00, 10.00, 5.00),
(4, 4, '2023-08-04', 2400.00, 2100.00, 240.00, 60.00),
(5, 5, '2023-08-05', 1200.00, 1050.00, 120.00, 30.00);

-- Tabla para Perfil
CREATE TABLE Perfiles (
    idPerfil INT PRIMARY KEY IDENTITY(1,1),
    nombrePerfil VARCHAR(50),
    descripcionPerfil TEXT,
    estadoPerfil VARCHAR(20)
);

-- Insertar datos en la tabla Perfiles
INSERT INTO Perfiles(nombrePerfil, descripcionPerfil, estadoPerfil)
VALUES 
('Administrador', 'Perfil con acceso total al sistema', 'Activo'),
('Vendedor', 'Perfil con acceso limitado a ventas', 'Activo'),
('Gerente', 'Perfil con acceso a reportes y gestión', 'Activo'),
('Soporte', 'Perfil con acceso a soporte técnico', 'Inactivo'),
('Cliente', 'Perfil con acceso a la tienda en línea', 'Activo');

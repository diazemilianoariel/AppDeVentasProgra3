--tabla llamada "Productos" con los siguientes campos:
-- idProducto (int, primary key, autoincremental)
-- nombreProducto (varchar(50))
-- precioProducto (float)
-- cantidadProducto (int)
-- fechaIngreso (date)
-- descripcionProducto (text)

CREATE table Productos(
    idProducto int primary key IDENTITY(1,1),
    nombreProducto varchar(50),
    precioProducto float,
    cantidadProducto int,
    fechaIngreso date,
    descripcionProducto text
)
-- cargar la tabla productos con 10 registros

INSERT INTO Productos(nombreProducto, precioProducto, cantidadProducto, fechaIngreso, descripcionProducto)
VALUES
('Producto 1', 100, 10, '2021-01-01', 'Descripcion 1'),
('Producto 2', 200, 20, '2021-01-02', 'Descripcion 2'),
('Producto 3', 300, 30, '2021-01-03', 'Descripcion 3'),
('Producto 4', 400, 40, '2021-01-04', 'Descripcion 4'),
('Producto 5', 500, 50, '2021-01-05', 'Descripcion 5'),
('Producto 6', 600, 60, '2021-01-06', 'Descripcion 6'),
('Producto 7', 700, 70, '2021-01-07', 'Descripcion 7'),
('Producto 8', 800, 80, '2021-01-08', 'Descripcion 8'),
('Producto 9', 900, 90, '2021-01-09', 'Descripcion 9'),
('Producto 10', 1000, 100, '2021-01-10', 'Descripcion 10')
SELECT * FROM Productos;

--tabla llamada "Clientes" con los siguientes campos:
-- idCliente (int, primary key, autoincremental)
-- nombreCliente (varchar(50))
-- apellidoCliente (varchar(50))
-- telefonoCliente (varchar(15))
-- direccionCliente (varchar(100))
-- emailCliente (varchar(50))


CREATE table Clientes(
    idCliente int primary key IDENTITY(1,1),
    nombreCliente varchar(50),
    apellidoCliente varchar(50),
    telefonoCliente varchar(15),
    direccionCliente varchar(100),
    emailCliente varchar(50)
)

-- cargar la tabla clientes con 5 registros

INSERT INTO Clientes(nombreCliente, apellidoCliente, telefonoCliente, direccionCliente, emailCliente)
VALUES
('Cliente 1', 'Apellido 1', '111111111', 'Direccion 1','email 1'),
('Cliente 2', 'Apellido 2', '222222222', 'Direccion 2','email 2'),
('Cliente 3', 'Apellido 3', '333333333', 'Direccion 3','email 3'),
('Cliente 4', 'Apellido 4', '444444444', 'Direccion 4','email 4'),
('Cliente 5', 'Apellido 5', '555555555', 'Direccion 5','email 5')

SELECT * FROM Clientes;

--tabla llamada "Ventas" con los siguientes campos:
-- idVenta (int, primary key, autoincremental)
-- idProducto (int, foreign key)
-- cantidadProducto (int)
-- idCliente (int, foreign key)
-- fechaVenta (date)
-- totalVenta (float)
-- subtotal (float)

CREATE table Ventas(
    idVenta int primary key IDENTITY(1,1),
    idProducto int foreign key references Productos(idProducto),
    cantidadProducto int,
    idCliente int foreign key references Clientes(idCliente),
    fechaVenta date,
    totalVenta float,
    subtotal float
)

-- cargar la tabla ventas con 5 registros

INSERT INTO Ventas(idProducto, cantidadProducto, idCliente, fechaVenta, totalVenta, subtotal)
VALUES
(1, 10, 1, '2021-01-01', 1000, 1000),
(2, 20, 2, '2021-01-02', 4000, 4000),
(3, 30, 3, '2021-01-03', 9000, 9000),
(4, 40, 4, '2021-01-04', 16000, 16000),
(5, 50, 5, '2021-01-05', 25000, 25000)

SELECT * FROM Ventas;

--tabla llamada "Usuarios" con los siguientes campos:
-- idUsuario (int, primary key, autoincremental)
-- nombreUsuario (varchar(50))
-- apellidoUsuario (varchar(50))
-- telefonoUsuario (varchar(15))
-- direccionUsuario (varchar(100))
-- emailUsuario (varchar(50))
-- passwordUsuario (varchar(50))

CREATE table Usuarios(
    idUsuario int primary key IDENTITY(1,1),
    nombreUsuario varchar(50),
    apellidoUsuario varchar(50),
    telefonoUsuario varchar(15),
    direccionUsuario varchar(100),
    emailUsuario varchar(50),
    passwordUsuario varchar(50)
)

-- cargar la tabla usuarios con 5 registros

INSERT INTO Usuarios(nombreUsuario, apellidoUsuario, telefonoUsuario, direccionUsuario, emailUsuario, passwordUsuario)
VALUES
('Usuario 1', 'Apellido 1', '111111111', 'Direccion 1','email 1', 'password 1'),
('Usuario 2', 'Apellido 2', '222222222', 'Direccion 2','email 2', 'password 2'),
('Usuario 3', 'Apellido 3', '333333333', 'Direccion 3','email 3', 'password 3'),
('Usuario 4', 'Apellido 4', '444444444', 'Direccion 4','email 4', 'password 4'),
('Usuario 5', 'Apellido 5', '555555555', 'Direccion 5','email 5', 'password 5')

SELECT * FROM Usuarios;




-- crear tabla de proveedores con los siguientes campos:
-- idProveedor (int, primary key, autoincremental)
-- nombreProveedor (varchar(50))
-- telefonoProveedor (varchar(15))
-- direccionProveedor (varchar(100))
-- emailProveedor (varchar(50))

CREATE table Proveedores(
    idProveedor int primary key IDENTITY(1,1),
    nombreProveedor varchar(50),
    telefonoProveedor varchar(15),
    direccionProveedor varchar(100),
    emailProveedor varchar(50)
)

-- cargar la tabla proveedores con 5 registros

INSERT INTO Proveedores(nombreProveedor, telefonoProveedor, direccionProveedor, emailProveedor)
VALUES
('Proveedor 1', '111111111', 'Direccion 1','email 1'),
('Proveedor 2', '222222222', 'Direccion 2','email 2'),
('Proveedor 3', '333333333', 'Direccion 3','email 3'),
('Proveedor 4', '444444444', 'Direccion 4','email 4'),
('Proveedor 5', '555555555', 'Direccion 5','email 5')

SELECT * FROM Proveedores;

--  crear tabla de compras con los siguientes campos:
-- idCompra (int, primary key, autoincremental)
-- idProducto (int, foreign key)
-- cantidadProducto (int)
-- idProveedor (int, foreign key)
-- fechaCompra (date)
-- totalCompra (float)
-- subtotal (float)

CREATE table Compras(
    idCompra int primary key IDENTITY(1,1),
    idProducto int foreign key references Productos(idProducto),
    cantidadProducto int,
    idProveedor int foreign key references Proveedores(idProveedor),
    fechaCompra date,
    totalCompra float,
    subtotal float
)

-- cargar la tabla compras con 5 registros

INSERT INTO Compras(idProducto, cantidadProducto, idProveedor, fechaCompra, totalCompra, subtotal)
VALUES
(1, 10, 1, '2021-01-01', 1000, 1000),
(2, 20, 2, '2021-01-02', 4000, 4000),
(3, 30, 3, '2021-01-03', 9000, 9000),
(4, 40, 4, '2021-01-04', 16000, 16000),
(5, 50, 5, '2021-01-05', 25000, 25000)

SELECT * FROM Compras;



-- crear tabla de facturas con los siguientes campos:
-- idFactura (int, primary key, autoincremental)
-- idVenta (int, foreign key)
-- idCliente (int, foreign key)
-- fechaFactura (date)
-- totalFactura (float)
-- subtotal (float)
-- totalIVA (float)
-- totalDescuento (float)

CREATE table Facturas(
    idFactura int primary key IDENTITY(1,1),
    idVenta int foreign key references Ventas(idVenta),
    idCliente int foreign key references Clientes(idCliente),
    fechaFactura date,
    totalFactura float,
    subtotal float,
    totalIVA float,
    totalDescuento float
)

-- cargar la tabla facturas con 5 registros

INSERT INTO Facturas(idVenta, idCliente, fechaFactura, totalFactura, subtotal, totalIVA, totalDescuento)
VALUES
(1, 1, '2021-01-01', 1000, 1000, 160, 40),
(2, 2, '2021-01-02', 4000, 4000, 640, 160),
(3, 3, '2021-01-03', 9000, 9000, 1440, 360),
(4, 4, '2021-01-04', 16000, 16000, 2560, 640),
(5, 5, '2021-01-05', 25000, 25000, 4000, 1000)

SELECT * FROM Facturas;


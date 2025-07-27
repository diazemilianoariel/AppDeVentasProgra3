-- Usamos la base de datos 'master' para poder crear la nueva
USE master;
GO

-- Si la base de datos ya existe, la borramos para empezar de cero
IF DB_ID('TiendaOnline') IS NOT NULL
BEGIN
    ALTER DATABASE TiendaOnline SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE TiendaOnline;
END
GO

-- 1. CREAR LA BASE DE DATOS
CREATE DATABASE TiendaOnline;
GO

USE TiendaOnline;
GO

-- 2. CREACIÓN DE TABLAS
CREATE TABLE dbo.Categorias(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	nombre nvarchar(50) NULL,
	estado bit NOT NULL DEFAULT 1
);
GO
CREATE TABLE dbo.Marcas(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	nombre nvarchar(50) NULL,
	estado bit NOT NULL DEFAULT 1
);
GO
CREATE TABLE dbo.Tipos(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	nombre nvarchar(50) NULL,
	estado bit NOT NULL DEFAULT 1
);
GO
CREATE TABLE dbo.Productos(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	nombre nvarchar(50) NULL,
	descripcion nvarchar(100) NULL,
	precio decimal(10, 2) NULL,
	imagen nvarchar(255) NULL,
	idMarca int NULL,
	idTipo int NULL,
	idCategoria int NULL,
	margenGanancia decimal(10, 2) NOT NULL,
	estado bit NOT NULL DEFAULT 1
);
GO
CREATE TABLE dbo.Proveedores(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	nombre nvarchar(50) NULL,
	direccion nvarchar(100) NULL,
	telefono nvarchar(15) NULL,
	email nvarchar(50) NULL,
	estado bit NOT NULL DEFAULT 1
);
GO
CREATE TABLE dbo.Proveedores_Productos(
	idProveedor int NOT NULL,
	idProducto int NOT NULL,
	PRIMARY KEY (idProveedor, idProducto)
);
GO
CREATE TABLE dbo.Perfiles(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	nombre nvarchar(50) NULL,
	estado bit NOT NULL DEFAULT 1
);
GO
CREATE TABLE dbo.Usuarios(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	nombre nvarchar(50) NULL,
	apellido nvarchar(50) NULL,
	dni nvarchar(20) NULL,
	direccion nvarchar(100) NULL,
	telefono nvarchar(15) NULL,
	email nvarchar(50) NULL,
	clave nvarchar(255) NULL,
	idPerfil int NOT NULL DEFAULT 1,
	estado bit NOT NULL DEFAULT 1
);
GO
CREATE TABLE dbo.EstadoVenta(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	nombre nvarchar(50) NULL
);
GO
CREATE TABLE dbo.Ventas(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	fecha date NULL,
	monto decimal(10, 2) NULL,
	idUsuario int NULL,
	EnLocal bit NOT NULL DEFAULT 0,
	idEstadoVenta int NOT NULL DEFAULT 1
);
GO
CREATE TABLE dbo.DetalleVentas(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	idVenta int NULL,
	idProducto int NULL,
	cantidad int NULL,
	precioVenta decimal(10, 2) NULL,
	estado bit NOT NULL DEFAULT 0
);
GO
CREATE TABLE dbo.Stock(
	idProducto int NOT NULL PRIMARY KEY,
	cantidad int NULL,
	stockMinimo int NULL,
	fechaActualizacion date NULL
);
GO
CREATE TABLE dbo.Ofertas(
	idProducto int NOT NULL PRIMARY KEY,
	precioOferta decimal(10, 2) NULL,
	FechaInicio date NULL,
	FechaFin date NULL
);
GO

-- 3. DEFINICIÓN DE FOREIGN KEYS
ALTER TABLE dbo.Productos ADD CONSTRAINT FK_Productos_Categorias FOREIGN KEY(idCategoria) REFERENCES dbo.Categorias (id);
ALTER TABLE dbo.Productos ADD CONSTRAINT FK_Productos_Marcas FOREIGN KEY(idMarca) REFERENCES dbo.Marcas (id);
ALTER TABLE dbo.Productos ADD CONSTRAINT FK_Productos_Tipos FOREIGN KEY(idTipo) REFERENCES dbo.Tipos (id);
ALTER TABLE dbo.Proveedores_Productos ADD CONSTRAINT FK_Proveedores_Productos_Proveedores FOREIGN KEY(idProveedor) REFERENCES dbo.Proveedores (id);
ALTER TABLE dbo.Proveedores_Productos ADD CONSTRAINT FK_Proveedores_Productos_Productos FOREIGN KEY(idProducto) REFERENCES dbo.Productos (id);
ALTER TABLE dbo.Usuarios ADD CONSTRAINT FK_Usuarios_Perfiles FOREIGN KEY(idPerfil) REFERENCES dbo.Perfiles (id);
ALTER TABLE dbo.Ventas ADD CONSTRAINT FK_Ventas_EstadoVenta FOREIGN KEY(idEstadoVenta) REFERENCES dbo.EstadoVenta (id);
ALTER TABLE dbo.Ventas ADD CONSTRAINT FK_Ventas_Usuarios FOREIGN KEY(idUsuario) REFERENCES dbo.Usuarios (id);
ALTER TABLE dbo.DetalleVentas ADD CONSTRAINT FK_DetalleVentas_Productos FOREIGN KEY(idProducto) REFERENCES dbo.Productos (id);
ALTER TABLE dbo.DetalleVentas ADD CONSTRAINT FK_DetalleVentas_Ventas FOREIGN KEY(idVenta) REFERENCES dbo.Ventas (id);
ALTER TABLE dbo.Stock ADD CONSTRAINT FK_Stock_Productos FOREIGN KEY(idProducto) REFERENCES dbo.Productos (id);
ALTER TABLE dbo.Ofertas ADD CONSTRAINT FK_Ofertas_Productos FOREIGN KEY(idProducto) REFERENCES dbo.Productos (id) ON DELETE CASCADE;
GO

-- 4. INSERCIÓN DE DATOS DE EJEMPLO
SET IDENTITY_INSERT dbo.Categorias ON;
INSERT INTO dbo.Categorias (id, nombre, estado) VALUES (4, N'camioneta', 1), (5, N'higiene', 1), (6, N'accesorios', 1), (7, N'juguetes', 1), (10, N'alimentacion', 1), (13, N'auto', 1), (18, N'Bazar', 1);
SET IDENTITY_INSERT dbo.Categorias OFF;
GO
SET IDENTITY_INSERT dbo.Marcas ON;
INSERT INTO dbo.Marcas (id, nombre, estado) VALUES (1, N'Disney', 1), (2, N'Hasbro', 1), (3, N'Infanti', 1), (4, N'CHICCO', 1), (5, N'AVENT PHILIPS', 1), (14, N'Wrangler', 1);
SET IDENTITY_INSERT dbo.Marcas OFF;
GO
SET IDENTITY_INSERT dbo.Tipos ON;
INSERT INTO dbo.Tipos (id, nombre, estado) VALUES (5, N'Productos de higiene', 1), (6, N'Productos de maternidad', 1), (7, N'Productos para el hogar', 1), (8, N'Productos para el bebé', 1), (9, N'Bazar', 1);
SET IDENTITY_INSERT dbo.Tipos OFF;
GO
SET IDENTITY_INSERT dbo.Perfiles ON;
INSERT INTO dbo.Perfiles (id, nombre, estado) VALUES (1, N'Cliente', 1), (2, N'Administrador', 1), (3, N'Vendedor', 1);
SET IDENTITY_INSERT dbo.Perfiles OFF;
GO
SET IDENTITY_INSERT dbo.Proveedores ON;
INSERT INTO dbo.Proveedores (id, nombre, direccion, telefono, email, estado) VALUES (1, N'MAYORISTA NINI', N'PARACAS 13', N'123456789', N'proveedor1@gmail.com', 1), (3, N'Proveedor 3', N'calle nueva 999', N'5555', N'proveedor3', 1), (4, N'Potigian', N'Av Lavalle 500', N'081066322', N'Potigian@Potigian', 1), (8, N'Clandestine22', N'Av La Plata 1223', N'000022222', N'Clandestine@gmail.com', 1);
SET IDENTITY_INSERT dbo.Proveedores OFF;
GO
SET IDENTITY_INSERT dbo.Productos ON;
INSERT INTO dbo.Productos (id, nombre, descripcion, precio, imagen, idMarca, idTipo, idCategoria, margenGanancia, estado) VALUES (14, N'Pañales Huggies', N'Pañales Huggies Supreme Care Xxg X 50 Unidades', 39024.00, N'https://http2.mlstatic.com/D_NQ_NP_892774-MLU71242516086_082023-O.webp', 5, 5, 5, 60.00, 1);
INSERT INTO dbo.Productos (id, nombre, descripcion, precio, imagen, idMarca, idTipo, idCategoria, margenGanancia, estado) VALUES (19, N'Cinta De Embalar', N'Transparente 48x100 Autoadhesiva Pack X36u', 36352.00, N'https://http2.mlstatic.com/D_NQ_NP_712516-MLA70239447799_062023-O.webp', 4, 5, 6, 50.00, 1);
INSERT INTO dbo.Productos (id, nombre, descripcion, precio, imagen, idMarca, idTipo, idCategoria, margenGanancia, estado) VALUES (20, N'Mamadera Nuk', N'Nature 260ml 100% Material Sostenible Color Natural', 20000.00, N'https://http2.mlstatic.com/D_NQ_NP_964549-MLU70983836885_082023-O.webp', 3, 8, 10, 10.00, 1);
INSERT INTO dbo.Productos (id, nombre, descripcion, precio, imagen, idMarca, idTipo, idCategoria, margenGanancia, estado) VALUES (27, N'Caña de pesca', N'La mejor caña de pesca', 100.00, N'https://http2.mlstatic.com/D_NQ_NP_693368-MLA81627116476_012025-O.webp', 2, 9, 6, 10.00, 1);
INSERT INTO dbo.Productos (id, nombre, descripcion, precio, imagen, idMarca, idTipo, idCategoria, margenGanancia, estado) VALUES (30, N'Cochecito de paseo', N'Mega Baby 3 en 1 Lanin Travel System negro con chasis color dorado', 100000.00, N'https://http2.mlstatic.com/D_NQ_NP_657394-MLA87099090156_072025-O.webp', 14, 8, 6, 50.00, 1);
SET IDENTITY_INSERT dbo.Productos OFF;
GO
INSERT INTO dbo.Proveedores_Productos (idProveedor, idProducto) VALUES (3, 14), (4, 19), (4, 20), (4, 27), (3, 30);
GO
INSERT INTO dbo.Stock (idProducto, cantidad, stockMinimo, fechaActualizacion) VALUES (14, 83, 5, '2025-07-26'), (19, 3, 5, '2025-03-17'), (20, 46, 5, '2025-03-23'), (27, 43, 5, '2025-04-13'), (30, 19, 5, '2025-07-26');
GO
SET IDENTITY_INSERT dbo.Usuarios ON;
INSERT INTO dbo.Usuarios (id, nombre, apellido, dni, email, clave, idPerfil, estado) VALUES (1, N'Roberto', N'Flores', N'6666666', N'cliente@mail.com', N'222', 1, 1), (2, N'administrador', N'Admin', N'12345678', N'admin@mail.com', N'111', 2, 1);
SET IDENTITY_INSERT dbo.Usuarios OFF;
GO
SET IDENTITY_INSERT dbo.EstadoVenta ON;
INSERT INTO dbo.EstadoVenta (id, nombre) VALUES (1, N'Pendiente'), (2, N'Aprobado'), (3, N'Cancelado');
SET IDENTITY_INSERT dbo.EstadoVenta OFF;
GO
INSERT INTO dbo.Ofertas (idProducto, precioOferta, FechaInicio, FechaFin) VALUES (14, 59000.00, '2025-07-27', '2025-08-15');
INSERT INTO dbo.Ofertas (idProducto, precioOferta, FechaInicio, FechaFin) VALUES (20, NULL, NULL, NULL);
GO
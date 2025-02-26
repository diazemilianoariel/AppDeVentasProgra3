﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using dominio;
using negocio;

namespace negocio
{

    public class ProductoNegocio
    {


        public int CantidadProductos()
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearConsulta("SELECT COUNT(*) FROM Productos");
                accesoDatos.EjecutarLectura();
                if (accesoDatos.Lector.Read())
                {
                    int cantidadUsuarios = accesoDatos.Lector.GetInt32(0);


                    return cantidadUsuarios; // lee lo que hay en la unica columna que hay y lo convierte y lo retorna como entero
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }


        public List<Producto> ListarProductos()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                // Usar parámetros para evitar inyecciones SQL
                accesoDatos.SetearConsulta("SELECT P.id, P.nombre, P.descripcion, P.imagen, P.precio, P.margenGanancia, S.cantidad, M.nombre AS MarcaNombre, T.nombre AS TipoNombre, C.nombre AS CategoriaNombre, P.estado, Prov.nombre AS ProveedorNombre " +
            "FROM Productos P " +
            "INNER JOIN Stock S ON P.id = S.idProducto " +
            "INNER JOIN Marcas M ON P.idMarca = M.id " +
            "INNER JOIN Tipos T ON T.id = P.idTipo " +
            "INNER JOIN Categorias C ON C.id = P.idCategoria " +
            "INNER JOIN Proveedores_Productos PP ON P.id = PP.idProducto " +
            "INNER JOIN Proveedores Prov ON PP.idProveedor = Prov.id");



                // Ejecutar la consulta
                accesoDatos.EjecutarLectura();

                // Leer los datos obtenidos
                while (accesoDatos.Lector.Read())
                {
                    Producto producto = new Producto();

                    producto.id = accesoDatos.Lector.GetInt32(0);
                    producto.nombre = accesoDatos.Lector.GetString(1);
                    producto.descripcion = accesoDatos.Lector.GetString(2);
                    producto.Imagen = accesoDatos.Lector.GetString(3);
                    producto.precio = accesoDatos.Lector.GetDecimal(4);
                    producto.margenGanancia = accesoDatos.Lector.GetDecimal(5);
                    producto.stock = accesoDatos.Lector.GetInt32(6);
                    producto.marca = accesoDatos.Lector.GetString(7);
                    producto.tipo = accesoDatos.Lector.GetString(8);
                    producto.categoria = accesoDatos.Lector.GetString(9);
                    producto.estado = accesoDatos.Lector.GetBoolean(10);
                    producto.proveedor = accesoDatos.Lector.GetString(11);




                    lista.Add(producto);





                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }

        public Producto ObtenerProducto(int id)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            Producto producto = new Producto();
            try
            {
                // Usar parámetros para evitar inyecciones SQL
                accesoDatos.SetearConsulta("SELECT P.id, P.nombre, P.descripcion, P.imagen, P.precio, P.margenGanancia, S.cantidad, M.nombre AS Marca, T.nombre AS Tipo, C.nombre AS Categoria, Pr.nombre AS Proveedor, P.estado " +
                                   "FROM Productos P " +
                                   "INNER JOIN Stock S ON P.id = S.idProducto " +
                                   "INNER JOIN Marcas M ON P.idMarca = M.id " +
                                   "INNER JOIN Tipos T ON P.idTipo = T.id " +
                                   "INNER JOIN Categorias C ON P.idCategoria = C.id " +
                                   "INNER JOIN Proveedores_Productos PP ON P.id = PP.idProducto " +
                                   "INNER JOIN Proveedores Pr ON PP.idProveedor = Pr.id " +
                                   "WHERE P.id = @id");




                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@id", id);

                // Ejecutar la consulta
                accesoDatos.EjecutarLectura();

                // Leer los datos obtenidos
                if (accesoDatos.Lector.Read())
                {
                    producto.id = accesoDatos.Lector.GetInt32(0);
                    producto.nombre = accesoDatos.Lector.GetString(1);
                    producto.descripcion = accesoDatos.Lector.GetString(2);
                    producto.Imagen = accesoDatos.Lector.GetString(3);
                    producto.precio = accesoDatos.Lector.GetDecimal(4);
                    producto.margenGanancia = accesoDatos.Lector.GetDecimal(5);
                    producto.stock = accesoDatos.Lector.GetInt32(6);
                    producto.marca = accesoDatos.Lector.GetString(7);
                    producto.tipo = accesoDatos.Lector.GetString(8);
                    producto.categoria = accesoDatos.Lector.GetString(9);
                    producto.proveedor = accesoDatos.Lector.GetString(10);
                    producto.estado = accesoDatos.Lector.GetBoolean(11);


                }

                return producto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }


        public void AgregarProducto(Producto producto)
        {
            AccesoDatos accesodatos = new AccesoDatos();
            int idProducto;



            try
            {
                // Verificar si el producto ya existe
                accesodatos.SetearConsulta("SELECT COUNT(*) " +
                                           "FROM Productos " +
                                           "WHERE nombre = @nombree AND descripcion = @descripcionn");
                accesodatos.SetearParametro("@nombree", producto.nombre);
                accesodatos.SetearParametro("@descripcionn", producto.descripcion);

                int count = Convert.ToInt32(accesodatos.EjecutarEscalar());

                if (count > 0)
                {
                    throw new Exception("El producto ya existe");
                }
            }
            catch (Exception ex)
            {
                throw ex;


            }
            finally
            {
                accesodatos.CerrarConexion();
            }





            try
            {
                // Insertar en la tabla Productos
                accesodatos.SetearConsulta("INSERT INTO Productos (nombre, descripcion, imagen, precio, margenGanancia, estado, idMarca, idTipo, idCategoria) " +
                                           "VALUES (@nombre, @descripcion, @imagen, @precio, @margenGanancia, @estado, @idMarca, @idTipo, @idCategoria); " +
                                           "SELECT SCOPE_IDENTITY();");

                accesodatos.SetearParametro("@nombre", producto.nombre);
                accesodatos.SetearParametro("@descripcion", producto.descripcion);
                accesodatos.SetearParametro("@imagen", producto.Imagen);
                accesodatos.SetearParametro("@precio", producto.precio);
                accesodatos.SetearParametro("@margenGanancia", producto.margenGanancia);
                accesodatos.SetearParametro("@estado", producto.estado);
                accesodatos.SetearParametro("@idMarca", producto.marca);
                accesodatos.SetearParametro("@idTipo", producto.tipo);
                accesodatos.SetearParametro("@idCategoria", producto.categoria);

                // Obtener el ID del producto insertado
                idProducto = Convert.ToInt32(accesodatos.EjecutarEscalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesodatos.CerrarConexion();
            }




            try
            {
                // Insertar en la tabla Stock
                accesodatos = new AccesoDatos();
                accesodatos.SetearConsulta("INSERT INTO Stock (idProducto, cantidad, stockMinimo, fechaActualizacion) " +
                                           "VALUES (@idProducto, @cantidad, @stockMinimo, @fechaActualizacion)");

                accesodatos.SetearParametro("@idProducto", idProducto);
                accesodatos.SetearParametro("@cantidad", producto.stock);
                accesodatos.SetearParametro("@stockMinimo", 5);
                accesodatos.SetearParametro("@fechaActualizacion", DateTime.Now);

                accesodatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesodatos.CerrarConexion();
            }

            try
            {
                // Insertar en la tabla Proveedores_Productos
                accesodatos = new AccesoDatos();
                accesodatos.SetearConsulta("INSERT INTO Proveedores_Productos (idProveedor, idProducto) " +
                                           "VALUES (@idProveedor, @idProducto)");

                accesodatos.SetearParametro("@idProveedor", producto.proveedor);
                accesodatos.SetearParametro("@idProducto", idProducto);

                accesodatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesodatos.CerrarConexion();
            }



        }




        public void ModificarProducto(Producto producto)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                // Usar parámetros para evitar inyecciones SQL
                accesoDatos.SetearConsulta("UPDATE Productos SET nombre = @nombre, descripcion = @descripcion, imagen = @imagen, precio = @precio,  estado = @estado WHERE id = @idProducto");

                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@nombre", producto.nombre);
                accesoDatos.SetearParametro("@descripcion", producto.descripcion);
                accesoDatos.SetearParametro("@imagen", producto.Imagen);
                accesoDatos.SetearParametro("@precio", producto.precio);
                accesoDatos.SetearParametro("@idMarca", producto.marca);
                accesoDatos.SetearParametro("@idTipo", producto.tipo);
                accesoDatos.SetearParametro("@idCategoria", producto.categoria);
                accesoDatos.SetearParametro("@estado", producto.estado);
                accesoDatos.SetearParametro("@idProducto", producto.id);

                // Ejecutar la acción de modificación
                accesoDatos.EjecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }



            // modificamos la cantidad de la tabla stock
            try
            {
                accesoDatos.SetearConsulta("UPDATE Stock SET cantidad = @cantidad WHERE idProducto = @idProductoCantidad");

                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@cantidad", producto.stock);
                accesoDatos.SetearParametro("@idProductoCantidad", producto.id);

                // Ejecutar la acción de modificación
                accesoDatos.EjecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }

            // modificamos el nombre de la marca en su tabla Marcas
            try
            {
                accesoDatos.SetearConsulta("UPDATE Marcas SET nombre = @nombreMarca WHERE id = @idMarca");

                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@nombreMarca", producto.marca);
                accesoDatos.SetearParametro("@idMarca", producto.id);

                // Ejecutar la acción de modificación
                accesoDatos.EjecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }


            // modificamos el nombre de Tipo en su tabla Tipos
            try
            {
                accesoDatos.SetearConsulta("UPDATE Tipos SET nombre = @nombreTipo WHERE id = @idTipo");

                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@nombreTipo", producto.tipo);
                accesoDatos.SetearParametro("@idTipo", producto.id);

                // Ejecutar la acción de modificación
                accesoDatos.EjecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }



            // modificamos el nombre de la categoria en su tabla Categorias
            try
            {
                accesoDatos.SetearConsulta("UPDATE Categorias SET nombre = @nombreCategoria WHERE id = @idCategoria");

                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@nombreCategoria", producto.categoria);
                accesoDatos.SetearParametro("@idCategoria", producto.id);

                // Ejecutar la acción de modificación
                accesoDatos.EjecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }


            // modificamos el nombre del proveedor en su tabla Proveedores
            try
            {
                accesoDatos.SetearConsulta("UPDATE Proveedores SET nombre = @nombreProveedor WHERE id = @idProveedor");

                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@nombreProveedor", producto.proveedor);
                accesoDatos.SetearParametro("@idProveedor", producto.id);

                // Ejecutar la acción de modificación
                accesoDatos.EjecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }




        }


        public void EliminarProducto(int id)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                // Usar parámetros para evitar inyecciones SQL
                accesoDatos.SetearConsulta("DELETE FROM Productos WHERE id = @id");

                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@id", id);

                // Ejecutar la acción de eliminación
                accesoDatos.EjecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }


        public List<Producto> BuscarProductos(string busqueda)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearConsulta("SELECT P.id, P.nombre, P.descripcion, P.imagen, P.precio, P.margenGanancia, S.cantidad, M.nombre AS MarcaNombre, T.nombre AS TipoNombre, C.nombre AS CategoriaNombre, P.estado, Prov.nombre AS ProveedorNombre " +
                                           "FROM Productos P " +
                                           "INNER JOIN Stock S ON P.id = S.idProducto " +
                                           "INNER JOIN Marcas M ON P.idMarca = M.id " +
                                           "INNER JOIN Tipos T ON T.id = P.idTipo " +
                                           "INNER JOIN Categorias C ON C.id = P.idCategoria " +
                                           "INNER JOIN Proveedores_Productos PP ON P.id = PP.idProducto " +
                                           "INNER JOIN Proveedores Prov ON PP.idProveedor = Prov.id " +
                                           "WHERE P.nombre LIKE @busqueda OR P.descripcion LIKE @busqueda");

                accesoDatos.SetearParametro("@busqueda", "%" + busqueda + "%");

                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Producto producto = new Producto();
                    producto.id = accesoDatos.Lector.GetInt32(0);
                    producto.nombre = accesoDatos.Lector.GetString(1);
                    producto.descripcion = accesoDatos.Lector.GetString(2);
                    producto.Imagen = accesoDatos.Lector.GetString(3);
                    producto.precio = accesoDatos.Lector.GetDecimal(4);
                    producto.margenGanancia = accesoDatos.Lector.GetDecimal(5);
                    producto.stock = accesoDatos.Lector.GetInt32(6);
                    producto.marca = accesoDatos.Lector.GetString(7);
                    producto.tipo = accesoDatos.Lector.GetString(8);
                    producto.categoria = accesoDatos.Lector.GetString(9);
                    producto.estado = accesoDatos.Lector.GetBoolean(10);
                    producto.proveedor = accesoDatos.Lector.GetString(11);

                    lista.Add(producto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }

            return lista;
        }



        // este metodo lo usa cuando el admin rechaza la compra del usuario y tiene que volver a insertar el stock que se descontó
        public void VolverAgregarStock(int idProducto, int cantidad)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearConsulta("UPDATE Stock SET cantidad = cantidad + @cantidad WHERE idProducto = @idProducto");
                accesoDatos.SetearParametro("@cantidad", cantidad);
                accesoDatos.SetearParametro("@idProducto", idProducto);
                accesoDatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }




    }

}
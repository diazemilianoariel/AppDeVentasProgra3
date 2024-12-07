using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dominio;
using negocio;

namespace negocio
{

    public class ProductoNegocio
    {
        public List<Producto> ListarProductos()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                // Usar parámetros para evitar inyecciones SQL
                accesoDatos.SetearConsulta("SELECT P.id, P.nombre, P.descripcion, P.imagen, P.precio, S.cantidad, M.nombre FROM Productos P " +
                    "INNER JOIN Stock S ON P.id = S.idProducto " +
                    "INNER JOIN Marcas M ON P.idMarca = M.id");

                // Ejecutar la consulta
                accesoDatos.EjecutarLectura();

                // Leer los datos obtenidos
                while (accesoDatos.Lector.Read())
                {
                    Producto producto = new Producto();
                    producto.id = accesoDatos.Lector.GetInt32(0);
                    producto.nombre = accesoDatos.Lector.GetString(1);
                    producto.descripcion = accesoDatos.Lector.GetString(2);
                    producto.imagen = accesoDatos.Lector.GetString(3);
                    producto.precio = accesoDatos.Lector.GetDecimal(4);
                    producto.stock = accesoDatos.Lector.GetInt32(5);
                    producto.marca = accesoDatos.Lector.GetString(6);


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
                accesoDatos.SetearConsulta("SELECT P.id, P.nombre, P.descripcion, P.imagen, P.precio, S.cantidad, M.nombre, T.nombre, C.nombre, Pr.nombre, P.estado FROM Productos P " +
                                         "INNER JOIN Stock S ON P.id = S.idProducto " +
                                         "INNER JOIN Marcas M ON P.idMarca = M.id " +
                                         "INNER JOIN Tipos T ON P.idTipo = T.id " +
                                         "INNER JOIN Categorias C ON P.idCategoria = C.id " +
                                         "INNER JOIN Proveedores Pr ON P.id = Pr.id " +
                                         "WHERE P.id = @id ");




                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@id", id);

                // Ejecutar la consulta
                accesoDatos.EjecutarLectura();

                // Leer los datos obtenidos
                if (accesoDatos.Lector.Read())
                {

                    producto.nombre = accesoDatos.Lector.GetString(1);
                    producto.descripcion = accesoDatos.Lector.GetString(2);
                    producto.imagen = accesoDatos.Lector.GetString(3);
                    producto.precio = accesoDatos.Lector.GetDecimal(4);
                    producto.stock = accesoDatos.Lector.GetInt32(5);
                    producto.marca = accesoDatos.Lector.GetString(6);
                    producto.tipo = accesoDatos.Lector.GetString(7);
                    producto.categoria = accesoDatos.Lector.GetString(8);
                    producto.proveedor = accesoDatos.Lector.GetString(9);
                    producto.estado = accesoDatos.Lector.GetString(10);
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
           
        }
        public void ModificarProducto(Producto producto)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                // Usar parámetros para evitar inyecciones SQL
                accesoDatos.SetearConsulta("UPDATE Productos SET nombre = @nombre, descripcion = @descripcion, imagen = @imagen, precio = @precio WHERE id = @id");

                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@id", producto.id);
                accesoDatos.SetearParametro("@nombre", producto.nombre);
                accesoDatos.SetearParametro("@descripcion", producto.descripcion);
                accesoDatos.SetearParametro("@imagen", producto.imagen);
                accesoDatos.SetearParametro("@precio", producto.precio);

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


        public Producto BuscarProducto(int id)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            Producto producto = new Producto();
            try
            {
                // Usar parámetros para evitar inyecciones SQL
                accesoDatos.SetearConsulta("SELECT id, nombre, descripcion, imagen, precio FROM Productos WHERE id = @id");

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
                    producto.imagen = accesoDatos.Lector.GetString(3);
                    producto.precio = accesoDatos.Lector.GetDecimal(4);
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


        public Producto BuscarProducto(string query)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            Producto producto = new Producto();
            try
            {
                // Usar parámetros para evitar inyecciones SQL
                accesoDatos.SetearConsulta("SELECT id, nombre, descripcion, imagen, precio FROM Productos WHERE nombre LIKE @query OR descripcion LIKE @query");

                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@query", "%" + query + "%");

                // Ejecutar la consulta
                accesoDatos.EjecutarLectura();

                // Leer los datos obtenidos
                if (accesoDatos.Lector.Read())
                {
                    producto.id = accesoDatos.Lector.GetInt32(0);
                    producto.nombre = accesoDatos.Lector.GetString(1);
                    producto.descripcion = accesoDatos.Lector.GetString(2);
                    producto.imagen = accesoDatos.Lector.GetString(3);
                    producto.precio = accesoDatos.Lector.GetDecimal(4);
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

        public Producto ObtenerProducto(object id)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            Producto producto = new Producto();

            try
            {
                // Usar parámetros para evitar inyecciones SQL
                accesoDatos.SetearConsulta("SELECT P.id, P.nombre, P.descripcion, P.imagen, P.precio, S.cantidad, M.nombre, T.nombre, C.nombre, Pr.nombre, P.estado FROM Productos P " +
                                         "INNER JOIN Stock S ON P.id = S.idProducto " +
                                         "INNER JOIN Marcas M ON P.idMarca = M.id " +
                                         "INNER JOIN Tipos T ON P.idTipo = T.id " +
                                         "INNER JOIN Categorias C ON P.idCategoria = C.id " +
                                         "INNER JOIN Proveedores Pr ON P.id = Pr.id " +
                                         "WHERE P.id = @id ");

                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@id", id);

                // Ejecutar la consulta
                accesoDatos.EjecutarLectura();

                // Leer los datos obtenidos
                if (accesoDatos.Lector.Read())
                {
                    producto.nombre = accesoDatos.Lector.GetString(1);
                    producto.descripcion = accesoDatos.Lector.GetString(2);
                    producto.imagen = accesoDatos.Lector.GetString(3);
                    producto.precio = accesoDatos.Lector.GetDecimal(4);
                    producto.stock = accesoDatos.Lector.GetInt32(5);
                    producto.marca = accesoDatos.Lector.GetString(6);
                    producto.tipo = accesoDatos.Lector.GetString(7);
                    producto.categoria = accesoDatos.Lector.GetString(8);
                    producto.proveedor = accesoDatos.Lector.GetString(9);
                    producto.estado = accesoDatos.Lector.GetString(10);
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
    }
}
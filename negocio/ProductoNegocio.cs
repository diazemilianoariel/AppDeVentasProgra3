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
                accesoDatos.SetearConsulta("SELECT id, nombre, descripcion, imagen, precio FROM Productos");

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

        public Producto  ObtenerProducto(int id)
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



        public void AgregarProducto(Producto producto)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                // Usar parámetros para evitar inyecciones SQL
                accesoDatos.SetearConsulta("INSERT INTO Productos (nombre, descripcion, imagen, precio) VALUES (@nombre, @descripcion, @imagen, @precio)");

                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@nombre", producto.nombre);
                accesoDatos.SetearParametro("@descripcion", producto.descripcion);
                accesoDatos.SetearParametro("@imagen", producto.imagen);
                accesoDatos.SetearParametro("@precio", producto.precio);


                // Ejecutar la acción de inserción
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

      
    }
}
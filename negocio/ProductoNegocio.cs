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

        public void AgregarProducto()
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                // Usar parámetros para evitar inyecciones SQL
                accesoDatos.SetearConsulta("INSERT INTO Productos (nombre, descripcion, imagen, precio) VALUES (@nombre, @descripcion, @imagen, @precio)");

                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@nombre", "Producto de prueba");
                accesoDatos.SetearParametro("@descripcion", "Descripcion de prueba");
                accesoDatos.SetearParametro("@imagen", "imagen.jpg");
                accesoDatos.SetearParametro("@precio", 100);


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



        public void ModificarProducto()
        {
            // logica de negocio
        }

        public void EliminarProducto()
        {
            // logica de negocio
        }

        public List<Producto> ListarProductos()
        {
            
            AccesoDatos datos = new AccesoDatos();
            List<Producto> lista = new List<Producto>();

            try
            {
                datos.SetearConsulta("select Id, Nombre, Descripcion, ImagenUrl, Precio from Productos");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.id = (int)datos.Lector["Id"];
                    aux.nombre = (string)datos.Lector["Nombre"];
                    aux.descripcion = (string)datos.Lector["Descripcion"];
                    aux.imagen = (string)datos.Lector["ImagenUrl"];
                    aux.precio = (decimal)datos.Lector["Precio"];
                    lista.Add(aux);
                }
                datos.CerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void BuscarProducto()
        {
            // logica de negocio
        }
    }
}
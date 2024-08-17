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
            // logica para agregrar un producto a la base de datos 


          
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
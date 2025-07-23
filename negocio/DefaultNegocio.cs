using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace negocio
{
    public class DefaultNegocio
    {


        public List<Producto> ListarProductos(string filtro = "")
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
                    SELECT 
                        P.id, P.nombre, P.descripcion, P.precio, P.imagen, P.margenGanancia, P.estado,
                        S.cantidad as Stock
                    FROM Productos P
                    LEFT JOIN Stock S ON P.id = S.idProducto
                    WHERE P.estado = 1 ";

                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += "AND P.nombre LIKE @filtro";
                    datos.SetearParametro("@filtro", "%" + filtro + "%");
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.descripcion = (string)datos.Lector["descripcion"];
                    aux.precio = (decimal)datos.Lector["precio"];
                    aux.margenGanancia = (decimal)datos.Lector["margenGanancia"];
                    aux.estado = (bool)datos.Lector["estado"];
                    if (datos.Lector["imagen"] != DBNull.Value)
                        aux.Imagen = (string)datos.Lector["imagen"];

                    aux.stock = datos.Lector["Stock"] != DBNull.Value ? (int)datos.Lector["Stock"] : 0;

                    // CÁLCULO CLAVE: Se calcula el precio de venta para mostrar en el catálogo.
                    aux.precioVenta = aux.precio + (aux.precio * (aux.margenGanancia / 100));

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Producto ObtenerProducto(int idProducto)
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            return productoNegocio.ObtenerProducto(idProducto);
        }

        public List<Producto> BuscarProductos(string busqueda)
        {
            List<Producto> listaProductos = ListarProductos();
            return listaProductos.Where(p => p.nombre.ToLower().Contains(busqueda.ToLower())).ToList();
        }

        public void AgregarProductosAlCarrito(List<Producto> carrito, Producto producto, int Cantidad)
        {
            Producto productoEnCarrito = carrito.FirstOrDefault(p => p.id == producto.id);
            if (productoEnCarrito != null)
            {
                productoEnCarrito.Cantidad += Cantidad;
            }
            else
            {
                producto.Cantidad = Cantidad;
                carrito.Add(producto);



            }
        }

        public void QuitarProductoDelCarrito(List<Producto> carrito, int idProducto)
        {
            Producto productoEnCarrito = carrito.Find(p => p.id == idProducto);
            if (productoEnCarrito != null)
            {
                carrito.Remove(productoEnCarrito);
            }
        }


        // otro metodo que  traiga la cantidad de producto por ID 
        public int CantidadProductoEnCarrito(List<Producto> carrito, int idProducto)
        {
            Producto productoEnCarrito = carrito.Find(p => p.id == idProducto);
            return productoEnCarrito != null ? productoEnCarrito.Cantidad : 0;
        }




    }




}
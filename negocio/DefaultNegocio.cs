using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace negocio
{
    public class DefaultNegocio
    {



        public List<Producto> ListarProductos()
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            return productoNegocio.ListarProductos();
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
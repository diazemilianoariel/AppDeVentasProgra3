using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace negocio
{
    public class DefaultNegocio
    {


        // EN DefaultNegocio.cs

        public List<Producto> ListarProductos(string filtro = "", string idCategoria = "")
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
            SELECT 
                P.id, P.nombre, P.descripcion, P.precio, P.imagen, P.margenGanancia, P.estado,
                S.cantidad as Stock,
                P.idCategoria, C.nombre as CategoriaNombre, -- Agregamos datos de categoría
                P.idMarca, M.nombre as MarcaNombre          -- Agregamos datos de marca
            FROM Productos P
            LEFT JOIN Stock S ON P.id = S.idProducto
            LEFT JOIN Categorias C ON P.idCategoria = C.id  -- JOIN para obtener nombre de categoría
            LEFT JOIN Marcas M ON P.idMarca = M.id          -- JOIN para obtener nombre de marca
            WHERE P.estado = 1 ";

                // Filtro por búsqueda de texto
                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += " AND (P.nombre LIKE @filtro OR P.descripcion LIKE @filtro)";
                    datos.SetearParametro("@filtro", "%" + filtro + "%");
                }

                // ¡NUEVO! Filtro por categoría
                if (!string.IsNullOrEmpty(idCategoria))
                {
                    consulta += " AND P.idCategoria = @idCategoria";
                    datos.SetearParametro("@idCategoria", idCategoria);
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    // ... (resto de las asignaciones) ...

                    // Mapeamos los nuevos datos de las relaciones
                    aux.IdCategoria = (int)datos.Lector["idCategoria"];
                    aux.Categoria = new Categoria { Id = aux.IdCategoria, nombre = (string)datos.Lector["CategoriaNombre"] };

                    aux.idMarca = (int)datos.Lector["idMarca"];
                    aux.Marca = new Marca { Id = aux.idMarca, nombre = (string)datos.Lector["MarcaNombre"] };

                    aux.stock = datos.Lector["Stock"] != DBNull.Value ? (int)datos.Lector["Stock"] : 0;
                    aux.CalcularPrecioVenta(); // Usamos el método que creamos en la clase Producto

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

        //public List<Producto> BuscarProductos(string busqueda)
        //{
        //   return ListarProductos(busqueda);
        //}

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
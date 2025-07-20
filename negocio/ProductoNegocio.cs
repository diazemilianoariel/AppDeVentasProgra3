using System;
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
                    Producto producto = new Producto
                    {
                        id = accesoDatos.Lector.GetInt32(0),
                        nombre = accesoDatos.Lector.GetString(1),
                        descripcion = accesoDatos.Lector.GetString(2),
                        Imagen = accesoDatos.Lector.GetString(3),
                        precio = accesoDatos.Lector.GetDecimal(4),
                        margenGanancia = accesoDatos.Lector.GetDecimal(5),
                        stock = accesoDatos.Lector.GetInt32(6),
                        Marca = new Marca { nombre = accesoDatos.Lector.GetString(7) },
                        Tipo = new Tipos { nombre = accesoDatos.Lector.GetString(8) },
                        Categoria = new Categoria { nombre = accesoDatos.Lector.GetString(9) },
                        estado = accesoDatos.Lector.GetBoolean(10),
                        proveedor = new Proveedor { Nombre = accesoDatos.Lector.GetString(11) }
                    };



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
                accesoDatos.SetearConsulta(" SELECT P.id, P.nombre, P.descripcion, P.imagen, P.precio, P.margenGanancia, S.cantidad, M.id AS MarcaId, M.nombre AS MarcaNombre, T.id AS TipoId, T.nombre AS TipoNombre, C.id AS CategoriaId, C.nombre AS CategoriaNombre, Pr.id AS ProveedorId, Pr.nombre AS ProveedorNombre, P.estado FROM Productos P " +
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
                    producto.Marca = new Marca { Id = accesoDatos.Lector.GetInt32(7), nombre = accesoDatos.Lector.GetString(8) };
                    producto.Tipo = new Tipos { Id = accesoDatos.Lector.GetInt32(9), nombre = accesoDatos.Lector.GetString(10) };
                    producto.Categoria = new Categoria { Id = accesoDatos.Lector.GetInt32(11), nombre = accesoDatos.Lector.GetString(12) };
                    producto.proveedor = new Proveedor { Id = accesoDatos.Lector.GetInt32(13), Nombre = accesoDatos.Lector.GetString(14) };
                    producto.estado = accesoDatos.Lector.GetBoolean(15);


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


        /* public void AgregarProducto(Producto producto)
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
                 accesodatos.SetearParametro("@idMarca", producto.idMarca);
                 accesodatos.SetearParametro("@idTipo", producto.idTipo);
                 accesodatos.SetearParametro("@idCategoria", producto.IdCategoria);




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
                 AccesoDatos accesoDatos = new AccesoDatos();
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
                 AccesoDatos accesoDatos = new AccesoDatos();
                 accesodatos.SetearConsulta("INSERT INTO Proveedores_Productos (idProveedor, idProducto) " +
                                            "VALUES (@idProveedor, @idProductoProveedorPorProducto)");

                 accesodatos.SetearParametro("@idProveedor", producto.IdProveedor);
                 accesodatos.SetearParametro("@idProductoProveedorPorProducto", idProducto);

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



         }*/


        public void AgregarProducto(Producto producto)
        {
            AccesoDatos accesodatos = new AccesoDatos();
            int idProducto;

            try
            {
                // Verificar si el producto ya existe
                accesodatos.SetearConsulta("SELECT COUNT(*) FROM Productos WHERE nombre = @nombree AND descripcion = @descripcionn");
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
                accesodatos.SetearParametro("@idMarca", producto.Marca.Id);
                accesodatos.SetearParametro("@idTipo", producto.Tipo.Id);
                accesodatos.SetearParametro("@idCategoria", producto.Categoria.Id);



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
                accesodatos.SetearConsulta("INSERT INTO Proveedores_Productos (idProveedor, idProducto) " +
                                           "VALUES (@idProveedor, @idProductoProveedorPorProducto)");

                accesodatos.SetearParametro("@idProveedor", producto.proveedor.Id);
                accesodatos.SetearParametro("@idProductoProveedorPorProducto", idProducto);

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

                accesoDatos.SetearConsulta("UPDATE Productos SET nombre = @nombre, descripcion = @descripcion, imagen = @imagen, precio = @precio, estado = @estado, idMarca = @idMarca, idCategoria = @idCategoria, idTipo = @idTipo  WHERE id = @idProducto");

                accesoDatos.SetearParametro("@nombre", producto.nombre);
                accesoDatos.SetearParametro("@descripcion", producto.descripcion);
                accesoDatos.SetearParametro("@imagen", producto.Imagen);
                accesoDatos.SetearParametro("@precio", producto.precio);
                accesoDatos.SetearParametro("@estado", producto.estado);
                accesoDatos.SetearParametro("@idMarca", producto.Marca.Id);
                accesoDatos.SetearParametro("@idTipo", producto.Tipo.Id);
                accesoDatos.SetearParametro("@idCategoria", producto.Categoria.Id);



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

            // Modificamos la cantidad de la tabla stock
            try
            {
                accesoDatos = new AccesoDatos();
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




            // Modificamos el nombre del proveedor en su tabla Proveedores
            try
            {

                accesoDatos = new AccesoDatos();
                accesoDatos.SetearConsulta("UPDATE Proveedores_Productos SET idProveedor = @idProveedor WHERE idProducto = @idProducto");

                // Agregar parámetros con los valores correspondientes
                accesoDatos.SetearParametro("@idProveedor", producto.proveedor.Id);
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
        }




        public void bajaLogicaProducto(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // En lugar de borrar, simplemente cambiamos el estado a 0 (inactivo).
                // Esto preserva toda la integridad de los datos históricos.
                datos.SetearConsulta("UPDATE Productos SET estado = 0 WHERE id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Se cierra la conexión del objeto 'datos' usado en el try.
                datos.CerrarConexion();
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
                    producto.Marca.nombre = accesoDatos.Lector.GetString(7);
                    producto.Marca.nombre = accesoDatos.Lector.GetString(8);
                    producto.Marca.nombre = accesoDatos.Lector.GetString(9);
                    producto.estado = accesoDatos.Lector.GetBoolean(10);
                    producto.Marca.nombre = accesoDatos.Lector.GetString(11);

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
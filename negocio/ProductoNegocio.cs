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
        // EN: ProductoNegocio.cs

        public List<Producto> Listar(string filtro = "", string idCategoria = "", int idProducto = 0)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
            SELECT P.id, P.nombre, P.descripcion, P.imagen, P.precio, P.margenGanancia, P.estado,
                   ISNULL(S.cantidad, 0) as Stock,
                   M.id as idMarca, M.nombre as MarcaNombre,
                   C.id as idCategoria, C.nombre as CategoriaNombre,
                   T.id as idTipo, T.nombre as TipoNombre
            FROM Productos P
            LEFT JOIN Stock S ON P.id = S.idProducto
            LEFT JOIN Marcas M ON P.idMarca = M.id
            LEFT JOIN Categorias C ON P.idCategoria = C.id
            LEFT JOIN Tipos T ON P.idTipo = T.id
            WHERE P.estado = 1";

                if (idProducto > 0)
                {
                    consulta += " AND P.id = @idProducto";
                    datos.SetearParametro("@idProducto", idProducto);
                }

                // --- INICIO DEL CÓDIGO QUE FALTABA ---
                if (!string.IsNullOrEmpty(idCategoria))
                {
                    consulta += " AND P.idCategoria = @idCategoria";
                    datos.SetearParametro("@idCategoria", idCategoria);
                }

                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += " AND (P.nombre LIKE @filtro OR P.descripcion LIKE @filtro)";
                    datos.SetearParametro("@filtro", "%" + filtro + "%");
                }
                // --- FIN DEL CÓDIGO QUE FALTABA ---

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.descripcion = (string)datos.Lector["descripcion"];
                    if (datos.Lector["imagen"] != DBNull.Value)
                        aux.Imagen = (string)datos.Lector["imagen"];
                    aux.precio = (decimal)datos.Lector["precio"];
                    aux.margenGanancia = (decimal)datos.Lector["margenGanancia"];
                    aux.estado = (bool)datos.Lector["estado"];
                    aux.stock = (int)datos.Lector["Stock"];

                    aux.Marca = new Marca { Id = (int)datos.Lector["idMarca"], nombre = (string)datos.Lector["MarcaNombre"] };
                    aux.Categoria = new Categoria { Id = (int)datos.Lector["idCategoria"], nombre = (string)datos.Lector["CategoriaNombre"] };
                    aux.Tipo = new Tipos { Id = (int)datos.Lector["idTipo"], nombre = (string)datos.Lector["TipoNombre"] };

                    aux.CalcularPrecioVenta();
                    lista.Add(aux);
                }

                if (idProducto > 0 && lista.Count > 0)
                {
                    datos.Lector.Close();
                    string consultaProveedores = "SELECT Pr.id, Pr.nombre FROM Proveedores Pr INNER JOIN Proveedores_Productos PP ON Pr.id = PP.idProveedor WHERE PP.idProducto = @idProducto";

                    // Limpiamos parámetros viejos antes de setear uno nuevo para esta consulta específica
                    datos.LimpiarParametros();
                    datos.SetearParametro("@idProducto", idProducto);
                    datos.SetearConsulta(consultaProveedores);
                    datos.EjecutarLectura();
                    while (datos.Lector.Read())
                    {
                        lista[0].Proveedores.Add(new Proveedor { Id = (int)datos.Lector["id"], Nombre = (string)datos.Lector["nombre"] });
                    }
                }

                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }
        }

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

        public Producto ObtenerProducto(int id)
        {
            // 1. Reutilizamos el método que ya hace todo el trabajo pesado.
            List<Producto> lista = this.Listar(idProducto: id);

            // 2. Si la lista tiene un resultado, ese es nuestro producto.
            if (lista.Count > 0)
            {
                return lista[0]; // Devolvemos el primer (y único) elemento.
            }

            // 3. Si no se encontró, devolvemos null.
            return null;
        }

        public void AgregarProducto(Producto nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                // 1. Verificamos si el producto existe y obtenemos su estado.
                datos.SetearConsulta("SELECT id, estado FROM Productos WHERE nombre = @nombre");
                datos.SetearParametro("@nombre", nuevo.nombre);
                datos.AbrirConexion();
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    bool estaActivo = (bool)datos.Lector["estado"];
                    int idExistente = (int)datos.Lector["id"];

                    if (estaActivo)
                    {
                        // Si está ACTIVO, lanzamos un error normal.
                        throw new Exception("Ya existe un producto activo con el nombre '" + nuevo.nombre + "'.");
                    }
                    else
                    {
                        // Si está INACTIVO, lanzamos nuestra excepción especial.
                        throw new ProductoInactivoException("Producto inactivo encontrado.", idExistente);
                    }
                }
                datos.CerrarConexion();

                // Iniciamos la conexión y la transacción si es que no se encontró un producto activo.
                datos.AbrirConexion();
                datos.IniciarTransaccion();

                // 1. Insertar en la tabla Productos y obtener el nuevo ID
                string consultaProducto = "INSERT INTO Productos (nombre, descripcion, imagen, precio, margenGanancia, estado, idMarca, idTipo, idCategoria) " +
                                          "VALUES (@nombre, @descripcion, @imagen, @precio, @margenGanancia, @estado, @idMarca, @idTipo, @idCategoria); " +
                                          "SELECT SCOPE_IDENTITY();";
                datos.SetearConsulta(consultaProducto);
                datos.SetearParametro("@nombre", nuevo.nombre);
                datos.SetearParametro("@descripcion", nuevo.descripcion);
                datos.SetearParametro("@imagen", nuevo.Imagen);
                datos.SetearParametro("@precio", nuevo.precio);
                datos.SetearParametro("@margenGanancia", nuevo.margenGanancia);
                datos.SetearParametro("@estado", nuevo.estado);
                datos.SetearParametro("@idMarca", nuevo.Marca.Id);
                datos.SetearParametro("@idTipo", nuevo.Tipo.Id);
                datos.SetearParametro("@idCategoria", nuevo.Categoria.Id);

                int idProductoNuevo = Convert.ToInt32(datos.EjecutarEscalar());
                nuevo.id = idProductoNuevo;

                // 2. Insertar en la tabla Stock
                string consultaStock = "INSERT INTO Stock (idProducto, cantidad, stockMinimo, fechaActualizacion) VALUES (@idProducto, @cantidad, 5, GETDATE())";
                datos.SetearConsulta(consultaStock);

                
                datos.LimpiarParametros();

                datos.SetearParametro("@idProducto", nuevo.id);
                datos.SetearParametro("@cantidad", nuevo.stock);
                datos.EjecutarAccion();

                // 3. Insertar en la tabla Proveedores_Productos
                if (nuevo.Proveedores != null && nuevo.Proveedores.Any())
                {
                    string consultaProveedor = "INSERT INTO Proveedores_Productos (idProveedor, idProducto) VALUES (@idProveedor, @idProducto)";
                    foreach (Proveedor prov in nuevo.Proveedores)
                    {
                        datos.SetearConsulta(consultaProveedor);
                        datos.LimpiarParametros();
                        datos.SetearParametro("@idProveedor", prov.Id);
                        datos.SetearParametro("@idProducto", nuevo.id);
                        datos.EjecutarAccion();
                    }
                }

                // Si todo salió bien, confirmamos la transacción
                datos.ConfirmarTransaccion();
            }
            catch (Exception ex)
            {
                // Si algo falló, revertimos todo
                datos.RevertirTransaccion();
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

  

        public void ModificarProducto(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.AbrirConexion();
                datos.IniciarTransaccion();

                // 1. Actualizar la tabla Productos
                string consultaProducto = @"
            UPDATE Productos 
            SET nombre = @nombre, descripcion = @descripcion, imagen = @imagen, 
                precio = @precio, margenGanancia = @margenGanancia, estado = @estado, 
                idMarca = @idMarca, idTipo = @idTipo, idCategoria = @idCategoria 
            WHERE id = @id";

                datos.SetearConsulta(consultaProducto);

                // --- INICIO DEL CÓDIGO CORREGIDO QUE FALTABA ---
                // Seteamos TODOS los parámetros que la consulta UPDATE necesita.
                datos.SetearParametro("@nombre", producto.nombre);
                datos.SetearParametro("@descripcion", producto.descripcion);
                datos.SetearParametro("@imagen", producto.Imagen);
                datos.SetearParametro("@precio", producto.precio);
                datos.SetearParametro("@margenGanancia", producto.margenGanancia);
                datos.SetearParametro("@estado", producto.estado);
                datos.SetearParametro("@idMarca", producto.Marca.Id);
                datos.SetearParametro("@idTipo", producto.Tipo.Id);
                datos.SetearParametro("@idCategoria", producto.Categoria.Id);
                datos.SetearParametro("@id", producto.id); // No olvidar el ID para el WHERE
                                                           // --- FIN DEL CÓDIGO CORREGIDO ---

                datos.EjecutarAccion();

                // 2. Actualizar la tabla Stock
                string consultaStock = "UPDATE Stock SET cantidad = @cantidad, fechaActualizacion = GETDATE() WHERE idProducto = @id";
                datos.SetearConsulta(consultaStock);
                datos.LimpiarParametros();
                datos.SetearParametro("@cantidad", producto.stock);
                datos.SetearParametro("@id", producto.id);
                datos.EjecutarAccion();

                // 3. Actualizar los proveedores (borrar los viejos e insertar los nuevos)
                string consultaBorrarProv = "DELETE FROM Proveedores_Productos WHERE idProducto = @id";
                datos.SetearConsulta(consultaBorrarProv);
                datos.LimpiarParametros();
                datos.SetearParametro("@id", producto.id);
                datos.EjecutarAccion();

                if (producto.Proveedores != null && producto.Proveedores.Any())
                {
                    string consultaInsertarProv = "INSERT INTO Proveedores_Productos (idProveedor, idProducto) VALUES (@idProveedor, @id)";
                    foreach (Proveedor prov in producto.Proveedores)
                    {
                        datos.SetearConsulta(consultaInsertarProv);
                        datos.LimpiarParametros();
                        datos.SetearParametro("@idProveedor", prov.Id);
                        datos.SetearParametro("@id", producto.id);
                        datos.EjecutarAccion();
                    }
                }

                datos.ConfirmarTransaccion();
            }
            catch (Exception ex)
            {
                datos.RevertirTransaccion();
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
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


        // EN: ProductoNegocio.cs

        public Producto ObtenerProductoParaAdmin(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
            SELECT P.id, P.nombre, P.descripcion, P.imagen, P.precio, P.margenGanancia, P.estado,
                   ISNULL(S.cantidad, 0) as Stock,
                   M.id as idMarca, M.nombre as MarcaNombre,
                   C.id as idCategoria, C.nombre as CategoriaNombre,
                   T.id as idTipo, T.nombre as TipoNombre
            FROM Productos P
            LEFT JOIN Stock S ON P.id = S.idProducto
            LEFT JOIN Marcas M ON P.idMarca = M.id
            LEFT JOIN Categorias C ON P.idCategoria = C.id
            LEFT JOIN Tipos T ON P.idTipo = T.id
            WHERE P.id = @id";

                datos.SetearConsulta(consulta);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();

                Producto producto = null;
                if (datos.Lector.Read())
                {
                    producto = new Producto();
                    // Mapeo de datos del producto...
                    producto.id = (int)datos.Lector["id"];
                    producto.nombre = (string)datos.Lector["nombre"];
                    producto.descripcion = (string)datos.Lector["descripcion"];
                    if (datos.Lector["imagen"] != DBNull.Value)
                        producto.Imagen = (string)datos.Lector["imagen"];
                    producto.precio = (decimal)datos.Lector["precio"];
                    producto.margenGanancia = (decimal)datos.Lector["margenGanancia"];
                    producto.estado = (bool)datos.Lector["estado"];
                    producto.stock = (int)datos.Lector["Stock"];

                    producto.Marca = new Marca { Id = (int)datos.Lector["idMarca"], nombre = (string)datos.Lector["MarcaNombre"] };
                    producto.Categoria = new Categoria { Id = (int)datos.Lector["idCategoria"], nombre = (string)datos.Lector["CategoriaNombre"] };
                    producto.Tipo = new Tipos { Id = (int)datos.Lector["idTipo"], nombre = (string)datos.Lector["TipoNombre"] };
                    producto.CalcularPrecioVenta();
                }
                else
                {
                    return null;
                }

                datos.Lector.Close();

                // Cargamos la lista de proveedores para este producto
                string consultaProveedores = "SELECT Pr.id, Pr.nombre FROM Proveedores Pr INNER JOIN Proveedores_Productos PP ON Pr.id = PP.idProveedor WHERE PP.idProducto = @id";
                datos.SetearConsulta(consultaProveedores);

                // --- INICIO DE LA CORRECCIÓN ---
                // Limpiamos los parámetros de la consulta anterior...
                datos.LimpiarParametros();
                // ...y volvemos a agregar el que necesitamos para esta nueva consulta.
                datos.SetearParametro("@id", id);
                // --- FIN DE LA CORRECCIÓN ---

                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    producto.Proveedores.Add(new Proveedor { Id = (int)datos.Lector["id"], Nombre = (string)datos.Lector["nombre"] });
                }

                return producto;
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



        public class ProductoInactivoException : Exception
        {
            public int IdProductoExistente { get; set; }

            public ProductoInactivoException(string mensaje, int id) : base(mensaje)
            {
                this.IdProductoExistente = id;
            }
        }


    }

}
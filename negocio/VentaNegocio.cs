using System;
using System.Collections.Generic;

using dominio;


namespace negocio
{
    public class VentaNegocio
    {


        

        public List<Venta> ListarVentasPorUsuario(int idUsuario, string filtro = "")
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
            SELECT V.id, V.fecha, V.monto, E.nombre as EstadoVenta
            FROM Ventas V
            INNER JOIN EstadoVenta E ON V.idEstadoVenta = E.id
            WHERE V.idUsuario = @idUsuario";

                datos.SetearParametro("@idUsuario", idUsuario);

                if (!string.IsNullOrEmpty(filtro))
                {



                    consulta += @" AND (
                            E.nombre LIKE @filtro OR 
                            CAST(V.id AS VARCHAR(20)) LIKE @filtro OR 
                            CONVERT(VARCHAR, V.fecha, 103) LIKE @filtro
                          )";


                    datos.SetearParametro("@filtro", "%" + filtro + "%");
                }

                consulta += " ORDER BY V.fecha DESC";

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta aux = new Venta();
                    aux.IdVenta = (int)datos.Lector["id"];
                    aux.Fecha = (DateTime)datos.Lector["fecha"];
                    aux.Monto = (decimal)datos.Lector["monto"];
                    aux.nombreEstadoVenta = (string)datos.Lector["EstadoVenta"];
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

        public int cantidadVentasHoy()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) as cantidad FROM Ventas WHERE CAST(fecha AS DATE) = CAST(GETDATE() AS DATE)");
                
                datos.EjecutarLectura();
                datos.Lector.Read();
                return (int)datos.Lector["cantidad"];
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

        public int cantidadVentaPendiente()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select count(*) as cantidad from ventas where idEstadoVenta = 1");
                datos.EjecutarLectura();
                datos.Lector.Read();
                return (int)datos.Lector["cantidad"];
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

        public List<Venta> ListarVentas( String filtro = "")
        {
            List<Venta> ventas = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                string consulta = "SELECT v.id, v.fecha, v.idUsuario, U.nombre, U.apellido, U.dni, U.email, U.telefono, U.direccion, v.enLocal, E.nombre as nombreestadoventa, v.idEstadoVenta, v.monto FROM ventas v INNER JOIN Usuarios U ON v.idUsuario = U.id INNER JOIN EstadoVenta E ON v.idEstadoVenta = E.id WHERE v.idEstadoVenta != 1 ";

                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += "AND (U.nombre LIKE @filtro OR U.apellido LIKE @filtro OR U.email LIKE @filtro)";
                    datos.SetearParametro("@filtro", "%" + filtro + "%");
                }


                consulta += " ORDER BY v.fecha DESC";
                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

               


                while (datos.Lector.Read())
                {

                    Venta aux = new Venta();
                    aux.IdVenta = (int)datos.Lector["id"];
                    aux.Fecha = (DateTime)datos.Lector["fecha"];
                    aux.Monto = (decimal)datos.Lector["monto"];
                    aux.EnLocal = (bool)datos.Lector["enLocal"];
                    aux.idEstadoVenta = (int)datos.Lector["idEstadoVenta"];
                    aux.nombreEstadoVenta = (string)datos.Lector["nombreestadoventa"];

                    aux.Cliente = new Usuario();
                   
                    aux.Cliente.Id = (int)datos.Lector["idUsuario"];
                    aux.Cliente.Nombre = (string)datos.Lector["nombre"];
                    aux.Cliente.Apellido = (string)datos.Lector["apellido"];
                    aux.Cliente.Dni = (string)datos.Lector["dni"];
                    aux.Cliente.Email = (string)datos.Lector["email"];
                    aux.Cliente.Telefono = (string)datos.Lector["telefono"];
                    aux.Cliente.Direccion = (string)datos.Lector["direccion"];

                    ventas.Add(aux);


                }

                return ventas;

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


        public List<Producto> ListarProductosPorVenta(int idVenta)
        {
            List<Producto> productos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // La consulta ahora es más simple y trae el dato que importa: dv.precioVenta
                string consulta = @"
                    SELECT p.id, p.nombre, dv.cantidad, dv.precioVenta 
                    FROM DetalleVentas dv 
                    INNER JOIN Productos p ON dv.idProducto = p.id 
                    WHERE dv.idVenta = @idVenta";

                datos.SetearConsulta(consulta);
                datos.SetearParametro("@idVenta", idVenta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();
                    producto.id = (int)datos.Lector["id"];
                    producto.nombre = (string)datos.Lector["nombre"];
                    producto.Cantidad = (int)datos.Lector["cantidad"];

                    // ¡ESTA ES LA CORRECCIÓN CLAVE!
                    // Le asignamos el precio histórico de la venta.
                    producto.precioVenta = (decimal)datos.Lector["precioVenta"];

                    productos.Add(producto);
                }
                return productos;
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


        public List<Venta> ListarVentasPendientes(string filtro = "")
        {


            List<Venta> ventas = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT v.id, v.fecha, v.idUsuario, U.nombre, U.apellido, U.dni, U.email, U.telefono, U.direccion, v.enLocal, E.nombre as nombreestadoventa, v.idEstadoVenta, v.monto FROM ventas v INNER JOIN Usuarios U ON v.idUsuario = U.id INNER JOIN EstadoVenta E ON v.idEstadoVenta = E.id WHERE v.idEstadoVenta = 1 ";

                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += "AND (U.nombre LIKE @filtro OR U.apellido LIKE @filtro OR U.email LIKE @filtro)";
                    datos.SetearParametro("@filtro", "%" + filtro + "%");
                }

                consulta += " ORDER BY v.fecha ASC";
                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();


          


                while (datos.Lector.Read())
                {

                    Venta aux = new Venta();
                    aux.IdVenta = (int)datos.Lector["id"];
                    aux.Fecha = (DateTime)datos.Lector["fecha"];
                    aux.Monto = (decimal)datos.Lector["monto"];
                    aux.EnLocal = (bool)datos.Lector["enLocal"];
                    aux.idEstadoVenta = (int)datos.Lector["idEstadoVenta"];
                    aux.nombreEstadoVenta = (string)datos.Lector["nombreestadoventa"];

                    aux.Cliente = new Usuario();
                   
                    aux.Cliente.Id = (int)datos.Lector["idUsuario"];
                    aux.Cliente.Nombre = (string)datos.Lector["nombre"];
                    aux.Cliente.Apellido = (string)datos.Lector["apellido"];
                    aux.Cliente.Dni = (string)datos.Lector["dni"];
                    aux.Cliente.Email = (string)datos.Lector["email"];
                    aux.Cliente.Telefono = (string)datos.Lector["telefono"];
                    aux.Cliente.Direccion = (string)datos.Lector["direccion"];

                    ventas.Add(aux);
                }

                return ventas;

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

        public void AprobarVenta(int idVenta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update ventas set idEstadoVenta = 2 where id = @idVenta");

                datos.SetearParametro("@idVenta", idVenta);
                datos.EjecutarAccion();
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


        // EN: VentaNegocio.cs
        // Agregá este método

        public Venta ObtenerVentaParaNotificacion(int idVenta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Traemos los datos básicos de la venta y el email del usuario.
                string consulta = @"
            SELECT v.id, U.Email, U.Nombre 
            FROM Ventas v 
            INNER JOIN Usuarios U ON v.idUsuario = U.id 
            WHERE v.id = @idVenta";

                datos.SetearConsulta(consulta);
                datos.SetearParametro("@idVenta", idVenta);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    Venta venta = new Venta();
                    venta.IdVenta = (int)datos.Lector["id"];
                    venta.Cliente = new Usuario
                    {
                        Email = (string)datos.Lector["Email"],
                        Nombre = (string)datos.Lector["Nombre"]
                    };
                    return venta;
                }
                return null;
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


        public void RechazarVenta(int idVenta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update ventas set idEstadoVenta = 3 where id = @idVenta");
                datos.SetearParametro("@idVenta", idVenta);
                datos.EjecutarAccion();
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



        // AGREGÁ ESTE MÉTODO A VentaNegocio.cs

        public void RechazarVentaYDevolverStock(int idVenta, List<Producto> productos)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Iniciamos la conexión y la transacción
                datos.AbrirConexion();
                datos.IniciarTransaccion();

                // --- TAREA 1: Devolver el stock de cada producto ---
                foreach (var producto in productos)
                {
                    datos.SetearConsulta("UPDATE Stock SET cantidad = cantidad + @cantidad WHERE idProducto = @idProducto");
                    datos.LimpiarParametros();
                    datos.SetearParametro("@cantidad", producto.Cantidad);
                    datos.SetearParametro("@idProducto", producto.id);
                    datos.EjecutarAccion();
                }

                // --- TAREA 2: Cambiar el estado de la venta a "Cancelado" (ID 3) ---
                datos.SetearConsulta("UPDATE Ventas SET idEstadoVenta = 3 WHERE id = @idVenta");
                datos.LimpiarParametros();
                datos.SetearParametro("@idVenta", idVenta);
                datos.EjecutarAccion();

                // Si ambas tareas fueron exitosas, confirmamos la transacción
                datos.ConfirmarTransaccion();
            }
            catch (Exception ex)
            {
                // Si algo falló, revertimos todos los cambios
                datos.RevertirTransaccion();
                throw ex;
            }
            finally
            {
                // Cerramos la conexión al final
                datos.CerrarConexion();
            }
        }


        public List<Venta> BuscarVentasPendientes(string query)
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearConsulta("SELECT * FROM Ventas WHERE (idVenta LIKE @query OR Cliente.id LIKE @query OR nombreEstadoVenta LIKE @query) AND idEstadoVenta = 1");
                accesoDatos.SetearParametro("@query", "%" + query + "%");
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Venta venta = new Venta();
                    // Asignar valores a las propiedades de venta
                    lista.Add(venta);
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

        public List<Producto> ObtenerCarritoPorVenta(int idVenta)
        {
            List<Producto> productos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select p.id, p.nombre, p.descripcion, p.precio, dv.cantidad, p.margenGanancia from DetalleVentas dv inner join Productos p on dv.idProducto = p.id where dv.idVenta = @idVenta");
                datos.SetearParametro("@idVenta", idVenta);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();
                    producto.id = (int)datos.Lector["id"];
                    producto.nombre = (string)datos.Lector["nombre"];
                    producto.descripcion = (string)datos.Lector["descripcion"];
                    producto.precio = (decimal)datos.Lector["precio"];
                    producto.Cantidad = (int)datos.Lector["cantidad"];
                    producto.margenGanancia = (decimal)datos.Lector["margenGanancia"];
                 
                    productos.Add(producto);
                }
                return productos;
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

        public List<Venta> BuscarVentas(string query)
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearConsulta("SELECT v.id, v.fecha, v.idUsuario, u.nombre, u.apellido, u.direccion, u.telefono, u.email, v.enLocal, e.nombre as nombreestadoventa, v.idEstadoVenta FROM Ventas v INNER JOIN Usuarios u ON v.idUsuario = u.id INNER JOIN EstadoVenta e ON v.idEstadoVenta = e.id WHERE v.id = @query");
                accesoDatos.SetearParametro("@query", query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Venta venta = new Venta();
                    venta.IdVenta = (int)accesoDatos.Lector["id"];
                    venta.Fecha = (DateTime)accesoDatos.Lector["fecha"];
                    venta.Cliente = new Usuario
                    {
                        Id = (int)accesoDatos.Lector["idUsuario"],
                        Nombre = (string)accesoDatos.Lector["nombre"],
                        Apellido = (string)accesoDatos.Lector["apellido"],
                        Direccion = (string)accesoDatos.Lector["direccion"],
                        Telefono = (string)accesoDatos.Lector["telefono"],
                        Email = (string)accesoDatos.Lector["email"]
                    };
                    venta.EnLocal = (bool)accesoDatos.Lector["enLocal"];
                    venta.idEstadoVenta = (int)accesoDatos.Lector["idEstadoVenta"];
                    venta.nombreEstadoVenta = (string)accesoDatos.Lector["nombreestadoventa"];
                    venta.Productos = ListarProductosPorVenta(venta.IdVenta);
                    lista.Add(venta);
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



        public Venta ObtenerVentaPorId(int idVenta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT v.id, v.fecha, v.idUsuario, v.monto, U.nombre, U.apellido, U.direccion, U.telefono, U.email FROM ventas v INNER JOIN Usuarios U ON v.idUsuario = U.id WHERE v.id = @idVenta");
                datos.SetearParametro("@idVenta", idVenta);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    Venta venta = new Venta();
                    venta.IdVenta = (int)datos.Lector["id"];
                    venta.Fecha = (DateTime)datos.Lector["fecha"];
                    venta.Monto = (decimal)datos.Lector["monto"];

                    venta.Cliente = new Usuario();
                    venta.Cliente.Id = (int)datos.Lector["idUsuario"];
                    venta.Cliente.Nombre = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : "";
                    venta.Cliente.Apellido = datos.Lector["apellido"] != DBNull.Value ? (string)datos.Lector["apellido"] : "";
                    venta.Cliente.Direccion = datos.Lector["direccion"] != DBNull.Value ? (string)datos.Lector["direccion"] : "";
                    venta.Cliente.Telefono = datos.Lector["telefono"] != DBNull.Value ? (string)datos.Lector["telefono"] : "";
                    venta.Cliente.Email = datos.Lector["email"] != DBNull.Value ? (string)datos.Lector["email"] : "";

                    // Cerramos el lector para la siguiente consulta
                    datos.Lector.Close();

                    // Llamamos al método CORREGIDO para cargar los productos.
                    venta.Productos = ListarProductosPorVenta(venta.IdVenta);
                    return venta;
                }
                return null;
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


        public decimal CalcularMontoPendiente()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT SUM(ISNULL(monto, 0)) FROM Ventas WHERE idEstadoVenta = 1");
                object result = datos.EjecutarEscalar();
                if (result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }
                return 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

        }


        public int ContarVentasPorEstado(int idEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM Ventas WHERE idEstadoVenta = @idEstado");
                datos.SetearParametro("@idEstado", idEstado);
                return (int)datos.EjecutarEscalar();
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


        public decimal CalcularIngresosTotales()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT SUM(ISNULL(monto, 0)) FROM Ventas WHERE idEstadoVenta = 2"); // Estado 2 = Aprobado
                object result = datos.EjecutarEscalar();
                if (result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }
                return 0;
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





        public List<Venta> ListarVentasParaReporte(string filtro = "", DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            List<Venta> ventas = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // CORRECCIÓN: Agregamos el INNER JOIN a EstadoVenta y seleccionamos E.nombre
                string consulta = @"
            SELECT v.id, v.fecha, v.idUsuario, U.nombre, U.apellido, v.monto, E.nombre as nombreEstadoVenta
            FROM Ventas v 
            INNER JOIN Usuarios U ON v.idUsuario = U.id
            INNER JOIN EstadoVenta E ON v.idEstadoVenta = E.id
            WHERE 1=1";

                if (fechaDesde.HasValue)
                {
                    consulta += " AND v.fecha >= @fechaDesde";
                    datos.SetearParametro("@fechaDesde", fechaDesde.Value);
                }
                if (fechaHasta.HasValue)
                {
                    consulta += " AND v.fecha < @fechaHasta";
                    datos.SetearParametro("@fechaHasta", fechaHasta.Value.AddDays(1));
                }

                consulta += " ORDER BY v.fecha DESC";
                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta aux = new Venta();
                    aux.IdVenta = (int)datos.Lector["id"];
                    aux.Fecha = (DateTime)datos.Lector["fecha"];
                    aux.Monto = (decimal)datos.Lector["monto"];

                    // CORRECCIÓN: Ahora leemos el nombre del estado que viene de la consulta
                    aux.nombreEstadoVenta = (string)datos.Lector["nombreEstadoVenta"];

                    aux.Cliente = new Usuario
                    {
                        Id = (int)datos.Lector["idUsuario"],
                        Nombre = (string)datos.Lector["nombre"],
                        Apellido = (string)datos.Lector["apellido"]
                    };
                    ventas.Add(aux);
                }
                return ventas;
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



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dominio;
using negocio;

namespace negocio
{
    public class VentaNegocio
    {


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

        public List<Venta> ListarVentas()
        {
            List<Venta> ventas = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("select v.id, v.fecha, v.idUsuario, U.nombre, U.apellido, U.dni, U.email, U.telefono, U.direccion, v.enLocal, E.nombre as nombreestadoventa, v.idEstadoVenta, v.monto from ventas v inner join Usuarios U on v.idUsuario = U.id INNER JOIN EstadoVenta E on v.idEstadoVenta = E.id where v.idEstadoVenta != 1");
                datos.EjecutarLectura();


                while (datos.Lector.Read())
                {



                    Venta aux = new Venta();
                    aux.IdVenta = (int)datos.Lector["id"];
                    aux.Fecha = (DateTime)datos.Lector["fecha"];
                    aux.Monto = (decimal)datos.Lector["monto"];

                    aux.Cliente = new Cliente();
                    aux.Cliente.Id = (int)datos.Lector["id"];
                    aux.Cliente.Nombre = (string)datos.Lector["nombre"];
                    aux.Cliente.Apellido = (string)datos.Lector["apellido"];
                    aux.Cliente.Dni = (string)datos.Lector["dni"];
                    aux.Cliente.Email = (string)datos.Lector["email"];
                    aux.Cliente.Telefono = (string)datos.Lector["telefono"];
                    aux.Cliente.Direccion = (string)datos.Lector["direccion"];
                    aux.EnLocal = (bool)datos.Lector["enLocal"];
                    aux.idEstadoVenta = (int)datos.Lector["idEstadoVenta"];
                    aux.nombreEstadoVenta = (string)datos.Lector["nombreestadoventa"];


                    // Cargar los productos de la venta
                    aux.Productos = ListarProductosPorVenta(aux.IdVenta);




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
                datos.SetearConsulta("select p.id, p.nombre, p.descripcion, p.precio, p.margenGanancia, dv.cantidad from DetalleVentas dv inner join Productos p on dv.idProducto = p.id where dv.idVenta = @idVenta");
                datos.SetearParametro("@idVenta", idVenta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();
                    producto.id = (int)datos.Lector["id"];
                    producto.nombre = (string)datos.Lector["nombre"];
                    producto.descripcion = (string)datos.Lector["descripcion"];
                    producto.precio = (decimal)datos.Lector["precio"];
                    producto.margenGanancia = (decimal)datos.Lector["margenGanancia"];
                    producto.Cantidad = (int)datos.Lector["cantidad"];
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

       

        public List<Venta> ListarVentasPendientes()
        {


            List<Venta> ventas = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("select v.id, v.fecha, v.idUsuario, U.nombre, U.apellido, U.dni, U.email, U.telefono, U.direccion, v.enLocal, E.nombre as nombreestadoventa, v.idEstadoVenta from ventas v inner join Usuarios U on v.idUsuario = U.id INNER JOIN EstadoVenta E on v.idEstadoVenta = E.id where v.idEstadoVenta = 1");
                datos.EjecutarLectura();


                while (datos.Lector.Read())
                {



                    Venta aux = new Venta();
                    aux.IdVenta = (int)datos.Lector["id"];
                    aux.Fecha = (DateTime)datos.Lector["fecha"];
                    aux.Cliente = new Cliente();
                    aux.Cliente.Id = (int)datos.Lector["id"];
                    aux.Cliente.Nombre = (string)datos.Lector["nombre"];
                    aux.Cliente.Apellido = (string)datos.Lector["apellido"];
                    aux.Cliente.Dni = (string)datos.Lector["dni"];
                    aux.Cliente.Email = (string)datos.Lector["email"];
                    aux.Cliente.Telefono = (string)datos.Lector["telefono"];
                    aux.Cliente.Direccion = (string)datos.Lector["direccion"];
                    aux.EnLocal = (bool)datos.Lector["enLocal"];
                    aux.idEstadoVenta = (int)datos.Lector["idEstadoVenta"];
                    aux.nombreEstadoVenta = (string)datos.Lector["nombreestadoventa"];
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
                    venta.Cliente = new Cliente
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
            Venta venta = new Venta();
            Cliente cliente = new Cliente();

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT fecha, idUsuario, U.nombre FROM Ventas v INNER JOIN Usuarios U ON V.idUsuario = U.id WHERE v.id = @idVenta");
                datos.SetearParametro("@idVenta", idVenta);
                
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    
                    venta.Fecha = (DateTime)datos.Lector["fecha"];
                    cliente.Id = (int)datos.Lector["idUsuario"];
                    cliente.Nombre = (string)datos.Lector["nombre"];
                    venta.Cliente = cliente;





                }
                return venta;
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
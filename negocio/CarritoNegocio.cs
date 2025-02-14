using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using dominio;
using System.Net.Mail;



namespace negocio
{
    public class CarritoNegocio
    {

        AccesoDatos datos = new AccesoDatos();

        // aca tiene que estar el metodo que inserta una venta que recibe una lista de productos desde el Carrpito
        // 

        public bool InsertarVenta(List<Producto> carrito, decimal totalGeneral, int idCliente)
        {

            // hay  stock sufuciente ??
            // se derifica que haya sstock suficiente para la venta


            int idVenta;

            // Insertar en la tabla Ventas
            AccesoDatos datosVenta = new AccesoDatos();
            try
            {
               // datosVenta.Conexion.Open();
                datosVenta.SetearConsulta("INSERT INTO Ventas (fecha, idUsuario, monto) OUTPUT INSERTED.id VALUES (@fecha, @idUsuario, @monto)");
                datosVenta.Comando.Parameters.Clear();
                datosVenta.SetearParametro("@fecha", DateTime.Now);
                datosVenta.SetearParametro("@idUsuario", idCliente); // Cambiar por el ID del usuario actual
                datosVenta.SetearParametro("@monto", totalGeneral);
                idVenta = (int)datosVenta.EjecutarEscalar();

                // aca se debe descontar del stock la cantidad de productos vendidos








            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la venta", ex);
            }
            finally
            {
                datosVenta.CerrarConexion();
            }

            return InsertarDetalleVenta(carrito, idVenta);
        }

        public bool InsertarDetalleVenta(List<Producto> carrito, int idVenta)
        {
            // Insertar en la tabla DetalleVentas
            AccesoDatos datosDetalleVenta = new AccesoDatos();
            try
            {
                datosDetalleVenta.Conexion.Open();

                foreach (var producto in carrito)
                {
                    datosDetalleVenta.SetearConsulta("INSERT INTO DetalleVentas (idVenta, idProducto, cantidad, precioVenta) VALUES (@idVenta, @idProducto, @cantidad, @precioVenta)");
                    datosDetalleVenta.Comando.Parameters.Clear();
                    datosDetalleVenta.SetearParametro("@idVenta", idVenta);
                    datosDetalleVenta.SetearParametro("@idProducto", producto.id);
                    datosDetalleVenta.SetearParametro("@cantidad", producto.Cantidad);
                    datosDetalleVenta.SetearParametro("@precioVenta", producto.precio);
                    datosDetalleVenta.EjecutarAccion();

                    DescontarStocK(producto.id, producto.Cantidad);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el detalle de la venta", ex);
            }
            finally
            {
                datosDetalleVenta.CerrarConexion();
            }
        }



        public bool DescontarStocK(int idProducto, int CantidadVendida)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Stock SET cantidad = cantidad - @cantidadV WHERE idProducto = @id");
                datos.Comando.Parameters.Clear();
                datos.SetearParametro("@id", idProducto);
                datos.SetearParametro("@cantidadV", CantidadVendida);
                datos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al descontar el stock", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }


        }

        public void Devolverstock(int id, int cantidad)
        {

            // aca se tiene que volver a insertar el stock que se descontó cuando el cliente confimo la compra en la pagina carrito
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Stock SET cantidad = cantidad + @cantidad WHERE idProducto = @id");
                datos.Comando.Parameters.Clear();
                datos.SetearParametro("@id", id);
                datos.SetearParametro("@cantidad", cantidad);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al devolver el stock", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }

        }
    }
}
    








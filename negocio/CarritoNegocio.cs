using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using dominio;



namespace negocio
{
    public class CarritoNegocio
    {

        AccesoDatos datos = new AccesoDatos();

        // aca tiene que estar el metodo que inserta una venta que recibe una lista de productos desde el Carrpito
        // 

        public bool InsertarVenta(List<Producto> carrito, decimal totalGeneral)
        {
            int idVenta;

            // Insertar en la tabla Ventas
            AccesoDatos datosVenta = new AccesoDatos();
            try
            {
                datosVenta.Conexion.Open();
                datosVenta.SetearConsulta("INSERT INTO Ventas (fecha, idUsuario, monto) OUTPUT INSERTED.id VALUES (@fecha, @idUsuario, @monto)");
                datosVenta.Comando.Parameters.Clear();
                datosVenta.SetearParametro("@fecha", DateTime.Now);
                datosVenta.SetearParametro("@idUsuario", 1); // Cambiar por el ID del usuario actual
                datosVenta.SetearParametro("@monto", totalGeneral);
                idVenta = (int)datosVenta.EjecutarEscalar();
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


    }
}
    








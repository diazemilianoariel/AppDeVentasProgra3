using System;
using System.Collections.Generic;
using System.Linq;
using dominio;

namespace negocio
{
    public class CarritoNegocio
    {
        public int ProcesarVenta(List<Producto> carrito, decimal totalGeneral, int idCliente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Abrimos la conexión e iniciamos la transacción UNA SOLA VEZ.
                datos.AbrirConexion();
                datos.IniciarTransaccion();

                // 1. Insertar la cabecera de la Venta y obtener el nuevo ID
                string consultaVenta = "INSERT INTO Ventas (fecha, idUsuario, monto) OUTPUT INSERTED.id VALUES (GETDATE(), @idUsuario, @monto)";
                datos.SetearConsulta(consultaVenta);
                datos.LimpiarParametros();
                datos.SetearParametro("@idUsuario", idCliente);
                datos.SetearParametro("@monto", totalGeneral);
                int idVenta = (int)datos.EjecutarEscalar();

                // 2. Recorrer el carrito para insertar el detalle y descontar el stock
                foreach (var producto in carrito)
                {
                    // 2a. Insertar en DetalleVentas
                    string consultaDetalle = "INSERT INTO DetalleVentas (idVenta, idProducto, cantidad, precioVenta) VALUES (@idVenta, @idProducto, @cantidad, @precioVenta)";
                    datos.SetearConsulta(consultaDetalle);
                    datos.LimpiarParametros();
                    datos.SetearParametro("@idVenta", idVenta);
                    datos.SetearParametro("@idProducto", producto.id);
                    datos.SetearParametro("@cantidad", producto.Cantidad);
                    datos.SetearParametro("@precioVenta", producto.precioVenta);
                    datos.EjecutarAccion();

                    // 2b. Descontar del Stock
                    string consultaStock = "UPDATE Stock SET cantidad = cantidad - @cantidad WHERE idProducto = @idProducto";
                    datos.SetearConsulta(consultaStock);
                    datos.LimpiarParametros();
                    datos.SetearParametro("@cantidad", producto.Cantidad);
                    datos.SetearParametro("@idProducto", producto.id);
                    datos.EjecutarAccion();
                }

                // 3. Si todo salió bien, confirmamos la transacción.
                datos.ConfirmarTransaccion();
                return idVenta; // Devolvemos el ID de la venta por si lo necesitamos.
            }
            catch (Exception ex)
            {
                // Si algo falló en cualquiera de los pasos, revertimos todo y lanzamos la excepción.
                datos.RevertirTransaccion();
                throw ex;
            }
            finally
            {
                // Cerramos la conexión al final de todo, sin importar si fue éxito o error.
                datos.CerrarConexion();
            }
        }

        // Este método ahora es para cuando un admin CANCELA una venta y debe devolver el stock.
        public void DevolverStock(List<Producto> productos)
        {
            // Este proceso también debería ser transaccional.
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.AbrirConexion();
                datos.IniciarTransaccion();

                foreach (var producto in productos)
                {
                    datos.SetearConsulta("UPDATE Stock SET cantidad = cantidad + @cantidad WHERE idProducto = @idProducto");
                    datos.LimpiarParametros();
                    datos.SetearParametro("@cantidad", producto.Cantidad);
                    datos.SetearParametro("@idProducto", producto.id);
                    datos.EjecutarAccion();
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
    }
}
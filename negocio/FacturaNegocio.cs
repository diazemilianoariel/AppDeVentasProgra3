using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace negocio
{
    public class FacturaNegocio
    {

        public CompraResumen ObtenerCompraPorId(int idVenta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"SELECT V.id as IdVenta, V.monto as TotalFactura, V.fecha as Fecha, E.nombre as Estado
                            FROM Ventas V
                            INNER JOIN EstadoVenta E ON V.idEstadoVenta = E.id
                            WHERE V.id = @idVenta";
                datos.SetearConsulta(consulta);
                datos.SetearParametro("@idVenta", idVenta);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    CompraResumen compra = new CompraResumen();
                    compra.IdVenta = (int)datos.Lector["IdVenta"];
                    compra.TotalFactura = (decimal)datos.Lector["TotalFactura"];
                    compra.Fecha = (DateTime)datos.Lector["Fecha"];
                    compra.Estado = (string)datos.Lector["Estado"];
                    return compra;
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


        
        public List<Producto> ObtenerDetalleVenta(int idVenta)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"SELECT P.nombre, DV.cantidad, DV.precioVenta 
                          FROM DetalleVentas DV
                          INNER JOIN Productos P ON P.id = DV.idProducto
                          WHERE DV.idVenta = @idVenta";
                datos.SetearConsulta(consulta);
                datos.SetearParametro("@idVenta", idVenta);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.Cantidad = (int)datos.Lector["cantidad"]; 
                    aux.precio = (decimal)datos.Lector["precioVenta"];
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


        public List<CompraResumen> ListarComprasPorCliente(int idCliente)
        {
            List<CompraResumen> lista = new List<CompraResumen>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
               
                string consulta = @"SELECT
                              V.id as IdVenta,
                              V.monto as TotalFactura,
                              V.fecha as Fecha,
                              E.nombre as Estado
                          FROM Ventas V
                          INNER JOIN EstadoVenta E ON V.idEstadoVenta = E.id
                          WHERE V.idUsuario = @idCliente";

                datos.SetearConsulta(consulta);
                datos.SetearParametro("@idCliente", idCliente);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                   
                    CompraResumen aux = new CompraResumen();
                    aux.IdVenta = (int)datos.Lector["IdVenta"];
                    aux.TotalFactura = (decimal)datos.Lector["TotalFactura"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Estado = (string)datos.Lector["Estado"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las compras del cliente.", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public List<Factura> ListarFacturas(int idCliente)
        {
            List<Factura> lista = new List<Factura>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT Facturas.id, idVenta, total , Facturas.fecha from Facturas INNER JOIN Ventas ON Facturas.idVenta = Ventas.id WHERE Ventas.idUsuario = @idCliente");
                datos.SetearParametro("@idCliente", idCliente);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Factura aux = new Factura();
                    aux.Id = (int)datos.Lector["id"];
                    aux.IdVenta = (int)datos.Lector["idVenta"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.TotalFactura = (decimal)datos.Lector["Total"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las facturas", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }

        }

        public void GenerarFactura(dominio.Factura factura)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Facturas (idVenta, Fecha, Total) VALUES (@idVenta, GETDATE(),@TotalFactura)");
                datos.SetearParametro("@idVenta", factura.IdVenta);
                datos.SetearParametro("@TotalFactura", factura.TotalFactura);
                
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la Factura", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }

        }

            


    }
}
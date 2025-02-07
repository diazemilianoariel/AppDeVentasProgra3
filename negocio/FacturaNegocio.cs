using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace negocio
{
    public class FacturaNegocio
    {

        // generar una factura insercion 


        // metodo para listar facturas

        public List<Factura> ListarFacturas(int idCliente)
        {
            List<Factura> lista = new List<Factura>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT Facturas.id, idVenta, total , Facturas.fecha from Facturas INNER JOIN Ventas ON Facturas.idVenta = Ventas.id WHERE Ventas.idUsuario = @idC");
                datos.SetearParametro("idC", idCliente);
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
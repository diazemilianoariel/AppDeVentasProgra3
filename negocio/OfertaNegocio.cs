using dominio;
using System;
using System.Collections.Generic;

namespace negocio
{
    public class OfertaNegocio
    {
        // Método para la PÁGINA PÚBLICA de ofertas.
        // Muestra solo las ofertas activas por fecha.
        public List<Producto> ListarOfertasActivas()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
                    SELECT P.id, P.nombre, P.imagen, P.precio, P.margenGanancia,
                           ISNULL(S.cantidad, 0) as Stock, O.precioOferta
                    FROM Productos P
                    INNER JOIN Ofertas O ON P.id = O.idProducto
                    LEFT JOIN Stock S ON P.id = S.idProducto
                    WHERE P.estado = 1 
                      AND (O.FechaInicio IS NULL OR O.FechaInicio <= GETDATE())
                      AND (O.FechaFin IS NULL OR O.FechaFin >= GETDATE())";

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.stock = (int)datos.Lector["Stock"];
                    aux.Imagen = (string)datos.Lector["imagen"];

                    // Lógica de precio inteligente
                    if (datos.Lector["precioOferta"] != DBNull.Value)
                    {
                        aux.precioVenta = (decimal)datos.Lector["precioOferta"];
                    }
                    else
                    {
                        aux.precio = (decimal)datos.Lector["precio"];
                        aux.margenGanancia = (decimal)datos.Lector["margenGanancia"];
                        aux.CalcularPrecioVenta();
                    }
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }
        }

        // Método para el PANEL DE GESTIÓN del admin.
        // Trae TODOS los productos y nos dice si están o no en oferta.
        public List<Producto> ListarTodosParaGestion()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
                    SELECT P.id, P.nombre, O.idProducto as IdOferta, O.precioOferta, O.FechaInicio, O.FechaFin
                    FROM Productos P
                    LEFT JOIN Ofertas O ON P.id = O.idProducto
                    WHERE P.estado = 1";
                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    // Propiedades extra para la grilla de gestión
                    aux.EnOferta = datos.Lector["IdOferta"] != DBNull.Value;
                    if (datos.Lector["precioOferta"] != DBNull.Value)
                        aux.PrecioOferta = (decimal)datos.Lector["precioOferta"];
                    if (datos.Lector["FechaInicio"] != DBNull.Value)
                        aux.FechaInicioOferta = (DateTime)datos.Lector["FechaInicio"];
                    if (datos.Lector["FechaFin"] != DBNull.Value)
                        aux.FechaFinOferta = (DateTime)datos.Lector["FechaFin"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }
        }

        // Método para GUARDAR los cambios desde el panel de gestión.
        public void GuardarOfertas(List<Oferta> ofertas)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.AbrirConexion();
                datos.IniciarTransaccion();
                // 1. Borramos todas las ofertas existentes para empezar de cero.
                datos.SetearConsulta("DELETE FROM Ofertas");
                datos.EjecutarAccion();

                // 2. Insertamos las nuevas ofertas seleccionadas.
                foreach (var oferta in ofertas)
                {
                    datos.SetearConsulta("INSERT INTO Ofertas (idProducto, precioOferta, FechaInicio, FechaFin) VALUES (@id, @precio, @inicio, @fin)");
                    datos.LimpiarParametros();
                    datos.SetearParametro("@id", oferta.IdProducto);
                    datos.SetearParametro("@precio", (object)oferta.PrecioOferta ?? DBNull.Value);
                    datos.SetearParametro("@inicio", (object)oferta.FechaInicio ?? DBNull.Value);
                    datos.SetearParametro("@fin", (object)oferta.FechaFin ?? DBNull.Value);
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
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

        public void RegistrarVenta(Venta venta)
        {



        }

        public List<Venta> ListarVentas()
        {
            List<Venta> ventas = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("select v.id, v.fecha, v.monto, v.idUsuario, U.nombre, U.apellido, U.dni, U.email, U.telefono, U.direccion, v.enLocal, v.idEstadoVenta from ventas v inner join Usuarios U on v.idUsuario = U.id");
                datos.EjecutarLectura();


                while (datos.Lector.Read())
                {
                    Venta aux = new Venta();
                    aux.IdVenta = (int)datos.Lector["id"];
                    aux.Fecha = (DateTime)datos.Lector["fecha"];
                    aux.Monto = (decimal)datos.Lector["monto"];

                    aux.Cliente = new Cliente();
                    aux.Cliente.Id = (int)datos.Lector["idCliente"];
                    aux.Cliente.Nombre = (string)datos.Lector["nombre"];
                    aux.Cliente.Apellido = (string)datos.Lector["apellido"];
                    aux.Cliente.Dni = (string)datos.Lector["dni"];
                    aux.Cliente.Email = (string)datos.Lector["email"];
                    aux.Cliente.Telefono = (string)datos.Lector["telefono"];
                    aux.Cliente.Direccion = (string)datos.Lector["direccion"];
                    aux.EnLocal = (bool)datos.Lector["enLocal"];
                    aux.idEstadoVenta = (int)datos.Lector["idEstadoVenta"];
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


        public void ActualizarEstadoVenta(int idVenta, int nuevoEstado)
        {
            
        }
    }
}
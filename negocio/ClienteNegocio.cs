using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> ListarClientes()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select U.id, U.nombre, U.apellido, U.dni, U.direccion, U.telefono, U.email, p.nombre as Perfil from Usuarios U INNER JOIN Perfiles P on U.idPerfil = P.id where U.estado = 1");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Direccion = (string)datos.Lector["Direccion"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.nombrePerfil = (string)datos.Lector["Perfil"];


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





        public Cliente ObtenerCliente(int id)
        {
            Cliente aux = new Cliente();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select Id, Nombre, apellido, dni, direccion, telefono, email from Clientes where Id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Direccion = (string)datos.Lector["Direccion"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Email = (string)datos.Lector["Email"];
                    return aux;
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



        public void AgregarCliente(Cliente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("insert into Usuarios (Nombre, Apellido, Dni, Direccion, Telefono, Email, Clave, idPerfil) values (@Nombre, @Apellido, @Dni, @Direccion, @Telefono, @Email, @Clave, @idPerfil)");
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Apellido", nuevo.Apellido);
                datos.SetearParametro("@Dni", nuevo.Dni);
                datos.SetearParametro("@Direccion", nuevo.Direccion);
                datos.SetearParametro("@Telefono", nuevo.Telefono);
                datos.SetearParametro("@Email", nuevo.Email);
                datos.SetearParametro("@Clave", nuevo.clave);
                datos.SetearParametro("@idPerfil", nuevo.idPerfil);
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


        public void ModificarCliente(Cliente modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update Clientes set Nombre = @Nombre, Apellido = @Apellido, Dni = @Dni, Direccion = @Direccion, Telefono = @Telefono, Email = @Email where Id = @Id");
                datos.SetearParametro("@Nombre", modificado.Nombre);
                datos.SetearParametro("@Apellido", modificado.Apellido);
                datos.SetearParametro("@Dni", modificado.Dni);
                datos.SetearParametro("@Direccion", modificado.Direccion);
                datos.SetearParametro("@Telefono", modificado.Telefono);
                datos.SetearParametro("@Email", modificado.Email);
                datos.SetearParametro("@Id", modificado.Id);
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



        public void EliminarCliente(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update Clientes SET estado = 0 where Id = @Id");
                datos.SetearParametro("@Id", id);
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



        //  selecciona al cliente con el  mismo dni y  estado en 0
        public Cliente ExisteClienteInactivo(string dni)
        {
            Cliente aux = null;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select Id, Nombre, Apellido, Dni, Direccion, Telefono, Email from Usuarios where Dni = @Dni and estado = 0");
                datos.SetearParametro("@Dni", dni);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    aux = new Cliente();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Direccion = (string)datos.Lector["Direccion"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Email = (string)datos.Lector["Email"];
                }
                return aux;
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


        // cambia el estado a 1 
        public void ActivarCliente(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update Clientes set estado = 1 where Id = @Id");
                datos.SetearParametro("@Id", id);
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








    }
}
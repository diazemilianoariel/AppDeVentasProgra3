using System;
using System.Collections.Generic;
using dominio;


namespace negocio
{
    public class ClienteNegocio
    {


        public List<Cliente> ListarClientes()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();
            {
                try
                {
                    datos.SetearConsulta("select U.id, U.Nombre, U.apellido, U.dni, U.direccion, U.telefono, U.email, U.clave, P.nombre as Perfil from Usuarios U INNER JOIN Perfiles P on U.idPerfil = P.id where U.estado = 1");
                    datos.EjecutarLectura();
                    while (datos.Lector.Read())
                    {
                        Cliente aux = new Cliente
                        {
                            Id = (int)datos.Lector["id"],
                            Nombre = (string)datos.Lector["Nombre"],
                            Apellido = (string)datos.Lector["apellido"],
                            Dni = (string)datos.Lector["dni"],
                            Direccion = (string)datos.Lector["direccion"],
                            Telefono = (string)datos.Lector["telefono"],
                            Email = (string)datos.Lector["email"],
                            clave = (string)datos.Lector["clave"],
                            nombrePerfil = (string)datos.Lector["Perfil"]
                        };
                        lista.Add(aux);
                    }
                    return lista;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar clientes", ex);
                }
            }
        }




        public Cliente ObtenerCliente(int id)
        {
            Cliente aux = new Cliente();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select Id, Nombre, apellido, dni, direccion, telefono, email, clave from Usuarios where Id = @id");
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
                    aux.clave = (string)datos.Lector["clave"];
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

        public Cliente ObtenerClientePorEmail(string Email)
        {
            Cliente aux = new Cliente();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select U.Id, U.Nombre, U.Apellido, U.Dni, U.Direccion, U.Telefono, U.Email, U.clave, U.idPerfil, P.Nombre as nombrePerfil from Usuarios U " +
                             "inner join Perfiles P on U.idPerfil = P.Id where U.Email = @Email and U.estado = 1");
                datos.SetearParametro("@Email", Email);
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
                    aux.clave = (string)datos.Lector["clave"];
                    aux.idPerfil = (int)datos.Lector["idPerfil"];
                    aux.nombrePerfil = (string)datos.Lector["nombrePerfil"];
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
                datos.SetearParametro("@estado", 1);
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


            if (string.IsNullOrEmpty(modificado.clave))
            {
                throw new ArgumentException("La clave no puede estar vacía.");
            }




            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update Usuarios set Nombre = @Nombre, Apellido = @Apellido, Dni = @Dni, Direccion = @Direccion, Telefono = @Telefono, Email = @Email, Clave = @Clave, idPerfil = @idPerfil where Id = @Id");
                datos.SetearParametro("@Nombre", modificado.Nombre);
                datos.SetearParametro("@Apellido", modificado.Apellido);
                datos.SetearParametro("@Dni", modificado.Dni);
                datos.SetearParametro("@Direccion", modificado.Direccion);
                datos.SetearParametro("@Telefono", modificado.Telefono);
                datos.SetearParametro("@Email", modificado.Email);
                datos.SetearParametro("@Clave", modificado.clave);
                datos.SetearParametro("@idPerfil", modificado.idPerfil);
                datos.SetearParametro("@Id", modificado.Id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar cliente", ex);
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
                datos.SetearConsulta("update Usuarios SET estado = 0 where Id = @Id");
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
                datos.SetearConsulta("select Id, Nombre, Apellido, Dni, Direccion, Telefono, Email, clave from Usuarios where Dni = @Dni and estado = 0");
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
                    aux.clave = (string)datos.Lector["clave"];
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


        public List<Perfil> ListarPerfiles()
        {
            List<Perfil> lista = new List<Perfil>();
            AccesoDatos datos = new AccesoDatos();
            {
                try
                {
                    datos.SetearConsulta("select id, Nombre, estado  from Perfiles  where estado = 1");
                    datos.EjecutarLectura();
                    while (datos.Lector.Read())
                    {
                        Perfil aux = new Perfil
                        {
                            Id = (int)datos.Lector["id"],
                            Nombre = (string)datos.Lector["Nombre"],
                            Estado = (bool)datos.Lector["estado"],


                        };
                        lista.Add(aux);
                    }
                    return lista;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar Perfils", ex);
                }
            }
        }



        // validar usuario 
        public bool Loguear(Cliente cliente)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.SetearConsulta("select Id, Nombre, Apellido, Dni, Direccion, Telefono, Email, idPerfil from Usuarios where Email = @Email and Clave = @Clave and estado = 1");
                datos.SetearParametro("@Email", cliente.Email);
                datos.SetearParametro("@Clave", cliente.clave);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    cliente.Id = (int)datos.Lector["Id"];
                    cliente.Nombre = (string)datos.Lector["Nombre"];
                    cliente.Apellido = (string)datos.Lector["Apellido"];
                    cliente.Dni = (string)datos.Lector["Dni"];
                    cliente.Direccion = (string)datos.Lector["Direccion"];
                    cliente.Telefono = (string)datos.Lector["Telefono"];

                    // 1 = cliente
                    // 2 = administrador 
                    // 3 = vendedor 
                    // 4 = soporte 

                    cliente.idPerfil = (int)datos.Lector["idPerfil"];

                    if (cliente.idPerfil == 1)
                    {
                        cliente.nombrePerfil = "Cliente";
                    }
                    if (cliente.idPerfil == 2)
                    {
                        cliente.nombrePerfil = "Administrador";
                    }
                    if (cliente.idPerfil == 3)
                    {
                        cliente.nombrePerfil = "Vendedor";
                    }
                    if (cliente.idPerfil == 4)
                    {
                        cliente.nombrePerfil = "Soporte";
                    }

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            // datos cerrados
            finally
            {
                AccesoDatos datos = new AccesoDatos();
                datos.CerrarConexion();
            }




        }



        public int CantidadUsuarios()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select count(*) as cantidad from Usuarios");
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["cantidad"];
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


        public string RecuperarContraseña(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select Clave from Usuarios where Email = @Email");
                datos.SetearParametro("@Email", email);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    return (string)datos.Lector["Clave"];
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






    }
}
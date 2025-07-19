using System;
using System.Collections.Generic;
using dominio;


namespace negocio
{
    public class UsuarioNegocio
    {


        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            {
                try
                {
                    datos.SetearConsulta("select U.id, U.Nombre, U.apellido, U.dni, U.direccion, U.telefono, U.email, U.clave, P.nombre as Perfil, U.estado from Usuarios U INNER JOIN Perfiles P on U.idPerfil = P.id where U.estado = 1");
                    datos.EjecutarLectura();
                    while (datos.Lector.Read())
                    {
                        Usuario aux = new Usuario();

                        aux.Id = (int)datos.Lector["Id"];
                        aux.Nombre = (string)datos.Lector["Nombre"];
                        aux.Apellido = (string)datos.Lector["Apellido"];
                        aux.Dni = (string)datos.Lector["Dni"];
                        aux.Direccion = (string)datos.Lector["Direccion"];
                        aux.Telefono = (string)datos.Lector["Telefono"];
                        aux.Email = (string)datos.Lector["Email"];
                        aux.clave = (string)datos.Lector["clave"];
                        aux.Perfil = (Perfil)datos.Lector["Perfil"];
                        aux.estado = (bool)datos.Lector["estado"];

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


        public Usuario ObtenerUsuario(int id)
        {
            Usuario aux = new Usuario();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select U.Id, U.Nombre, U.apellido, U.dni, U.direccion, U.telefono, U.email, U.clave, U.idPerfil, P.nombre as 'Perfil', U.estado from Usuarios U INNER JOIN Perfiles p on U.idPerfil = p.id where U.id = @id");
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
                    aux.Perfil = (Perfil)datos.Lector["idPerfil"];
                    aux.Perfil = (Perfil)datos.Lector["Perfil"];
                    aux.estado = (bool)datos.Lector["estado"];
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

        public Usuario ObtenerUsuarioPorEmail(string Email)
        {
            Usuario aux = new Usuario();
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
                    aux.Perfil = (Perfil)datos.Lector["idPerfil"];
                    aux.Perfil = (Perfil)datos.Lector["nombrePerfil"];
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



        public void AgregarUsuarios(Usuario nuevo)
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
                datos.SetearParametro("@idPerfil", nuevo.Perfil);
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


        public void ModificarUsuarios(Usuario modificado)
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
                datos.SetearParametro("@idPerfil", modificado.Perfil);
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



        public void EliminarUsuario(int id)
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
        public Usuario ExisteUsuarioInactivo(string dni)
        {
            Usuario aux = null;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select Id, Nombre, Apellido, Dni, Direccion, Telefono, Email, clave from Usuarios where Dni = @Dni and estado = 0");
                datos.SetearParametro("@Dni", dni);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    aux = new Usuario();
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
        public void ActivarUsuario(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update Usuario set estado = 1 where Id = @Id");
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

        // metodo para obtener perdil por id 
        public Perfil ObtenerPerfil(int idPerfil)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select id, Nombre, estado from Perfiles where id = @id");
                datos.SetearParametro("@id", idPerfil);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    return new Perfil
                    {
                        Id = (int)datos.Lector["id"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Estado = (bool)datos.Lector["estado"]
                    };
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


        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
              
                datos.SetearConsulta("SELECT Id FROM Usuarios WHERE Email = @Email AND Clave = @Clave AND estado = 1");
                datos.SetearParametro("@Email", usuario.Email);
                datos.SetearParametro("@Clave", usuario.clave);
                datos.EjecutarLectura();

                // Si el lector puede leer una fila, significa que el login es correcto.
                if (datos.Lector.Read())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Se cierra la conexión del objeto 'datos' usado en el try.
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
                datos.SetearConsulta("select Clave from Usuarios where Email = @Email AND estado = 1");
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
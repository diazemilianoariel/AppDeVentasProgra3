using System;
using System.Collections.Generic;
using dominio;


namespace negocio
{
    public class UsuarioNegocio
    {


        public List<Usuario> ListarUsuarios(string filtro = "")
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
            SELECT U.id, U.nombre, U.apellido, U.dni, U.direccion, U.telefono, U.email, U.estado, P.nombre as PerfilNombre 
            FROM Usuarios U 
            LEFT JOIN Perfiles P ON U.idPerfil = P.id";

                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += " WHERE U.nombre LIKE @filtro OR U.apellido LIKE @filtro OR U.email LIKE @filtro";
                    datos.SetearParametro("@filtro", "%" + filtro + "%");
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Nombre = (string)datos.Lector["nombre"];
                    aux.Apellido = (string)datos.Lector["apellido"];
                    aux.Dni = (string)datos.Lector["dni"];
                    aux.Direccion = (string)datos.Lector["direccion"];
                    aux.Telefono = (string)datos.Lector["telefono"];
                    aux.Email = (string)datos.Lector["email"];
                    aux.estado = (bool)datos.Lector["estado"];

                    if (datos.Lector["PerfilNombre"] != DBNull.Value)
                        aux.Perfil = new Perfil { Nombre = (string)datos.Lector["PerfilNombre"] };
                    else
                        aux.Perfil = new Perfil { Nombre = "Sin Perfil" };

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


        public Usuario ObtenerUsuarioPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT U.Id, U.Nombre, U.apellido, U.dni, U.direccion, U.telefono, U.email, U.clave, P.id as IdPerfil, P.nombre as NombrePerfil, U.estado FROM Usuarios U INNER JOIN Perfiles P ON U.idPerfil = P.id WHERE U.id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
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
                    aux.estado = (bool)datos.Lector["estado"];

                    aux.Perfil = new Perfil
                    {
                        Id = (int)datos.Lector["IdPerfil"],
                        Nombre = (string)datos.Lector["NombrePerfil"]
                    };
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
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT U.Id, U.Nombre, U.Apellido, U.Dni, U.Direccion, U.Telefono, U.Email, U.clave, P.id as IdPerfil, P.Nombre as NombrePerfil FROM Usuarios U INNER JOIN Perfiles P ON U.idPerfil = P.Id WHERE U.Email = @Email AND U.estado = 1");
                datos.SetearParametro("@Email", Email);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
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

                    aux.Perfil = new Perfil
                    {
                        Id = (int)datos.Lector["IdPerfil"],
                        Nombre = (string)datos.Lector["NombrePerfil"]
                    };

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



        public void AgregarUsuario(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Usuarios (Nombre, Apellido, Dni, Direccion, Telefono, Email, Clave, idPerfil) VALUES (@Nombre, @Apellido, @Dni, @Direccion, @Telefono, @Email, @Clave, @idPerfil)");
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Apellido", nuevo.Apellido);
                datos.SetearParametro("@Dni", nuevo.Dni);
                datos.SetearParametro("@Direccion", nuevo.Direccion);
                datos.SetearParametro("@Telefono", nuevo.Telefono);
                datos.SetearParametro("@Email", nuevo.Email);
                datos.SetearParametro("@Clave", nuevo.clave);
                // CORRECCIÓN: Se pasa el ID del perfil, no el objeto completo.
                datos.SetearParametro("@idPerfil", nuevo.Perfil.Id);
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


        public void ModificarUsuario(Usuario modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET Nombre = @Nombre, Apellido = @Apellido, Dni = @Dni, Direccion = @Direccion, Telefono = @Telefono, Email = @Email, Clave = @Clave, idPerfil = @idPerfil WHERE Id = @Id");
                datos.SetearParametro("@Nombre", modificado.Nombre);
                datos.SetearParametro("@Apellido", modificado.Apellido);
                datos.SetearParametro("@Dni", modificado.Dni);
                datos.SetearParametro("@Direccion", modificado.Direccion);
                datos.SetearParametro("@Telefono", modificado.Telefono);
                datos.SetearParametro("@Email", modificado.Email);
                datos.SetearParametro("@Clave", modificado.clave);
                // CORRECCIÓN: Se pasa el ID del perfil, no el objeto completo.
                datos.SetearParametro("@idPerfil", modificado.Perfil.Id);
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



        public void BajaLogicaUsuario(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET estado = 0 WHERE Id = @Id");
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
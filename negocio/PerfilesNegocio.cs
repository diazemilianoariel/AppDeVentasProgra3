using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dominio;

namespace negocio
{
    public class PerfilesNegocio

    {


        // metodo existe perfil

        public bool ExistePerfil(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select id, nombre, estado from Perfiles where nombre = @nombre");
                datos.SetearParametro("@nombre", nombre);
                datos.EjecutarLectura();
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
                datos.CerrarConexion();
            }
        }



        public List<Perfil> ListarPerfiles(string filtro = "")
        {
            List<Perfil> lista = new List<Perfil>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT id, nombre, estado FROM Perfiles WHERE estado = 1";

                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += " AND nombre LIKE @filtro";
                    datos.SetearParametro("@filtro", "%" + filtro + "%");
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Perfil perfil = new Perfil();
                    perfil.Id = (int)datos.Lector["id"];
                    perfil.Nombre = (string)datos.Lector["nombre"];
                    perfil.Estado = (bool)datos.Lector["estado"];
                    lista.Add(perfil);
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

        public void AgregarPerfil(Perfil nuevo)
        {

            AccesoDatos datos = new AccesoDatos();

            // verificamos que el perfil a ingresar no este en la base de datos o este inactivo



            try
            {

                datos.SetearConsulta("insert into Perfiles (nombre, estado) values (@nombre, @estado)");
                datos.SetearParametro("@nombre", nuevo.Nombre);
                datos.SetearParametro("@estado", 1); // lo pongo en 1 porque se asume que al agregar un perfil, este se encuentra activo

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


        public void ModificarPerfil(Perfil perfil)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update Perfiles set nombre = @nombre, estado = @estado where id = @id");
                datos.SetearParametro("@nombre", perfil.Nombre);
                datos.SetearParametro("@estado", perfil.Estado);
                datos.SetearParametro("@id", perfil.Id);
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


        public void BajaLogicaPerfiles(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update Perfiles set estado = 0 where id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AltaLogicaPerfiles(Perfil perfil)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update Perfiles set estado = 1 where id = @id");
                datos.SetearParametro("@id", perfil.Id);
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

        // obtener perfil por ID
        public Perfil ObtenerPerfil(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select id, nombre, estado from Perfiles where id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                Perfil perfil = new Perfil();
                if (datos.Lector.Read())
                {
                    perfil.Id = (int)datos.Lector["id"];
                    perfil.Nombre = (string)datos.Lector["nombre"];
                    perfil.Estado = (bool)datos.Lector["estado"];
                    return perfil;
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


        // esto es para verificar si el perfil ya existe en la base de datos


        public Perfil ObtenerPerfilPoreNombre(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select id, nombre, estado from Perfiles where nombre = @nombre");
                datos.SetearParametro("@nombre", nombre);
                datos.EjecutarLectura();
                Perfil perfil = new Perfil();
                if (datos.Lector.Read())
                {
                    perfil.Id = (int)datos.Lector["id"];
                    perfil.Nombre = (string)datos.Lector["nombre"];
                    perfil.Estado = (bool)datos.Lector["estado"];
                    return perfil;
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

        public Perfil ExistePerfilInactivo(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select id, nombre, estado from Perfiles where nombre = @nombre and estado = 0");
                datos.SetearParametro("@nombre", nombre);
                datos.EjecutarLectura();
                Perfil perfil = new Perfil();
                if (datos.Lector.Read())
                {
                    perfil.Id = (int)datos.Lector["id"];
                    perfil.Nombre = (string)datos.Lector["nombre"];
                    perfil.Estado = (bool)datos.Lector["estado"];
                    return perfil;
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

        public void EliminarPerfil (Perfil perfil)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("delete from Perfiles where id = @id");
                datos.SetearParametro("@id", perfil.Id);
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
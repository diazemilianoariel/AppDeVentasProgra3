using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using dominio;
using negocio;


namespace negocio
{
    public class TipoNegocio
    {
        public List<Tipos> ListarTipos(string filtro = "")
        {
            List<Tipos> tipos = new List<Tipos>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT id, nombre, estado FROM Tipos WHERE estado = 1";

                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += " AND nombre LIKE @filtro";
                    datos.SetearParametro("@filtro", "%" + filtro + "%");
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();


                while (datos.Lector.Read())
                {
                    Tipos aux = new Tipos();
                    aux.Id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.estado = (bool)datos.Lector["estado"];
                    tipos.Add(aux);
                }
                return tipos;
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

        public Tipos BuscarTipo(int id)
        {
            Tipos aux = new Tipos();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select id, nombre, estado from Tipos where id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    aux.Id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.estado = (bool)datos.Lector["estado"];
                    return aux;
                }
                else
                {
                    return null;
                }
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

        public void AgregarTipo(Tipos tipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("insert into Tipos (nombre) values (@nombre)");
                datos.SetearParametro("@nombre", tipo.nombre);
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

        public void ActualizarTipo(Tipos tipo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update Tipos set nombre = @nombre, estado = @estado where id = @id");
                datos.SetearParametro("@nombre", tipo.nombre);
                datos.SetearParametro("@estado", tipo.estado);
                datos.SetearParametro("@id", tipo.Id);
                
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

        public void bajaLogica(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update Tipos set estado = 0 where id = @id");
                datos.SetearParametro("@id", id);
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



        public void bajaFisica(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("delete from Tipos where id = @id");
                datos.SetearParametro("@id", id);
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

        public void altaLogica(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update Tipos set estado = 1 where nombre = @nombre");
                datos.SetearParametro("@nombre", nombre);
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

        public bool buscarTipoNombre(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select id, nombre from Tipos where nombre = @nombre");
                datos.SetearParametro("@nombre", nombre);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

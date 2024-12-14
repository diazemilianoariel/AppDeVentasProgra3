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
    public class CategoriaNegocio
    {



        public List<Categoria> ListarCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select id, nombre from Categorias where estado = 1");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    categorias.Add(aux);
                }
                return categorias;
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

        public Categoria BuscarCategoria(int id)
        {
            Categoria aux = new Categoria();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select id, nombre from Categorias where id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
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

        public void AgregarCategoria(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("insert into Categorias (nombre) values (@nombre)");
                datos.SetearParametro("@nombre", categoria.nombre);
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

        public void ActualizarCategoria(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update Categorias set nombre = @nombre where id = @id");
                datos.SetearParametro("@nombre", categoria.nombre);
                datos.SetearParametro("@id", categoria.id);
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
                datos.SetearConsulta("update Categorias set estado = 0 where id = @id");
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
                datos.SetearConsulta("update Categorias set estado = 1 where nombre = @nombre");
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

        public bool buscarCategoriaNombre(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select id, nombre from Categorias where nombre = @nombre");
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


        public bool ExisteCategoria(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select id, nombre from Categorias where nombre = @nombre");
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
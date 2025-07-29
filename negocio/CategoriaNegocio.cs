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



        public List<Categoria> ListarCategorias(string filtro = "")
        {
            List<Categoria> categorias = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT id, nombre, estado FROM Categorias WHERE estado = 1";

                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += " AND nombre LIKE @filtro";
                    datos.SetearParametro("@filtro", "%" + filtro + "%");
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.estado = (bool)datos.Lector["estado"];

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
                datos.SetearConsulta("select id, nombre, estado from Categorias where id = @id");
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


        public void AgregarCategoria(Categoria categoria)
        {
            // Verificación de duplicados ANTES de insertar
            Categoria existente = VerificarCategoria(categoria.nombre);
            if (existente != null)
            {
                if (existente.estado) // Si está ACTIVA
                {
                    throw new Exception("Ya existe una categoría activa con ese nombre.");
                }
                else // Si está INACTIVA
                {
                    throw new CategoriaInactivaException("Categoría inactiva encontrada.", existente);
                }
            }

            // Si no existe, la crea
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Categorias (nombre, estado) VALUES (@nombre, @estado)");
                datos.SetearParametro("@nombre", categoria.nombre);
                datos.SetearParametro("@estado", categoria.estado);
                datos.EjecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }
        }


        public void ActualizarCategoria(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update Categorias set nombre = @nombre, estado = @estado where id = @id");
                datos.SetearParametro("@nombre", categoria.nombre);
                datos.SetearParametro("@estado", categoria.estado);
                datos.SetearParametro("@id", categoria.Id);
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


        public void bajaFisica(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("delete from Categorias where id = @id");
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




        public Tuple<List<string>, List<int>> CantidadesPorCategoria()
        {
            List<string> categorias = new List<string>();
            List<int> cantidades = new List<int>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT  C.nombre as nombre, (COUNT(P.idCategoria))  as cantidad FROM Productos P RIGHT JOIN Categorias C ON P.idCategoria = C.id GROUP BY C.nombre");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    categorias.Add((string)datos.Lector["nombre"]);
                    cantidades.Add((int)datos.Lector["cantidad"]);
                }
                return Tuple.Create(categorias, cantidades);
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

        
        public Categoria VerificarCategoria(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT id, nombre, estado FROM Categorias WHERE nombre = @nombre");
                datos.SetearParametro("@nombre", nombre);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.estado = (bool)datos.Lector["estado"];
                    return aux; // Devuelve la categoría encontrada (activa o inactiva)
                }
                return null; // No existe
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }
        }



        public void ReactivarCategoria(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Categorias SET estado = 1 WHERE id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }
        }


        public class CategoriaInactivaException : Exception
        {
            public Categoria CategoriaExistente { get; set; }

            public CategoriaInactivaException(string mensaje, Categoria categoria) : base(mensaje)
            {
                this.CategoriaExistente = categoria;
            }
        }




    }
}
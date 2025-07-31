using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class MarcaNegocio
    {


        public List<Marca> ListarMarcas(string filtro = "")
        {
            List<Marca> marcas = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT id, nombre, estado FROM Marcas WHERE estado = 1";

                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += " AND nombre LIKE @filtro";
                    datos.SetearParametro("@filtro", "%" + filtro + "%");
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();




                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();

                    aux.Id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.estado = (bool)datos.Lector["estado"];

                    marcas.Add(aux);
                }
                return marcas;
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

        public Marca BuscarMarca(int id)
        {
            Marca aux = new Marca();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select id, nombre, estado from Marcas where id = @id");
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

        public Marca VerificarMarca(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT id, nombre, estado FROM Marcas WHERE nombre = @nombre");
                datos.SetearParametro("@nombre", nombre);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.estado = (bool)datos.Lector["estado"];
                    return aux; // Devuelve la marca encontrada (activa o inactiva)
                }
                return null; // No existe
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }
        }


        public void AgregarMarca(Marca marca)
        {
            Marca existente = VerificarMarca(marca.nombre);
            if (existente != null)
            {
                if (existente.estado) // Si está ACTIVA
                {
                    throw new Exception("Ya existe una marca activa con ese nombre.");
                }
                else // Si está INACTIVA
                {
                    throw new MarcaInactivaException("Marca inactiva encontrada.", existente);
                }
            }

            // Si no existe, la crea
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Marcas (nombre, estado) VALUES (@nombre, @estado)");
                datos.SetearParametro("@nombre", marca.nombre);
                datos.SetearParametro("@estado", marca.estado);
                datos.EjecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }
        }


        public void ActualizarMarca(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update Marcas set nombre = @nombre, estado = @estado where id = @id");
                datos.SetearParametro("@nombre", marca.nombre);
                datos.SetearParametro("@estado", marca.estado);
                datos.SetearParametro("@id", marca.Id);
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
                datos.SetearConsulta("update Marcas set estado = 0 where id = @id");
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
                datos.SetearConsulta("update Marcas set estado = 1 where nombre = @nombre");
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

        public void ReactivarMarca(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Marcas SET estado = 1 WHERE id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            // Omitimos el finally si EjecutarAccion ya gestiona la conexión
        }

        public bool ExisteMarca(string nombreActual)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM Marcas WHERE nombre = @nombre");
                datos.SetearParametro("@nombre", nombreActual);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    return (int)datos.Lector[0] > 0; // Devuelve true si hay al menos una marca con ese nombre
                }
                return false; // No existe ninguna marca con ese nombre
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



        public class MarcaInactivaException : Exception
        {
            public Marca MarcaExistente { get; set; }

            public MarcaInactivaException(string mensaje, Marca marca) : base(mensaje)
            {
                this.MarcaExistente = marca;
            }
        }

    }







}

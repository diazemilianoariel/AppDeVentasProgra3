using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dominio;
using negocio;


namespace negocio
{
    public class ProveedoresNegocio
    {
        public List<Proveedor> ListarProveedores(string filtro = "")
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                string consulta = "SELECT id, nombre, direccion, telefono, email, estado FROM Proveedores WHERE estado = 1";

                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += " AND (nombre LIKE @filtro OR email LIKE @filtro)";
                    accesoDatos.SetearParametro("@filtro", "%" + filtro + "%");
                }

                accesoDatos.SetearConsulta(consulta);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Proveedor proveedor = new Proveedor();


                    proveedor.Id = (int)accesoDatos.Lector["id"];
                    proveedor.Nombre = (String)accesoDatos.Lector["nombre"];
                    proveedor.Direccion = (String)accesoDatos.Lector["direccion"];
                    proveedor.Telefono = (String)accesoDatos.Lector["telefono"];
                    proveedor.Email = (String)accesoDatos.Lector["email"];
                    proveedor.estado = (bool)accesoDatos.Lector["estado"];
                    lista.Add(proveedor);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }


        public Proveedor ObtenerProveedor(int id)
        {

            AccesoDatos accesoDatos = new AccesoDatos();
            Proveedor proveedor = new Proveedor();
            try
            {
                accesoDatos.SetearConsulta("SELECT id, nombre, direccion, telefono, email,estado FROM Proveedores WHERE id = @id");
                accesoDatos.SetearParametro("@id", id);
                accesoDatos.EjecutarLectura();

                if (accesoDatos.Lector.Read())
                {
                    proveedor.Id = accesoDatos.Lector.GetInt32(0);
                    proveedor.Nombre = accesoDatos.Lector.GetString(1);
                    proveedor.Direccion = accesoDatos.Lector.GetString(2);
                    proveedor.Telefono = accesoDatos.Lector.GetString(3);
                    proveedor.Email = accesoDatos.Lector.GetString(4);
                    proveedor.estado = accesoDatos.Lector.GetBoolean(5);
                }

                return proveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }

        public void AgregarProveedor(Proveedor proveedor)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearConsulta("INSERT INTO Proveedores (nombre, direccion, telefono, email) VALUES (@nombre, @direccion, @telefono, @correo)");
                accesoDatos.SetearParametro("@nombre", proveedor.Nombre);
                accesoDatos.SetearParametro("@direccion", proveedor.Direccion);
                accesoDatos.SetearParametro("@telefono", proveedor.Telefono);
                accesoDatos.SetearParametro("@correo", proveedor.Email);
                accesoDatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }

        public void ModificarProveedor(Proveedor proveedor)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {

                accesoDatos.SetearConsulta("UPDATE Proveedores SET nombre = @nombre, direccion = @direccion, telefono = @telefono, email = @correo, estado = @estado WHERE id = @id");
                accesoDatos.SetearParametro("@nombre", proveedor.Nombre);
                accesoDatos.SetearParametro("@direccion", proveedor.Direccion);
                accesoDatos.SetearParametro("@telefono", proveedor.Telefono);
                accesoDatos.SetearParametro("@correo", proveedor.Email);
                accesoDatos.SetearParametro("@estado", proveedor.estado);
                accesoDatos.SetearParametro("@id", proveedor.Id);
                accesoDatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }


        }

        public void EliminarProveedor(int id)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearConsulta("UPDATE Proveedores SET estado = 0 WHERE id = @id");
                accesoDatos.SetearParametro("@id", id);
                accesoDatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }


        public void ActivarProveedor(int id)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearConsulta("UPDATE Proveedores SET estado = 1 WHERE id = @id");
                accesoDatos.SetearParametro("@id", id);
                accesoDatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }


        }


        public Proveedor ObtenerProveedorPorEmail(string email)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            Proveedor proveedor = null;
            try
            {
                accesoDatos.SetearConsulta("SELECT id, nombre, direccion, telefono, email, estado FROM Proveedores WHERE email = @Email");
                accesoDatos.SetearParametro("@Email", email);
                accesoDatos.EjecutarLectura();

                if (accesoDatos.Lector.Read())
                {
                    proveedor = new Proveedor
                    {
                        Id = accesoDatos.Lector.GetInt32(0),
                        Nombre = accesoDatos.Lector.GetString(1),
                        Direccion = accesoDatos.Lector.GetString(2),
                        Telefono = accesoDatos.Lector.GetString(3),
                        Email = accesoDatos.Lector.GetString(4),
                        estado = accesoDatos.Lector.GetBoolean(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }

            return proveedor;
        }

    }
}

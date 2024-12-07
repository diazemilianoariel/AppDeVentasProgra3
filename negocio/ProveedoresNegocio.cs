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
        public List<Proveedor> ListarProveedores()
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearConsulta("SELECT id, nombre, direccion, telefono, email FROM Proveedores where estado = 1");
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Proveedor proveedor = new Proveedor();


                    proveedor.id = (int)accesoDatos.Lector["id"];
                    proveedor.Nombre =  accesoDatos.Lector["Nombre"].ToString();
                    proveedor.Direccion = accesoDatos.Lector["Direccion"].ToString();
                    proveedor.Telefono = accesoDatos.Lector["Telefono"].ToString();
                    proveedor.Email = accesoDatos.Lector["Email"].ToString();

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
                accesoDatos.SetearConsulta("SELECT id, nombre, direccion, telefono, email FROM Proveedores WHERE id = @id");
                accesoDatos.SetearParametro("@id", id);
                accesoDatos.EjecutarLectura();

                if (accesoDatos.Lector.Read())
                {
                    proveedor.id = accesoDatos.Lector.GetInt32(0);
                    proveedor.Nombre = accesoDatos.Lector.GetString(1);
                    proveedor.Direccion = accesoDatos.Lector.GetString(2);
                    proveedor.Telefono = accesoDatos.Lector.GetString(3);
                    proveedor.Email = accesoDatos.Lector.GetString(4);
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

                accesoDatos.SetearConsulta("UPDATE Proveedores SET nombre = @nombre, direccion = @direccion, telefono = @telefono, email = @correo WHERE id = @id");
                accesoDatos.SetearParametro("@id", proveedor.id);
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

     
    }
}

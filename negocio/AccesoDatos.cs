using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;



namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;




        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public SqlConnection Conexion
        {
            get { return conexion; }
        }

        public SqlCommand Comando
        {
            get { return comando; }
        }



        //defino el constructor
        public AccesoDatos()
        {
            // Configura la cadena de conexión para usar UTF-8
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=TiendaOnline; integrated security=true");
            comando = new SqlCommand();
        }


        public void SetearConsulta(string query)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
        }

        public void SetearProcedimiento(string sp)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
        }

        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                }
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EjecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        
     

        public void SetearParametro(string parametro, object valor)
        {
            if (valor is string)
            {
                comando.Parameters.Add(new SqlParameter(parametro, SqlDbType.NVarChar) { Value = valor });
            }
            else
            {
                comando.Parameters.AddWithValue(parametro, valor);
            }
        }

      

   

        // para un registro recien inserttado 
        public object EjecutarEscalar()
        {
            comando.Connection = conexion;
            try
            {
                if (conexion.State != System.Data.ConnectionState.Open)
                {
                    conexion.Open();
                }
                return comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

      


        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();
            if (conexion.State == ConnectionState.Open)
            {
                conexion.Close();
            }
        }


        private SqlTransaction transaccion;


        // 2. Añade estos NUEVOS MÉTODOS a tu clase (puedes ponerlos después del constructor)

        public void AbrirConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
                conexion.Open();
        }

        public void IniciarTransaccion()
        {
            transaccion = conexion.BeginTransaction();
            comando.Transaction = transaccion;
        }

        public void ConfirmarTransaccion()
        {
            // El '?' es un 'null-conditional operator'. Solo ejecuta Commit() si transaccion no es null.
            transaccion?.Commit();
        }

        public void RevertirTransaccion()
        {
            transaccion?.Rollback();
        }

        public void LimpiarParametros()
        {
            comando.Parameters.Clear();
        }


    }


}

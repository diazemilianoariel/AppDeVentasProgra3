using System;
using System.Data;
using System.Data.SqlClient;

namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        private SqlTransaction transaccion;

        public SqlDataReader Lector => lector;

        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=TiendaOnline; integrated security=true");
            comando = new SqlCommand();
        }

        // --- MÉTODOS DE TRANSACCIONES ---
        public void AbrirConexion()
        {
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();
        }

        public void IniciarTransaccion()
        {
            // Solo inicia una transacción si la conexión está abierta
            if (conexion.State == ConnectionState.Open)
            {
                transaccion = conexion.BeginTransaction();
                comando.Transaction = transaccion;
            }
        }

        public void ConfirmarTransaccion()
        {
            transaccion?.Commit();
            transaccion = null; // Limpiamos la transacción
        }

        public void RevertirTransaccion()
        {
            transaccion?.Rollback();
            transaccion = null; // Limpiamos la transacción
        }

        public void LimpiarParametros()
        {
            comando.Parameters.Clear();
        }

        // --- MÉTODOS DE EJECUCIÓN ---
        public void SetearConsulta(string query)
        {
            comando.CommandType = CommandType.Text;
            comando.CommandText = query;
            comando.Connection = this.conexion; // Aseguramos siempre la conexión
            if (this.transaccion != null)
                comando.Transaction = this.transaccion; // Y la transacción si existe
        }

        public void EjecutarLectura()
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EjecutarAccion()
        {
            // Este método ahora es consciente de las transacciones.
            // No abre ni cierra la conexión por sí mismo si hay una transacción activa.
            comando.Connection = conexion;
            if (transaccion == null) // Si NO hay transacción, maneja su propia conexión.
            {
                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
                finally
                {
                    conexion.Close();
                }
            }
            else // Si HAY transacción, simplemente ejecuta.
            {
                comando.ExecuteNonQuery();
            }
        }

        public object EjecutarEscalar()
        {
            // Este método ahora es consciente de las transacciones.
            comando.Connection = conexion;
            if (transaccion == null) // Si NO hay transacción
            {
                try
                {
                    conexion.Open();
                    return comando.ExecuteScalar();
                }
                finally
                {
                    conexion.Close();
                }
            }
            else // Si HAY transacción
            {
                return comando.ExecuteScalar();
            }
        }

        public void SetearParametro(string parametro, object valor)
        {
            comando.Parameters.AddWithValue(parametro, valor);
        }

        public void CerrarConexion()
        {
            lector?.Close();
            if (conexion.State == ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}
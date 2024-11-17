using System;
using System.Collections.Generic;
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


        //defino el constructor
        public AccesoDatos()
        {
            //VER CONEXION SERVER...
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
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
     

        public void SetearParametro(string parametro, object valor)
        {
            comando.Parameters.AddWithValue(parametro, valor);
        }
        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }
    }


}

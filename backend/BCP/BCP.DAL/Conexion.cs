using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BCP.DAL
{
    public class Conexion
    {
        //singleton
        private static Conexion conexion = null;
        private SqlConnection conn;

        private Conexion()
        {
            conn = new SqlConnection("Data Source=VALE\\LOCALHOST;Initial Catalog=BCP;Integrated Security=True");

        }

        public static Conexion getConexion()
        {
            if(conexion == null)
            {
                conexion = new Conexion();
            }
            return conexion;
        }

        public SqlConnection getConn()
        {
            return conn;
        }

        public void cerrarConexion()
        {
            conexion = null;
        }
    }
}

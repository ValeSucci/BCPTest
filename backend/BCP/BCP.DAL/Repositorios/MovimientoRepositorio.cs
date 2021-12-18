using BCP.DAL.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BCP.DAL.Repositorios
{
    public class MovimientoRepositorio : IMovimientoRepositorio
    {
        private Conexion conexion;
        private SqlCommand comando;

        public MovimientoRepositorio()
        {
            conexion = Conexion.getConexion();
        }

        public  async Task<List<Movimiento>> obtenerMovimientos(string nroCuenta)
        {
            List<Movimiento> listaMovimientos = new List<Movimiento>();
            string query = "EXEC ObtenerMovimientos @NRO_CUENTA = '"+nroCuenta+"'";

            try
            {
                comando = new SqlCommand(query, conexion.getConn());
                conexion.getConn().Open();
                SqlDataReader read = await comando.ExecuteReaderAsync();
                while (read.Read())
                {
                    Movimiento movimiento = new Movimiento(
                        read[0].ToString(),
                        read[2].ToString(),
                        Convert.ToDecimal(read[3].ToString())
                    );
                    movimiento.fecha = Convert.ToDateTime(read[1].ToString());

                    listaMovimientos.Add(movimiento);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.getConn().Close();
                conexion.cerrarConexion();
            }

            return listaMovimientos;

        }
    }
}

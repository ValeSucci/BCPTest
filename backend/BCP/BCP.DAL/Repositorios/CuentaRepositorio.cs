using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BCP.DAL.Modelos;

namespace BCP.DAL.Repositorios
{
    public class CuentaRepositorio : ICuentaRepositorio
    {
        private Conexion conexion;
        private SqlCommand comando;

        public CuentaRepositorio()
        {
            conexion = Conexion.getConexion();
        }

        public async Task<int> crearCuenta(Cuenta cuenta)
        {
            int success = 0;
            string query = "EXEC CrearCuenta @NRO_CUENTA = '"+cuenta.nroCuenta+ "', @TIPO = '" + cuenta.tipo + "', @MONEDA = '" + cuenta.moneda + "', @NOMBRE = '" + cuenta.nombre + "'";
            try
            {
                comando = new SqlCommand(query, conexion.getConn());
                conexion.getConn().Open();
                await comando.ExecuteNonQueryAsync();
                success = 1;
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
            return success;
        }

        public async Task<int> depositarORetirar(Movimiento movimiento)
        {
            int success = 0;
            string query = "EXEC DepositarORetirar @NRO_CUENTA = '" + movimiento.nroCuenta + "', @TIPO = '" + movimiento.tipo + "', @IMPORTE = '" + movimiento.importe + "'";
            try
            {
                comando = new SqlCommand(query, conexion.getConn());
                conexion.getConn().Open();
                await comando.ExecuteNonQueryAsync();
                success = 1;
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
            return success;
        }

        public async Task<int> transferir(Cuenta cuentaO, Cuenta cuentaD, decimal monto)
        {
            int success = 0;
            string query = "EXEC Transferir @NRO_CUENTA_ORIGEN = '" + cuentaO.nroCuenta + "', @NRO_CUENTA_DESTINO = '" + cuentaD.nroCuenta + "', @MONTO = '" + monto + "'";
            try
            {
                comando = new SqlCommand(query, conexion.getConn());
                conexion.getConn().Open();
                await comando.ExecuteNonQueryAsync();
                success = 1;
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
            return success;
        }

        public async Task<Cuenta> obtenerCuenta(Cuenta cuenta)
        {
 
            string query = "EXEC EncontrarCuenta @NRO_CUENTA = '" + cuenta.nroCuenta + "'";

            try
            {
                comando = new SqlCommand(query, conexion.getConn());
                conexion.getConn().Open();
                SqlDataReader read = await comando.ExecuteReaderAsync();
                if (read.Read())
                {
                    cuenta.nroCuenta = read[0].ToString();
                    cuenta.tipo = read[1].ToString();
                    cuenta.moneda = read[2].ToString();
                    cuenta.nombre = read[3].ToString();
                    cuenta.saldo = Convert.ToDecimal(read[4].ToString());

                    cuenta.estado = 99;
                } else
                {
                    cuenta.estado = 1; //no cuentas
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

            return cuenta;
        }

        public async Task<List<Cuenta>> obtenerCuentas()
        {
            List<Cuenta> listaCuentas = new List<Cuenta>();
            string query = "EXEC ObtenerCuentas";

            try
            {
                comando = new SqlCommand(query, conexion.getConn());
                conexion.getConn().Open();
                SqlDataReader read = await comando.ExecuteReaderAsync();
                while (read.Read())
                {
                    Cuenta cuenta = new Cuenta(
                        read[0].ToString(),
                        read[1].ToString(),
                        read[2].ToString(),
                        read[3].ToString()
                    );
                    cuenta.saldo = Convert.ToDecimal(read[4].ToString());

                    listaCuentas.Add(cuenta);
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

            return listaCuentas;
        }

    }
}

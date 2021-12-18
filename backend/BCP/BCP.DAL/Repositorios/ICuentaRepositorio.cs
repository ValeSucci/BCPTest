using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCP.DAL.Modelos;

namespace BCP.DAL.Repositorios
{
    interface ICuentaRepositorio
    {
        Task<int> crearCuenta(Cuenta cuenta);

        Task<int> depositarORetirar(Movimiento movimiento);

        Task<int> transferir(Cuenta cuentaO, Cuenta cuentaD, decimal monto);

        Task<Cuenta> obtenerCuenta(Cuenta cuenta);

        Task<List<Cuenta>> obtenerCuentas();
    }
}

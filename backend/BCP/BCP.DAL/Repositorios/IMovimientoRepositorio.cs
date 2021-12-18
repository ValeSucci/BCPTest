using BCP.DAL.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.DAL.Repositorios
{
    interface IMovimientoRepositorio
    {
        Task<List<Movimiento>> obtenerMovimientos(string nroCuenta);
    }
}

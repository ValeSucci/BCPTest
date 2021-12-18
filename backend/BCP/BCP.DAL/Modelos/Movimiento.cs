using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.DAL.Modelos
{
    public class Movimiento
    {
        public string nroCuenta { get; set; }
        public DateTime fecha { get; set; }
        public string tipo { get; set; }
        public decimal importe { get; set; }

        public Movimiento(string nroCuenta, string tipo, decimal importe)
        {
            this.nroCuenta = nroCuenta;
            this.tipo = tipo;
            this.importe = importe;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.BL.DTOs
{
    public class CuentaDto
    {
        public string nroCuenta { get; set; }
        public string tipo { get; set; }
        public string moneda { get; set; }
        public string nombre { get; set; }
        public decimal saldo { get; set; }
        //public int estado { get; set; }

    }
}

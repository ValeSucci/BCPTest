using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.BL.DTOs
{
    public class MovimientoDto
    {
        public string nroCuenta { get; set; }
        public DateTime fecha { get; set; }
        public string tipo { get; set; }
        public decimal importe { get; set; }

    }
}

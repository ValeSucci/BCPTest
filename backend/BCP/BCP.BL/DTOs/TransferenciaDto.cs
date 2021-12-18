using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.BL.DTOs
{
    public class TransferenciaDto
    {
        public CuentaDto cuentaO { get; set; }
        public CuentaDto cuentaD { get; set; }
        public decimal monto { get; set; }


    }
}

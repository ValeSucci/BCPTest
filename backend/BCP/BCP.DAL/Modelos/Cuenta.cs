using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.DAL.Modelos
{
    public class Cuenta
    {
        //atributos
        public string nroCuenta { get; set; }
        public string tipo { get; set; }
        public string moneda { get; set; }
        public string nombre { get; set; }
        public decimal saldo { get; set; }


        //para control de errores
        public int estado { get; set; }

        //constructor
        public Cuenta()
        {
        }

        public Cuenta(string nroCuenta, string tipo, string moneda, string nombre)
        {
            this.nroCuenta = nroCuenta;
            this.tipo = tipo;
            this.moneda = moneda;
            this.nombre = nombre;
            this.saldo = 0;
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BCP.BL;
using BCP.BL.DTOs;
using System.Threading.Tasks;


namespace BCP.API.Controllers
{
    public class CuentasController : ApiController
    {
        private readonly CuentaBL cuentaBL;

        public CuentasController()
        {
            cuentaBL = new CuentaBL();
        }


        // GET: api/Cuentas
        [HttpGet]
        public async Task<IHttpActionResult> ObtenerTodas()
        {
            var cuentas = await cuentaBL.obtenerCuentas();
            return Ok(cuentas);
        }

        // GET: api/Cuenta
        [HttpGet]
        public async Task<IHttpActionResult> Obtener(string id)
        {
            CuentaDto cuentaDtoAux = new CuentaDto();
            cuentaDtoAux.nroCuenta = id;
            var cuenta = await cuentaBL.obtenerCuenta(cuentaDtoAux);
            return Ok(cuenta);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CrearCuenta(CuentaDto cuentaDto)
        {
            var res = await cuentaBL.crearCuenta(cuentaDto);
            if(res.ToString() == "1")
            {
                return Ok(res);
            } else
            {
                return BadRequest(res.ToString());
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> DepositarORetirar(MovimientoDto movimientoDto)
        {
            var res = await cuentaBL.depositarORetirar(movimientoDto);
            if (res.ToString() == "1")
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res.ToString());
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Transferir(TransferenciaDto transferenciaDto)
        {
            var res = await cuentaBL.transferir(transferenciaDto.cuentaO,transferenciaDto.cuentaD,transferenciaDto.monto);
            if (res.ToString() == "1")
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res.ToString());
            }
        }

    }
}

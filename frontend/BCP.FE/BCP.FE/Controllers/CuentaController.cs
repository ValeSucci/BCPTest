using BCP.BL.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BCP.FE.Controllers
{
    public class CuentaController : Controller
    {
        string api = "https://localhost:44379/api/";

        // GET: Cuentas
        public async Task<ActionResult> Cuentas()
        {
            //cuentas/ObtenerTodas
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(api + "cuentas/ObtenerTodas");
            var cuentas = JsonConvert.DeserializeObject<List<CuentaDto>>(json);
            return View(cuentas);

        }

        // GET: Cuentas
        public async Task<ActionResult> Movimientos(string id)
        {
            //cuentas/ObtenerTodas
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(api + "movimientos/obtener/"+id);
            var movimientos = JsonConvert.DeserializeObject<List<MovimientoDto>>(json);


            var json2 = await httpClient.GetStringAsync(api + "cuentas/obtener/" + id);
            var cuenta = JsonConvert.DeserializeObject<CuentaDto>(json2);

            ViewData["nro"] = cuenta.nroCuenta;
            ViewData["nombre"] = cuenta.nombre;
            ViewData["saldo"] = cuenta.saldo;
            ViewData["moneda"] = cuenta.moneda;

            return View(movimientos);

        }



        public ActionResult CrearCuenta()
        {
            return View();
        }

        // Post: Cuenta
        [HttpPost]
        public async Task<ActionResult> CrearCuenta(CuentaDto cuentaDto)
        {
            var aux = 200;
            var httpClient = new HttpClient();
            var res = await httpClient.PostAsync(api + "cuentas/CrearCuenta", new StringContent(
                new JavaScriptSerializer().Serialize(cuentaDto), Encoding.UTF8, "application/json"));
            aux = Convert.ToInt32(res.StatusCode);
            if (aux != 200)
            {
                //error
                ViewBag.Alert = "Ocurrió un error, intente nuevamente";
            }
            return View();
        }



        public async Task<ActionResult> DepositarORetirar()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(api + "cuentas/ObtenerTodas");
            var cuentas = JsonConvert.DeserializeObject<List<CuentaDto>>(json);
            return View(cuentas);
        }

        // Post: Cuenta
        [HttpPost]
        public async Task<ActionResult> DepositarORetirar(MovimientoDto movimientoDto, string submit)
        {
            switch (submit)
            {
                case "Deposito":
                    movimientoDto.tipo = "A";
                    break;
                case "Retiro":
                    movimientoDto.tipo = "D";
                    break;
                default:
                    break;
            }

            var aux = 200;
            var httpClient = new HttpClient();
            var res = await httpClient.PostAsync(api + "cuentas/DepositarORetirar", new StringContent(
                new JavaScriptSerializer().Serialize(movimientoDto), Encoding.UTF8, "application/json"));
            aux = Convert.ToInt32(res.StatusCode);
            if (aux != 200)
            {
                //error
                ViewBag.Alert = "Ocurrió un error, intente nuevamente";
            }

            var json = await httpClient.GetStringAsync(api + "cuentas/ObtenerTodas");
            var cuentas = JsonConvert.DeserializeObject<List<CuentaDto>>(json);
            return View(cuentas);
        }




        public async Task<ActionResult> Transferir()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(api + "cuentas/ObtenerTodas");
            var cuentas = JsonConvert.DeserializeObject<List<CuentaDto>>(json);

            return View(cuentas);
        }

        // Post: Cuenta
        [HttpPost]
        public async Task<ActionResult> Transferir(string nroCuentaO, string saldo, string nroCuentaD,string monto)
        {
            CuentaDto cuentaO = new CuentaDto();
            cuentaO.nroCuenta = nroCuentaO.Split('-')[0];
            cuentaO.saldo = Convert.ToDecimal(nroCuentaO.Split('-')[1]);
            CuentaDto cuentaD = new CuentaDto();
            cuentaD.nroCuenta = nroCuentaD;

            TransferenciaDto transferenciaDto = new TransferenciaDto();
            transferenciaDto.cuentaO = cuentaO;
            transferenciaDto.cuentaD = cuentaD;
            transferenciaDto.monto = Convert.ToDecimal(monto);

            var aux = 200;
            var httpClient = new HttpClient();
            var res = await httpClient.PostAsync(api + "cuentas/Transferir", new StringContent(
                new JavaScriptSerializer().Serialize(transferenciaDto), Encoding.UTF8, "application/json"));
            aux = Convert.ToInt32(res.StatusCode);
            if (aux != 200)
            {
                //error
                ViewBag.Alert = "Ocurrió un error, intente nuevamente";
            }

            var json = await httpClient.GetStringAsync(api + "cuentas/ObtenerTodas");
            var cuentas = JsonConvert.DeserializeObject<List<CuentaDto>>(json);
            return View(cuentas);
        }

    }
}
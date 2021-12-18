using BCP.BL.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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


        // GET: Cuenta
        public ActionResult CrearCuenta()
        {
            return View();
        }
    }
}
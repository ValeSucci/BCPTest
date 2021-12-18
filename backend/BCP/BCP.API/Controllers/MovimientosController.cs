using BCP.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BCP.API.Controllers
{
    public class MovimientosController : ApiController
    {
        private readonly MovimientoBL movimientoBL;

        public MovimientosController()
        {
            movimientoBL = new MovimientoBL();
        }


        // GET: api/Movimientos
        [HttpGet]
        public async Task<IHttpActionResult> Obtener(string id)
        {
            var movimientos = await movimientoBL.obtenerMovimientos(id);
            return Ok(movimientos);
        }

    }
}

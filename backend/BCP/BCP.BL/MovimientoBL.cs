using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCP.DAL.Repositorios;
using BCP.DAL.Modelos;
using BCP.BL.DTOs;
using AutoMapper;


namespace BCP.BL
{
    public class MovimientoBL
    {
        public MovimientoRepositorio movimientoRepositorio;
        private IMapper mapper;

        public MovimientoBL()
        {
            movimientoRepositorio = new MovimientoRepositorio();
            mapper = MapperConfig.MapperConfiguration().CreateMapper();
        }

        public async Task<List<MovimientoDto>> obtenerMovimientos(string nroCuenta)
        {
            List<Movimiento> listaMovimientos = await movimientoRepositorio.obtenerMovimientos(nroCuenta);
            return mapper.Map<List<MovimientoDto>>(listaMovimientos);
        }



    }
}

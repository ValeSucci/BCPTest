using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BCP.DAL.Modelos;

namespace BCP.BL.DTOs
{
    public class MapperConfig
    {
        public static MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cuenta, CuentaDto>();
                cfg.CreateMap<CuentaDto, Cuenta>();

                cfg.CreateMap<Movimiento, MovimientoDto>();
                cfg.CreateMap<MovimientoDto, Movimiento>();
            });
        }
    }
}

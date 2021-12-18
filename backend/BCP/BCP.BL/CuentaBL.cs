using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCP.DAL.Modelos;
using BCP.DAL.Repositorios;
using BCP.BL.DTOs;
using AutoMapper;

namespace BCP.BL
{
    public class CuentaBL
    {

        public CuentaRepositorio cuentaRepositorio;
        private IMapper mapper;

        public CuentaBL()
        {
            cuentaRepositorio = new CuentaRepositorio();
            mapper = MapperConfig.MapperConfiguration().CreateMapper();
        }

        public async Task<int> crearCuenta(CuentaDto cuentaDto)
        {
            Cuenta cuenta = mapper.Map<CuentaDto, Cuenta>(cuentaDto);
            //validar tipo (AHO/CTE)
            string tipo = cuenta.tipo;
            if(tipo == null)
            {
                cuenta.estado = 20;
                return 2;
            }
            else
            {
                tipo = cuenta.tipo.Trim();
                if (! (tipo == "AHO" || tipo == "CTE"))
                {
                    cuenta.estado = 2;
                    return 2;
                }
            }
            //validar largo nro_cuenta (14/13)
            string nroCuenta = cuenta.nroCuenta;
            if (nroCuenta == null)
            {
                cuenta.estado = 30;
                return 30;
            }
            else
            {
                nroCuenta = cuenta.nroCuenta.Trim();
                if (!(tipo=="AHO" && nroCuenta.Length==14 || tipo=="CTE" && nroCuenta.Length == 13))
                {
                    cuenta.estado = 3;
                    return 3;
                }
            }
            //validar moneda (BOL/USD)
            string moneda = cuenta.moneda;
            if (moneda == null)
            {
                cuenta.estado = 40;
                return 40;
            }
            else
            {
                moneda = cuenta.moneda.Trim();
                if (!(moneda == "BOL" || moneda == "USD"))
                {
                    cuenta.estado = 4;
                    return 4;
                }
            }

            //validar nombre (max 40)
            string nombre = cuenta.nombre;
            if (nombre == null)
            {
                cuenta.estado = 50;
                return 50;
            }
            else
            {
                nombre = cuenta.nombre.Trim();
                if (!(nombre.Length >= 0 && nombre.Length <= 40))
                {
                    cuenta.estado = 5;
                    return 5;
                }
            }

            //validar no duplicados
            Cuenta cuentaAux = new Cuenta();
            cuentaAux.nroCuenta = cuenta.nroCuenta;
            Cuenta res = await cuentaRepositorio.obtenerCuenta(cuentaAux);

            if (! (res.estado == 1))
            {
                cuenta.estado = 6;
                return 6;
            }

            cuenta.estado = 99;
            return await cuentaRepositorio.crearCuenta(cuenta);
        }

        public async Task<int> depositarORetirar(MovimientoDto movimientoDto)
        {
            Movimiento movimiento = mapper.Map<MovimientoDto, Movimiento>(movimientoDto);

            //validar que exista nro_cuenta
            Cuenta cuentaAux = new Cuenta();
            cuentaAux.nroCuenta = movimiento.nroCuenta;
            Cuenta cuenta = await cuentaRepositorio.obtenerCuenta(cuentaAux);
            if (cuenta.estado == 1)
            {
                //cuenta.estado = 2;
                return 2;
            }

            //validar tipo (D/A)
            string tipo = movimiento.tipo;
            if (tipo == null)
            {
                //cuenta.estado = 30;
                return 30;
            }
            else
            {
                tipo = movimiento.tipo.Trim();
                if (!(tipo == "A" || tipo == "D"))
                {
                    //cuenta.estado = 3;
                    return 3;
                }
            }

            //validar importe menor a saldo, si tipo=D
            string importeAux = movimiento.importe.ToString();
            decimal importe;
            if (importeAux == null)
            {
                //cuenta.estado = 40;
                return 40;
            }
            else
            {
                importe = Convert.ToDecimal(movimiento.importe);
                if (!(tipo == "A" || tipo =="D" && cuenta.saldo>=importe))
                {
                    //cuenta.estado = 4;
                    return 4;
                }
            }

            cuenta.estado = 99;
            return await cuentaRepositorio.depositarORetirar(movimiento);
        }

        public async Task<int> transferir(CuentaDto cuentaODto, CuentaDto cuentaDDto, decimal monto)
        {
            Cuenta cuentaO = mapper.Map<CuentaDto, Cuenta>(cuentaODto);
            Cuenta cuentaD = mapper.Map<CuentaDto, Cuenta>(cuentaDDto);

            //validar que exista nro_cuenta origen
            Cuenta cuentaOAux = new Cuenta();
            cuentaOAux.nroCuenta = cuentaO.nroCuenta;
            cuentaO  = await cuentaRepositorio.obtenerCuenta(cuentaOAux);
            if (cuentaO.estado == 1)
            {
                //cuenta.estado = 2;
                return 2;
            }

            //validar que exista nro_cuenta destino
            Cuenta cuentaDAux = new Cuenta();
            cuentaDAux.nroCuenta = cuentaD.nroCuenta;
            cuentaD = await cuentaRepositorio.obtenerCuenta(cuentaDAux);
            if (cuentaD.estado == 1)
            {
                //cuenta.estado = 3;
                return 3;
            }

            //validar que monto sea menor a saldo en origen
            if (!(cuentaO.saldo >= monto))
            {
                //cuenta.estado = 4;
                return 4;
            }


            cuentaO.estado = 99;
            cuentaD.estado = 99;
            return await cuentaRepositorio.transferir(cuentaO,cuentaD,monto);
        }

        public async Task<CuentaDto> obtenerCuenta(CuentaDto cuentaDto)
        {
            Cuenta cuenta = mapper.Map<CuentaDto, Cuenta>(cuentaDto);

            Cuenta cuentaAux = await cuentaRepositorio.obtenerCuenta(cuenta);

            return mapper.Map<CuentaDto>(cuentaAux);
        }

        public async Task<List<CuentaDto>> obtenerCuentas()
        {
            List<Cuenta> listaCuentas = await cuentaRepositorio.obtenerCuentas();
            return mapper.Map<List<CuentaDto>>(listaCuentas);
        }

    }
}

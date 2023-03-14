using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driven;
using Domain.Ports.Driving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Entities.Vistas;

namespace DomainServices.DomServ
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculosPatrullajeRepo _repo;
        private readonly IUsuariosConfiguradorQuery _user;

        public VehiculoService(IVehiculosPatrullajeRepo repo, IUsuariosConfiguradorQuery uc)
        {
            _repo = repo;
            _user = uc;
        }

        public async Task ActualizaAsync(VehiculoDtoForUpdate vehiculo, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                await _repo.ActualizaAsync(vehiculo.IdVehiculo, vehiculo.Matricula, vehiculo.NumeroEconomico, vehiculo.Habilitado, vehiculo.TipoPatrullaje, vehiculo.TipoVehiculo);
            }
        }

        public async Task AgregaAsync(VehiculoDtoForCreate vehiculo, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                await _repo.AgregaAsync( vehiculo.Matricula, vehiculo.NumeroEconomico, vehiculo.Habilitado, vehiculo.TipoPatrullaje, vehiculo.TipoVehiculo, vehiculo.Comandancia);
            }
        }

        public async Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorOpcionAsync(string opcion, int region, string criterio, string usuario)
        {
            var l = new List<VehiculoPatrullajeVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);
            string descripcion = "";

            if (opcion.Contains("-"))
            { 
                var chunks = opcion.Split('-');
                opcion = chunks[0];
                descripcion = chunks[1];
            }


            if (user != null)
            {
                switch (opcion)
                {
                    case "TODOS":
                        switch (criterio) 
                        {
                            case "":
                                l = await _repo.ObtenerVehiculosPorRegionAsync(region);
                                break;
                            default:
                                l = await _repo.ObtenerVehiculosPorRegionCriterioAsync(region, criterio);
                                break;
                        }
                        break;
                    case "AEREO":
                        switch (criterio)
                        {
                            case "":
                                l = await _repo.ObtenerVehiculosPorRegionParaPatrullajeAereoAsync(region);
                                break;
                            default:
                                l = await _repo.ObtenerVehiculosPorRegionCriterioParaPatrullajeAereoAsync(region, criterio);
                                break;
                        }
                        break;
                    case "TERRESTRE":
                        switch (criterio)
                        {
                            case "":
                                l = await _repo.ObtenerVehiculosPorRegionParaPatrullajeTerrestreAsync(region);
                                break;
                            default:
                                l = await _repo.ObtenerVehiculosPorRegionCriterioParaPatrullajeTerrestreAsync(region, criterio);
                                break;
                        }
                        break;
                    case "VEHICULOSPATRULLAJEEXTRAORDINARIO":

                        var idPropueta =  Int32.Parse(criterio);
                        l = await _repo.ObtenerVehiculosPatrullajeExtraordinarioPorDescripcionAsync(idPropueta, descripcion);

                        break;
                }
            }

            return l;
        }
    }
}

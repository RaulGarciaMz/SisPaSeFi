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
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;

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

        public async Task ActualizaAsync(VehiculoDtoForUpdate vehiculo)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(vehiculo.Usuario);

            if (user != null)
            {
                await _repo.ActualizaAsync(vehiculo.IdVehiculo, vehiculo.Matricula, vehiculo.NumeroEconomico, vehiculo.Habilitado, vehiculo.TipoPatrullaje, vehiculo.TipoVehiculo);
            }
        }

        public async Task AgregaAsync(VehiculoDtoForCreate vehiculo)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(vehiculo.Usuario);

            if (user != null)
            {
                var existe = await ExisteVehiculo(vehiculo);
                if (! existe )
                {
                    await _repo.AgregaAsync(vehiculo.Matricula, vehiculo.NumeroEconomico, vehiculo.Habilitado, vehiculo.TipoPatrullaje, vehiculo.TipoVehiculo, vehiculo.Comandancia);
                }                
            }
        }

        public async Task BorraPorOpcionAsync(string opcion, string dato, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                switch (opcion)
                {
                    case "EliminarVehiculoDePrograma":
                        
                        if (dato.IsNullOrEmpty() ||  ! dato.Contains("-")) return;

                        var datos = dato.Split("-");
                        var idPrograma = Int32.Parse(datos[0]);
                        var idVehiculo = Int32.Parse(datos[1]);

                        await _repo.BorraPorOpcionAsync(idPrograma, idVehiculo);
                        break;
                }
            }
        }
        public async Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorOpcionAsync(string opcion, int region, string? criterio, string usuario)
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
            if (criterio == null)
            {
                criterio = "";
            }
            else
            {
                criterio = "%" + criterio + "%";
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

                        var idPropuesta =  Int32.Parse(criterio);
                        l = await _repo.ObtenerVehiculosPatrullajeExtraordinarioPorDescripcionAsync(idPropuesta, descripcion);

                        break;
                    case "AEREOHABILITADOS":

                        switch (criterio)
                        {
                            case "":
                                l = await _repo.ObtenerVehiculosHabilitadosPorRegionAereoAsync(region);
                                break;
                            default:
                                l = await _repo.ObtenerVehiculosHabilitadosPorRegionCriterioAereoAsync(region, criterio);
                                break;
                        }
                        break;

                    case "TERRESTREHABILITADOS":

                        switch (criterio)
                        {
                            case "":
                                l = await _repo.ObtenerVehiculosHabilitadosPorRegionTerrestreAsync(region);
                                break;
                            default:
                                l = await _repo.ObtenerVehiculosHabilitadosPorRegionCriterioTerrestreAsync(region, criterio);
                                break;
                        }
                        break;
                }
            }

            return l;
        }

        private async Task<bool> ExisteVehiculo(VehiculoDtoForCreate vehiculo)
        {
            var vehis = await _repo.ObtenerNumeroDeVehiculosPorMatriculaAndComandanciaAsync(vehiculo.Matricula, vehiculo.Comandancia);

            if (vehis > 0) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}

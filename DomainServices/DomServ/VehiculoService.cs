using Domain.DTOs;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using Microsoft.IdentityModel.Tokens;

namespace DomainServices.DomServ
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculosPatrullajeRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public VehiculoService(IVehiculosPatrullajeRepo repo, IUsuariosParaValidacionQuery uc)
        {
            _repo = repo;
            _user = uc;
        }

        public async Task ActualizaAsync(VehiculoDtoForUpdate vehiculo)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(vehiculo.strUsuario);

            if (user != null)
            {
                await _repo.ActualizaAsync(vehiculo.intIdVehiculo, vehiculo.strMatricula, vehiculo.strNumeroEconomico, vehiculo.intHabilitado, vehiculo.intTipoPatrullaje, vehiculo.intTipoVehiculo);
            }
        }

        public async Task AgregaAsync(VehiculoDtoForCreate vehiculo)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(vehiculo.strUsuario);

            if (user != null)
            {
                var existe = await ExisteVehiculoAsync(vehiculo);
                if (! existe )
                {
                    await _repo.AgregaAsync(vehiculo.strMatricula, vehiculo.strNumeroEconomico, vehiculo.intHabilitado, vehiculo.intTipoPatrullaje, vehiculo.intTipoVehiculo, vehiculo.intRegionSSF);
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
        public async Task<List<VehiculoDto>> ObtenerVehiculosPorOpcionAsync(string opcion, int region, string? criterio, string usuario)
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
                                criterio = "%" + criterio + "%";
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
                                criterio = "%" + criterio + "%";
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
                                criterio = "%" + criterio + "%";
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
                                criterio = "%" + criterio + "%";
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
                                criterio = "%" + criterio + "%";
                                l = await _repo.ObtenerVehiculosHabilitadosPorRegionCriterioTerrestreAsync(region, criterio);
                                break;
                        }
                        break;
                }
            }

            return ConvierteListaVehiculosToDto(l);
        }

        private async Task<bool> ExisteVehiculoAsync(VehiculoDtoForCreate vehiculo)
        {
            var vehis = await _repo.ObtenerNumeroDeVehiculosPorMatriculaAndComandanciaAsync(vehiculo.strMatricula, vehiculo.intRegionSSF);

            if (vehis > 0) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        private VehiculoDto ConvierteVehiculoPatrullajeToDto(VehiculoPatrullajeVista v)
        {
            return new VehiculoDto() 
            {
                intIdVehiculo = v.id_vehiculo,
                intRegionSSF = v.id_comandancia,
                intTipoVehiculo = v.id_tipovehiculo,
                intTipoPatrullaje = v.id_tipopatrullaje,
                strNumeroEconomico = v.numeroeconomico,
                strMatricula = v.matricula,
                strDescripcionTipoPatrullaje = v.descripcion,
                strDescripcionTipoVehiculo = v.descripciontipoVehiculo,
                intHabilitado = v.habilitado
            };
        }

        private List<VehiculoDto> ConvierteListaVehiculosToDto(List<VehiculoPatrullajeVista> lstaVehiculos)
        {
            var lsta = new List<VehiculoDto>();

            foreach (var v in lstaVehiculos)
            { 
                var nvoVehiculo = ConvierteVehiculoPatrullajeToDto(v);
                lsta.Add(nvoVehiculo);
            }

            return lsta;
        }
    }
}

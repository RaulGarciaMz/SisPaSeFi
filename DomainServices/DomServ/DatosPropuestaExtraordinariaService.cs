using Domain.DTOs;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class DatosPropuestaExtraordinariaService : IDatosPropuestaExtraordinariaService
    {
        private readonly IDatosPropuestaExtraRepo _repo;
        private readonly IUsuariosParaValidacionQuery _userConf;

        public DatosPropuestaExtraordinariaService(IDatosPropuestaExtraRepo repo, IUsuariosParaValidacionQuery uc)
        {
            _repo = repo;
            _userConf = uc;
        }

        public async Task<DatosPropuestaExtraordinariaDto> ObtenerDatosPropuestaExtraordinariaAsync(int idPropuesta, string usuario)
        {
            var resul = new DatosPropuestaExtraordinariaDto();

            var userId = await _userConf.ObtenerIdUsuarioPorUsuarioNomAsync(usuario);

            if (userId != null)
            {
                var lineas = new List<DatosPropuestaExtraordinariaLineaDto>();
                var vehiculos = new List<DatosPropuestaExtraordinariaVehiculoDto>();

                var ubs = await _repo.ObtenerUbicacionLineasPorIdPropuestaAsync(idPropuesta);
                if (ubs != null && ubs.Count > 0)
                {
                    foreach (var b in ubs)
                    {
                        var l = new DatosPropuestaExtraordinariaLineaDto()
                        {
                            strClave = b.clave,
                            strUbicacionPuntoInicio = b.inicio,
                            strUbicacionPuntoFin = b.fin
                        };

                        lineas.Add(l);
                    }
                }

                resul.listaLineas = lineas;

                var vehi = await _repo.ObtenerVehiculosPorIdPropuestaAsync(idPropuesta);
                if (vehi != null && vehi.Count > 0)
                {
                    foreach (var v in vehi)
                    {
                        var vh = new DatosPropuestaExtraordinariaVehiculoDto()
                        {
                            strMatricula = v.placas,
                            strNumeroEconomico =v.numero,
                            strDescripcionTipoVehiculo = v.tipo
                        };

                        vehiculos.Add(vh);
                    }
                }

                resul.listaVehiculos = vehiculos;

                var responsables = await _repo.ObtenerResponsablesPorIdPropuestaAsync(idPropuesta);
                if (responsables != null && responsables.Count > 0)
                { 
                    var r = responsables[0];

                    resul.strFechaPatrullaje = r.fechaPatrullaje.ToString("yyyy-MM-dd");
                    resul.strFechaTermino = r.fechaTermino.ToString("yyyy-MM-dd");
                    resul.strUbicacion = r.ubicacion;
                    resul.strMunicipioUbicacion = r.municipio;
                    resul.strEstadoUbicacion = r.estado;
                    resul.strNombre = r.nombre;
                    resul.strApellido1 = r.apellido1;
                    resul.strApellido2 = r.apellido2;
                    resul.strInstalacionResponsable = r.InstalacionResponsable;
                    resul.strMunicipioPuntoResponsable = r.mun;
                    resul.strEstadoPuntoResponsable = r.edo;
                }
            }

            return resul;
        }
    }
}
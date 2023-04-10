using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Enums;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.DomServ
{
    public class EstructurasService: IEstructurasService
    {
        private readonly IEstructurasRepo _repo;
        private readonly IUsuariosConfiguradorQuery _userConf;

        public EstructurasService(IEstructurasRepo repo, IUsuariosConfiguradorQuery uc)
        {
            _repo = repo;
            _userConf = uc;
        }

        public async Task ActualizaUbicacionAsync(int idEstructura, string nombre, int idMunicipio, string latitud, string longitud, string usuario)
        {
            var user = await _userConf.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null) 
            {
                string coordenadas = latitud + "," + longitud;
                var enUsoPorOtros = await EstanCoordenadasDeEstructuraEnUso(idEstructura,coordenadas);
                if (!enUsoPorOtros)
                {
                    await _repo.ActualizaUbicacionAsync(idEstructura, nombre, idMunicipio, latitud, longitud);
                }
            }
        }

        public async Task AgregaAsync(int idLinea, string nombre, int idMunicipio, string? latitud, string? longitud, string usuario)
        {
            var user = await _userConf.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            
            if (user != null)
            {
                if (latitud != null && longitud != null)
                {
                    string coordenadas = latitud + "," + longitud;
                    var enUso = await EstanCoordenadasEnUso(coordenadas);
                    if (!enUso)
                    {
                        await _repo.AgregaAsync(idLinea, nombre, idMunicipio, latitud, longitud);
                    }
                }
            }
        }

        public async Task<List<EstructuraDto>> ObtenerEstructuraPorOpcionAsync(int opcion, int idLinea, string criterio, string usuario)
        {      
            var retDto = new List<EstructuraDto>();
            var lstEst = new List<EstructurasVista>();

            var user = await _userConf.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            var miOpcion = (LineaEstructuraOpcion)opcion;

            if (user != null)
            {

                switch (miOpcion) 
                {
                    case LineaEstructuraOpcion.EstructurasDeUnaLinea:
                        lstEst = await _repo.ObtenerEstructuraPorLineaAsync(idLinea);
                        break;
                    case LineaEstructuraOpcion.EstructurasDeUnaLineaEnRuta:
                        var idRuta = Int32.Parse(criterio);
                        lstEst = await _repo.ObtenerEstructuraPorLineaEnRutaAsync(idLinea, idRuta);
                        break;
                    case LineaEstructuraOpcion.EstructurasAlrededorDeCoordenada:

                        var coord = criterio.Split(",");
                        lstEst = await _repo.ObtenerEstructuraAlrededorDeCoordenadaAsync(float.Parse(coord[0]), float.Parse(coord[1]));
                        break;
                }
                
                retDto = ConvierteListaEstructurasDomainToDto(lstEst);
            }

            return retDto;
        }

        public async Task<EstructuraDto> ObtenerEstructuraPorIdAsync(int idEstructura, string usuario)
        {
            var retDto = new EstructuraDto();
            var user = await _userConf.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var estructura = await _repo.ObtenerEstructuraPorIdAsync(idEstructura);

                if (estructura != null) 
                {
                    retDto = ConvierteEstructuraToDto(estructura);
                }                
            }

            return retDto;
        }


        private List<EstructuraDto> ConvierteListaEstructurasDomainToDto(List<EstructurasVista> lista) 
        {
            var retDto = new List< EstructuraDto>();

            foreach (var item in lista) 
            { 
                retDto.Add(ConvierteEstructuraToDto(item));
            }

            return retDto;
        }

        private async Task<bool> EstanCoordenadasEnUso(string coordenadas) {

            var existen = await _repo.ObtenerEstructurasEnCoordenadas(coordenadas);

            if (existen == null) 
            { 
                return false;
            }

            if (existen.Count() == 0) 
            { 
                return false; 
            }

            return true;
        }

        private async Task<bool> EstanCoordenadasDeEstructuraEnUso(int idEstructura, string coordenadas)
        {

            var existen = await _repo.ObtenerEstructurasEnCoordenadasPorId(idEstructura,coordenadas);

            if (existen == null)
            {
                return false;
            }

            if (existen.Count() == 0)
            {
                return false;
            }

            return true;
        }

        private EstructuraDto ConvierteEstructuraToDto(EstructurasVista e) 
        {
            return new EstructuraDto()
            { 
                IdEstructura = e.id_estructura,
                Clave = e.clave,
                Coordenadas = e.coordenadas,
                Descripcion = e.descripcion,
                Estado = e.estado,
                IdEstado = e.id_estado,
                IdGerenciaDivision = e.id_GerenciaDivision,
                IdLinea = e.id_linea,
                IdMunicipio = e.id_municipio,
                IdProcesoResponsable   = e.id_ProcesoResponsable,
                Latitud = e.latitud,
                Longitud = e.longitud,
                Municipio = e.municipio,
                Nombre = e.nombre
            };
        }
    }
}

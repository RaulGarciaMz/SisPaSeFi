using Domain.DTOs;
using Domain.Entities.Vistas;
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
                await _repo.ActualizaUbicacionAsync(idEstructura, nombre, idMunicipio, latitud, longitud);
            }
        }

        public async Task AgregaAsync(int idLinea, string nombre, int idMunicipio, string latitud, string longitud, string usuario)
        {
            var user = await _userConf.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                await _repo.AgregaAsync(idLinea, nombre, idMunicipio, latitud, longitud);
            }
        }

        public async Task<List<EstructuraDto>> ObtenerEstructuraPorLineaAsync(int idLinea, string usuario)
        {
            var retDto = new List<EstructuraDto>();
            var user = await _userConf.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var lstEstr = await _repo.ObtenerEstructuraPorLineaAsync(idLinea);
                retDto = ConvierteListaEstructurasDomainToDto(lstEstr);
            }

            return retDto;
        }

        public async Task<List<EstructuraDto>> ObtenerEstructuraPorLineaEnRutaAsync(int idLinea, int idRuta, string usuario)
        {
            var retDto = new List<EstructuraDto>();
            var user = await _userConf.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
              var lstEstr =  await _repo.ObtenerEstructuraPorLineaEnRutaAsync(idLinea, idRuta);

                retDto = ConvierteListaEstructurasDomainToDto(lstEstr);                
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

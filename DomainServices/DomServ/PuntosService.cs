using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using SqlServerAdapter.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using NetTopologySuite;

namespace DomainServices.DomServ
{
    public class PuntosService : IPuntosService
    {
        private readonly IPuntoPatrullajeRepo _repo;
        private readonly GeometryFactory _geometryFactory;

        public PuntosService(IPuntoPatrullajeRepo repo)
        {
            _repo = repo;
            _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        }

        /// <summary>
        /// Método <c>Agrega</c> Implementa la interfaz para el caso de uso de agregar un punto de patrullaje
        /// </summary>
        public void Agrega(PuntoDto pp)
        {           
            if (_repo.CoordenadasEnUso(pp.latitud, pp.longitud) == true)
            {
                return;
            }

            var p = ConvierteDtoToDominio(pp);           

            _repo.Agrega(p);
        }

        /// <summary>
        /// Método <c>Delete</c> Implementa la interfaz para el caso de uso de eliminar un punto de patrullaje, mientras no esté en otros itinerarios
        /// </summary>
        public void Delete(int id)
        {
            if (ExisteEnItinerarios(id))
            {
                return;
            }

            _repo.Delete(id);
        }

        /// <summary>
        /// Método <c>Agrega</c> Implementa la interfaz para el caso de uso de actualizar un punto de patrullaje
        /// </summary>
        public void Update(PuntoDto pp)
        {           
            if (_repo.CoordenadasEnUsoEnPuntoDiferente(pp.latitud, pp.longitud, pp.id_punto) == true)
            {
                return;
            }

            var p = ConvierteDtoToDominio(pp); 

            _repo.Update(p);
        }

        /// <summary>
        /// Método <c>ObtenerPorOpcion</c> Implementa la interfaz para el caso de uso de obtener puntos de patrullaje acorde a un filtro indicado
        /// </summary>
        public List<PuntoDto> ObtenerPorOpcion(FiltroPunto opcion, string valor)
        {
            var puntos = new List<PuntoPatrullaje>();
            var r = new List<PuntoDto>();

            switch (opcion)
            {
                case FiltroPunto.Estado:
                    var b = int.TryParse(valor, out int j);
                    if (!b)
                    {
                        return r;
                    }
                    puntos = _repo.ObtenerPorEstado(j);
                    break;

                case FiltroPunto.Ubicacion:
                    puntos = _repo.ObtenerPorUbicacion(valor).ToList();
                    break;
            }

            foreach (var p in puntos)
            {
                var miPto = ConvierteDominioToDto(p);
                r.Add(miPto);
            }

            return r;
        }

        /// <summary>
        /// Método <c>ExisteEnItinerarios</c> verifica si el punto de patrullaje indicado existe en algún itinerario
        /// </summary>
        private bool ExisteEnItinerarios(int id)
        {
            if (_repo.ObtenerItinerariosPorPunto(id) > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Método <c>ConvierteDtoToDominio</c> convierte objetos de transferencia de datos (DTO) a objetos del dominio
        /// </summary>
        private PuntoPatrullaje ConvierteDtoToDominio(PuntoDto p)
        {
            return new PuntoPatrullaje
            {
                Id = p.id_punto,
                Ubicacion = p.ubicacion,
                CoordenadasSrid = _geometryFactory.CreatePoint(new Coordinate(p.latitud, p.longitud)),
                EsInstalacion = p.esInstalacion,
                IdNivelRiesgo = p.id_nivelRiesgo,
                IdComandancia = p.id_comandancia,
                IdProcesoResponsable = p.id_ProcesoResponsable,
                UltimaActualizacion = DateTime.UtcNow,
                Bloqueado = p.bloqueado,
                IdMunicipio = p.id_municipio
            };
        }

        /// <summary>
        /// Método <c>ConvierteDominioToDto</c> convierte objetos del dominio a objetos de transferencia de datos (DTO)
        /// </summary>
        private PuntoDto ConvierteDominioToDto(PuntoPatrullaje p)
        {
            return new PuntoDto
            {
                id_punto = p.Id,
                ubicacion = p.Ubicacion,
                esInstalacion = p.EsInstalacion,
                id_nivelRiesgo = p.IdNivelRiesgo,
                id_comandancia = p.IdComandancia,
                id_ProcesoResponsable = p.IdProcesoResponsable,
                bloqueado = p.Bloqueado,
                latitud =  p.CoordenadasSrid.X,
                longitud = p.CoordenadasSrid.X,
                id_municipio = p.IdMunicipio,
                id_estado = p.IdMunicipioNavigation.IdEstado,
                municipio = p.IdMunicipioNavigation.Nombre,
                estado = p.IdMunicipioNavigation.IdEstadoNavigation.Nombre  
            };
        }

    }    
}

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

namespace DomainServices.DomServ
{
    public class PuntosService : IPuntosService
    {
        private readonly IPuntoPatrullajeRepo _repo;

        public PuntosService(IPuntoPatrullajeRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Método <c>Agrega</c> Implementa la interfaz para el caso de uso de agregar un punto de patrullaje
        /// </summary>
        public void Agrega(PuntoDto pp)
        {
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
                id_punto = p.id_punto,
                ubicacion = p.ubicacion,
                coordenadas = p.coordenadas,
                esInstalacion = p.esInstalacion,
                id_nivelRiesgo = p.id_nivelRiesgo,
                id_comandancia = p.id_comandancia,
                id_ProcesoResponsable = p.id_ProcesoResponsable,
                id_GerenciaDivision = p.id_GerenciaDivision,
                bloqueado = p.bloqueado,
                //latitud =  ,
                //longitud = ,
                id_municipio = p.id_municipio,
            };
        }

        /// <summary>
        /// Método <c>ConvierteDominioToDto</c> convierte objetos del dominio a objetos de transferencia de datos (DTO)
        /// </summary>
        private PuntoDto ConvierteDominioToDto(PuntoPatrullaje p)
        {
            return new PuntoDto
            {
                id_punto = p.id_punto,
                ubicacion = p.ubicacion,
                coordenadas = p.coordenadas,
                esInstalacion = p.esInstalacion,
                id_nivelRiesgo = p.id_nivelRiesgo,
                id_comandancia = p.id_comandancia,
                id_ProcesoResponsable = p.id_ProcesoResponsable,
                id_GerenciaDivision = p.id_GerenciaDivision,
                bloqueado = p.bloqueado,
                //latitud =  ,
                //longitud = ,
                id_municipio = p.id_municipio,
                id_estado = p.Municipio.id_estado,
                municipio = p.Municipio.nombre,
                //estado = p.Municipio.id_estado,  // Falta relación Municipio- estado en Dominio
            };
        }

    }    
}

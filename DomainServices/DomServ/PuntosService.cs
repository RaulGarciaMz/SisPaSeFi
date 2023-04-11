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
        public async Task Agrega(PuntoDto pp, string usuario)
        {
            if (await EsUsuarioConfigurador(usuario))
            {
                var p = ConvierteDtoToDominio(pp);
                await _repo.Agrega(p);
            }
        }

        /// <summary>
        /// Método <c>Delete</c> Implementa la interfaz para el caso de uso de eliminar un punto de patrullaje, mientras no esté en otros itinerarios
        /// </summary>
        public async Task Delete(int id, string usuario)
        {        
            if (await EsUsuarioConfigurador(usuario))
            {
                if (await ExisteEnItinerarios(id))
                {
                    return;
                }

                await _repo.Delete(id);
            }                
        }

        /// <summary>
        /// Método <c>Agrega</c> Implementa la interfaz para el caso de uso de actualizar un punto de patrullaje
        /// </summary>
        public async Task Update(PuntoDto pp, string usuario)
        {
            if (await EsUsuarioConfigurador(usuario))
            {
                var p = ConvierteDtoToDominio(pp);
                await _repo.Update(p);
            }
        }

        /// <summary>
        /// Método <c>ObtenerPorOpcion</c> Implementa la interfaz para el caso de uso de obtener puntos de patrullaje acorde a un filtro indicado
        /// </summary>
        public async Task<List<PuntoDto>> ObtenerPorOpcionAsync(FiltroPunto opcion, string valor, string usuario)
        {
            var puntos = new List<PuntoPatrullaje>();
            var r = new List<PuntoDto>();
            var b = int.TryParse(valor, out int j);

            if (await EsUsuarioConfigurador(usuario))
            {
                switch (opcion)
                {
                    case FiltroPunto.Ubicacion:
                        puntos = await _repo.ObtenerPorUbicacionAsync(valor);
                        break;

                    case FiltroPunto.Estado:
                        if (!b)
                        {
                            return r;
                        }
                        puntos = await _repo.ObtenerPorEstadoAsync(j);
                        break;

                    case FiltroPunto.Ruta:
                        if (!b)
                        {
                            return r;
                        }
                        puntos = await _repo.ObtenerPorRutaAsync(j);
                        break;

                    case FiltroPunto.Region:
                        if (!b)
                        {
                            return r;
                        }
                        puntos = await _repo.ObtenerPorRegionAsync(j);
                        break;
                }

                foreach (var p in puntos)
                {
                    var miPto = ConvierteDominioToDto(p);
                    r.Add(miPto);
                }
            }

            return r;
        }


        /// <summary>
        /// Método <c>ExisteEnItinerarios</c> verifica si el punto de patrullaje indicado existe en algún itinerario
        /// </summary>
        private async Task<bool> ExisteEnItinerarios(int id)
        {
            if (await _repo.ObtenerItinerariosPorPuntoAsync(id) > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Método <c>EsUsuarioConfigurador</c> verifica si el nombre del usuario corresponde a un usuario configurador
        /// </summary>
        private async Task<bool> EsUsuarioConfigurador(string usuario)
        {
            if (await _repo.ObtenerIdUsuarioConfiguradorAsync(usuario) >= 0)
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
            var coor = p.coordenadas.Trim();
            var coorXY = p.coordenadas.Split(",");
            var lat = coorXY[0].Trim();
            var longi = coorXY[1].Trim();

            return new PuntoPatrullaje
            {
                IdPunto = p.id_punto,
                Ubicacion = p.ubicacion,
                Coordenadas = coor,
                EsInstalacion = p.esInstalacion,
                IdNivelRiesgo = p.id_nivelRiesgo,
                IdComandancia = p.id_comandancia,
                IdProcesoResponsable = p.id_ProcesoResponsable,
                IdGerenciaDivision = p.id_GerenciaDivision,
                Bloqueado = p.bloqueado,
                Latitud =  lat,
                Longitud = longi,
                IdMunicipio = p.id_municipio,
                IdUsuario= p.id_usuario
            };
        }

        /// <summary>
        /// Método <c>ConvierteDominioToDto</c> convierte objetos del dominio a objetos de transferencia de datos (DTO)
        /// </summary>
        private PuntoDto ConvierteDominioToDto(PuntoPatrullaje p)
        {
            return new PuntoDto
            {
                id_punto = p.IdPunto,
                ubicacion = p.Ubicacion,
                coordenadas = p.Coordenadas.Trim(),
                esInstalacion = p.EsInstalacion,
                id_nivelRiesgo = p.IdNivelRiesgo,
                id_comandancia = p.IdComandancia,
                id_ProcesoResponsable = p.IdProcesoResponsable,
                id_GerenciaDivision = p.IdGerenciaDivision,
                bloqueado = p.Bloqueado,
                //Latitud =  p.Latitud,
                //Longitud = ,
                id_municipio = p.IdMunicipio,
                id_estado = p.IdMunicipioNavigation.IdEstado,
                municipio = p.IdMunicipioNavigation.Nombre,
                estado = p.IdMunicipioNavigation.IdEstadoNavigation.Nombre  
            };
        }


    }    
}

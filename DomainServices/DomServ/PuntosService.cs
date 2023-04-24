using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        public async Task<List<PuntoDto>> ObtenerPorOpcionAsync(FiltroPunto opcion, string criterio, string usuario)
        {
            var puntos = new List<PuntoPatrullaje>();
            var r = new List<PuntoDto>();
            var b = int.TryParse(criterio, out int j);

            if (await EsUsuarioConfigurador(usuario))
            {
                switch (opcion)
                {
                    case FiltroPunto.Ubicacion:
                        puntos = await _repo.ObtenerPorUbicacionAsync(criterio);
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
                        int idInstalacionEstrategica = 3;
                        int idComandancia = 0;
                        int idNivelRiesgo = 0;

                        if (criterio.Contains("-"))
                        {
                            var datos = criterio.Split('-');
                            idComandancia = Int32.Parse(datos[0]);
                            idNivelRiesgo = Int32.Parse(datos[1]);
                        }
                        else 
                        {
                            idComandancia = Int32.Parse(criterio);
                            idNivelRiesgo = idInstalacionEstrategica;
                        }

                        puntos = await _repo.ObtenerPorRegionAsync(idComandancia, idNivelRiesgo);
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
            var coor = p.strCoordenadas.Trim();
            var coorXY = p.strCoordenadas.Split(",");
            var lat = coorXY[0].Trim();
            var longi = coorXY[1].Trim();

            return new PuntoPatrullaje
            {
                IdPunto = p.intIdPunto,
                Ubicacion = p.strUbicacion,
                Coordenadas = coor,
                EsInstalacion = p.intEsInstalacion,
                IdNivelRiesgo = p.intIdNivelRiesgo,
                IdComandancia = p.intIdComandancia,
                IdProcesoResponsable = p.intIdProcesoResponsable,
                IdGerenciaDivision = p.intIdGerenciaDivision,
                Bloqueado = p.intBloqueado,
                Latitud =  lat,
                Longitud = longi,
                IdMunicipio = p.intIdMunicipio,
                IdUsuario= p.intIdUsuario
            };
        }

        /// <summary>
        /// Método <c>ConvierteDominioToDto</c> convierte objetos del dominio a objetos de transferencia de datos (DTO)
        /// </summary>
        private PuntoDto ConvierteDominioToDto(PuntoPatrullaje p)
        {
            return new PuntoDto
            {
                intIdPunto = p.IdPunto,
                strUbicacion = p.Ubicacion,
                strCoordenadas = p.Coordenadas.Trim(),
                intEsInstalacion = p.EsInstalacion,
                intIdNivelRiesgo = p.IdNivelRiesgo,
                intIdComandancia = p.IdComandancia,
                intIdProcesoResponsable = p.IdProcesoResponsable,
                intIdGerenciaDivision = p.IdGerenciaDivision,
                intBloqueado = p.Bloqueado,
                //Latitud =  p.Latitud,
                //Longitud = ,
                intIdMunicipio = p.IdMunicipio,
                intIdEstado = p.IdMunicipioNavigation.IdEstado,
                strNombreMunicipio = p.IdMunicipioNavigation.Nombre,
                strNombreEstado = p.IdMunicipioNavigation.IdEstadoNavigation.Nombre  
            };
        }


    }    
}

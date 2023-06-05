﻿using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Enums;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

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
        public async Task AgregaAsync(PuntoDtoForCreate pp, string usuario)
        {
            if (await EsUsuarioConfigurador(usuario))
            {
                var p = ConvierteDtoForCreateToDominio(pp);
                await _repo.Agrega(p);
            }
        }

        /// <summary>
        /// Método <c>Delete</c> Implementa la interfaz para el caso de uso de eliminar un punto de patrullaje, mientras no esté en otros itinerarios
        /// </summary>
        public async Task DeleteAsync(int id, string usuario)
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
        public async Task UpdateAsync(PuntoDtoForUpdate pp)
        {
            if (await EsUsuarioConfigurador(pp.usuario))
            {
                await _repo.Update(pp);
            }
        }

        /// <summary>
        /// Método <c>ObtenerPorOpcion</c> Implementa la interfaz para el caso de uso de obtener puntos de patrullaje acorde a un filtro indicado
        /// </summary>
        public async Task<List<PuntoDto>> ObtenerPorOpcionAsync(FiltroPunto opcion, string criterio, string usuario)
        {
            var puntos = new List<PuntoPatrullajeVista>();
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


        private PuntoPatrullaje ConvierteDtoForCreateToDominio(PuntoDtoForCreate p)
        {
            var coor = p.strCoordenadas.Trim();
            var coorXY = p.strCoordenadas.Split(",");
            var lat = coorXY[0].Trim();
            var longi = coorXY[1].Trim();

            return new PuntoPatrullaje
            {
                Ubicacion = p.strUbicacion,
                Coordenadas = coor,
                EsInstalacion = p.intEsInstalacion,
                IdNivelRiesgo = p.intIdNivelRiesgo,
                IdComandancia = p.intIdComandancia,
                IdProcesoResponsable = p.intIdProcesoResponsable,
                IdGerenciaDivision = p.intIdGerenciaDivision,
                IdUsuario = p.intIdUsuario,
                IdMunicipio = p.intIdMunicipio,
                Bloqueado = p.intBloqueado,
                Latitud = lat,
                Longitud = longi,
                
                //Campos no nulos
                UltimaActualizacion = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Método <c>ConvierteDominioToDto</c> convierte objetos del dominio a objetos de transferencia de datos (DTO)
        /// </summary>
        private PuntoDto ConvierteDominioToDto(PuntoPatrullajeVista p)
        {
            return new PuntoDto
            {
                intIdPunto = p.id_punto,
                strUbicacion = p.ubicacion,
                strCoordenadas = p.coordenadas.Trim(),
                intEsInstalacion = p.esinstalacion,
                intIdNivelRiesgo = p.id_nivelriesgo,
                intIdComandancia = p.id_comandancia,
                intIdProcesoResponsable = p.id_procesoresponsable,
                intIdGerenciaDivision = p.id_gerenciadivision,
                intBloqueado = p.bloqueado,
                //Latitud =  p.Latitud,
                //Longitud = ,
                intIdMunicipio = p.id_municipio,
                intIdEstado = p.id_estado,
                strNombreMunicipio = p.municipio,
                strNombreEstado = p.estado  
            };
        }


    }    
}

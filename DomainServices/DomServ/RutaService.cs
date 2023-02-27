﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Enums;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using Microsoft.VisualBasic;


namespace DomainServices.DomServ
{
    public class RutaService : IRutaService
    {
        IRutasRepo _repo;

        public RutaService(IRutasRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Método <c>Agrega</c> Implementa la interfaz para el caso de uso de agregar una ruta junto con sus itinerarios
        /// </summary>
        public async Task Agrega(RutaDto pp, string usuario)
        {
            if (await EsUsuarioConfigurador(usuario))
            {
                if (await ExisteItinerarioConfiguradoEnRegionesZona(pp.IdTipoPatrullaje, pp.RegionSSF, pp.RegionMilitarSDN, pp.ZonaMilitarSDN, pp.Itinerario))
                {
                    return;
                }

                int totalRutas = await CalculaTotalRutas(pp.IdTipoPatrullaje, pp.RegionMilitarSDN);
                pp.Clave = await GeneraClaveRuta(pp.IdTipoPatrullaje, pp.RegionMilitarSDN, pp.ZonaMilitarSDN, totalRutas);
                pp.TotalRutasRegionMilitarSDN = totalRutas;

                var rp = ConvierteRutaDto(pp);
                var itinerarios = ConvierteRecorridosAItinearios(pp.Recorridos);

                await _repo.AgregaAsync(rp, itinerarios);
            }
        }

        /// <summary>
        /// Método <c>Update</c> Implementa la interfaz para el caso de uso de actualizar una ruta
        /// </summary>
        public async Task Update(RutaDto pp, string usuario)
        {
            if (await EsUsuarioConfigurador(usuario))
            {
                if (await ExisteRutaConMismaClave(pp.IdRuta, pp.Clave))
                {
                    return;
                }

                if (await ExisteItinerarioConfiguradoEnOtraRuta(pp.IdTipoPatrullaje, pp.RegionSSF, pp.RegionMilitarSDN, pp.ZonaMilitarSDN, pp.IdRuta, pp.Itinerario))
                {
                    return;
                }

                int totalRutas = await CalculaTotalRutas(pp.IdTipoPatrullaje, pp.RegionMilitarSDN);
                var strClave = await AsignaClaveRuta(pp, totalRutas);

                pp.Clave = strClave;

                var rp = ConvierteRutaDto(pp);

                await _repo.UpdateAsync(rp);
            }
        }

        /// <summary>
        /// Método <c>Update</c> Implementa la interfaz para el caso de uso de eliminar una ruta
        /// </summary>
        public async Task Delete(int id, string usuario)
        {
            if (await EsUsuarioConfigurador(usuario))
            {
                if (await ExisteRutaEnPropuestaOrPrograma(id))
                {
                    return ;
                }

                await _repo.DeleteAsync(id);
            }            
        }

        /// <summary>
        /// Método <c>ObtenerPorFiltro</c> Implementa la interfaz para el caso de uso de obterne rutas filtradas
        /// </summary>
        public async Task<List<RutaDto>> ObtenerPorFiltro(string usuario, int opcion, string tipo, string criterio, string actividad)
        {
            List<RutaDto> retornadas = new List<RutaDto>();
            List<RutaVista> encontradas = new List<RutaVista>();

            if (!await EsUsuarioConfigurador(usuario)) 
            {
                return retornadas;
            }

            var opcionFiltro = (FiltroRutaOpcion)opcion;
            switch (opcionFiltro)
            {
                case FiltroRutaOpcion.RegionSsf:

                    if (Int32.TryParse(criterio, out int j))
                    {
                        encontradas = await _repo.ObtenerRutasPorRegionSsfAsync(tipo, j);
                    }
                    break;
                default:

                    switch (actividad)
                    {
                        case "Propuesta":
                            var regionSsf = await ObtenerRegionSsfPorUsuario(usuario);
                            var hayUsuarioConRegion = regionSsf is null;

                            switch (opcionFiltro)
                            {
                                case FiltroRutaOpcion.RegionMilitar:
                                    if (hayUsuarioConRegion) 
                                    {
                                        encontradas = await _repo.ObtenerPropuestasPorRegionMilitarAndRegionSsfAsync(tipo, criterio, regionSsf.Value);
                                    }
                                    else
                                    {
                                        encontradas = await _repo.ObtenerRutasPorRegionMilitarAsync(tipo, criterio);
                                    }                                    
                                    break;
                                case FiltroRutaOpcion.RutaItinerarioObservacion:
                                    if (hayUsuarioConRegion)
                                    {
                                        encontradas = await _repo.ObtenerPropuestasPorCombinacionFiltrosConRegionSsfAsync(tipo, criterio, regionSsf.Value);
                                    }
                                    else 
                                    {
                                        encontradas = await _repo.ObtenerRutasPorCombinacionFiltrosAsync(tipo, criterio);
                                    }                                    
                                    break;
                            }
                            break;
                        default:
                            switch (opcionFiltro)
                            {
                                case FiltroRutaOpcion.RegionMilitar:
                                    encontradas = await _repo.ObtenerRutasPorRegionMilitarAsync(tipo, criterio);
                                    break;
                                case FiltroRutaOpcion.RutaItinerarioObservacion:
                                    encontradas = await _repo.ObtenerRutasPorCombinacionFiltrosAsync(tipo, criterio);
                                    break;
                            }
                            break;
                    }
                    break;
            }

            if (encontradas.Count > 0)
            {
                foreach (var item in encontradas)
                {
                    var miruta = new RutaDto()
                    {
                       IdRuta = item.id_ruta,
                       Clave = item.clave,  
                       RegionMilitarSDN = item.regionMilitarSDN,
                       RegionSSF= item.regionSSF,   
                       ZonaMilitarSDN= item.zonaMilitarSDN,
                       Observaciones = item.observaciones,  
                       ConsecutivoRegionMilitarSDN= item.consecutivoRegionMilitarSDN,
                       TotalRutasRegionMilitarSDN = item.totalRutasRegionMilitarSDN,
                       Bloqueado = item.bloqueado,
                       Itinerario = item.itinerarioruta,
                       Habilitado= item.habilitado
                };
                    retornadas.Add(miruta);
                }              
            }

            return retornadas;
        }

        /// <summary>
        /// Método <c>ExisteRutaConMismaClave</c> verifica si en el catálogo de rutas existe otra con la misma clave
        /// </summary>
        private async Task<bool> ExisteRutaConMismaClave(int idRuta, string clave)
        {
            int numRutas = await _repo.ObtenerNumeroRutasPorFiltroAsync(clave, idRuta);
            
            if (numRutas > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Método <c>ExisteItinerarioConfiguradoEnRegionesZona</c> verifica si en el catálogo de itinerarios existe otra para la misma ruta, región, zona y tipo de patrullaje
        /// </summary>
        private async Task<bool> ExisteItinerarioConfiguradoEnRegionesZona(int idTipoPatrullaje, string regionSsf, string regionMilitar, int zonaMilitar, string ruta)
        {
            int numItinerarios = await _repo.ObtenerNumeroItinerariosConfiguradosPorZonasRutaAsync(idTipoPatrullaje, regionSsf, regionMilitar, zonaMilitar, ruta);
  
            if (numItinerarios > 0)
            { 
                return true; 
            }

            return false;
        }

        /// <summary>
        /// Método <c>ExisteItinerarioConfiguradoEnOtraRuta</c> verifica si en el catálogo de itinerarios ya existe con figurado para otra ruta, para la región, zona y tipo de patrullaje
        /// </summary>
        private async Task<bool> ExisteItinerarioConfiguradoEnOtraRuta(int idTipoPatrullaje, string regionSsf, string regionMilitar, int zonaMilitar, int ruta, string rutaItinerario)
        {
            int numItinerarios = await _repo.ObtenerNumeroItinerariosConfiguradosEnOtraRutaAsync(idTipoPatrullaje, regionSsf, regionMilitar, zonaMilitar, ruta, rutaItinerario);

            if (numItinerarios > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Método <c>ExisteRutaEnPropuestaOrPrograma</c> verifica si en los catálogos de programas y propuestas de patrullaje ya existe la ruta indicada
        /// </summary>
        private async Task<bool> ExisteRutaEnPropuestaOrPrograma(int idRuta)
        {
            if ( await _repo.ObtenerNumeroProgramasPorRutaAsync(idRuta) > 0) {
                return true;
            }

            if (await _repo.ObtenerNumeroPropuestasPorRutaAsync(idRuta) > 0)
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
            if (await _repo.ObtenerUsuarioConfiguradorAsync(usuario) != null)
            {
                return true;
            }

            return false;
        }



        /// <summary>
        /// Método <c>CalculaTotalRutas</c> calcula el número de rutas de cierto tipo de patrullaje para una región militar indicadaverifica si en los catálogos de programas y propuestas de patrullaje ya existe la ruta indicada
        /// </summary>
        private async Task<int> CalculaTotalRutas(int tipoPatrullaje, string regionMilitar) 
        {
            int numRutas = await _repo.ObtenerNumeroRutasPorTipoAndRegionMilitarAsync(tipoPatrullaje, regionMilitar);

            return numRutas + 1;
        }

        /// <summary>
        /// Método <c>GeneraClaveRuta</c> genera la clave de la ruta de acuerdo al tipo, región, zona y total de rutas
        /// </summary>
        private async Task<string> GeneraClaveRuta(int tipoPatrullaje, string regionMilitar, int zonaMilitar, int totalRutas) 
        {
           string clave= await GeneraPrefijoClave(tipoPatrullaje, totalRutas);

            return clave + 
                   "-" +
                   ConvierteARomano(regionMilitar) +
                   "-" + zonaMilitar +
                   "-" + totalRutas +
                   "/" + totalRutas;
        }

        /// <summary>
        /// Método <c>AsignaClaveRuta</c> cambia la clave de la ruta en caso de requerirse para una actualización
        /// </summary>
        private async Task<string> AsignaClaveRuta(RutaDto pp, int totalRutas)
        {
            var clave = await GeneraPrefijoClave(pp.IdTipoPatrullaje, totalRutas);

            var strClave = clave + "-" + ConvierteARomano(pp.RegionMilitarSDN) + "-" + pp.ZonaMilitarSDN;
            if ((Strings.Left(pp.Clave, strClave.Length) ?? "") == (strClave ?? ""))
            {
                strClave = pp.Clave;
            }
            else
            {
                strClave = strClave + "-" + totalRutas + "/" + totalRutas;
            }

            return strClave;
        }

        /// <summary>
        /// Método <c>GeneraPrefijoClave</c> genera la el prfijo inicial de la clave de la ruta de acuerdo a la descripción del tipo de patrullaje
        /// </summary>
        private async Task<string> GeneraPrefijoClave(int tipoPatrullaje, int totalRutas)
        {
            var desc = await _repo.ObtenerDescripcionTipoPatrullajeAsync(tipoPatrullaje);

            if (desc == "AEREO")
            {
                return  "PAE";
            }
            else
            {
                return "PTE";
            }
        }

        /// <summary>
        /// Método <c>ObtenerRegionSsfPorUsuario</c> Obtiene la Región SSF a la que pertenece un usuario especificado. Regresa null si el usuario no tiene Region SSF
        /// </summary>
        private async Task<int?> ObtenerRegionSsfPorUsuario(string usuario)
        {
            var u = await _repo.ObtenerUsuarioConfiguradorAsync(usuario);
            if (u is null)
            {
                return null;
            }
            else
            {
                return u.RegionSsf;
            }
        }

        /// <summary>
        /// Método <c>ConvierteARomano</c> Convierte una cadena numérica a una cadena que representa un número romano
        /// </summary>
        string ConvierteARomano(string num) {

            var toRoman = new Dictionary<string, string>() {
                { "1","I"},
                { "2","II"},
                { "3","III"},
                { "4","IV"},
                { "5","V"},
                { "6","VI"},
                { "7","VII"},
                { "8","VIII"},
                { "9","IX"},
                { "10","X"},
                { "11","XI"},
                { "12","XII"},
                { "13","XIII"},
                { "14","XIV"},
                { "15","XV"},
                { "16","XVI"},
                { "17","XVII"},
                { "18","XVIII"},
                { "19","XIX"},
                { "20","XX"},
            };

            return toRoman[num];
        }

        /// <summary>
        /// Método <c>ConvierteRecorridosAItinearios</c> Convierte una lista de recorridos (DTO) a itinerarios
        /// </summary>
        private List<Itinerario> ConvierteRecorridosAItinearios(List<RecorridoDto> recorridos) 
        {
            List<Itinerario> itinerarios = new List<Itinerario>();

            foreach (var reco in recorridos)
            {
                var iti = new Itinerario()
                {
                    IdPunto = reco.IdPunto,
                    Posicion = reco.Posicion
                };
                itinerarios.Add(iti);
            }

            return itinerarios;
        }

        /// <summary>
        /// Método <c>ConvierteRutaDto</c> Convierte un objeto ruta de visualización (DTO) a un objeto Ruta del domino
        /// </summary>
        private Ruta ConvierteRutaDto(RutaDto r)
        {
            return new Ruta()
            {
                Clave = r.Clave,
                RegionMilitarSdn = r.RegionMilitarSDN,
                RegionSsf = r.RegionSSF,
                IdTipoPatrullaje = r.IdTipoPatrullaje,
                Bloqueado = r.Bloqueado,
                ZonaMilitarSdn = r.ZonaMilitarSDN,
                Observaciones = r.Observaciones,
                Habilitado = r.Habilitado
            };
        }
    }
}

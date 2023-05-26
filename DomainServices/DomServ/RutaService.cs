using Domain.DTOs;
using Domain.DTOs.catalogos;
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
        public async Task AgregaAsync(RutaDto pp, string usuario)
        {
            if (await EsUsuarioConfiguradorAsync(usuario))
            {
                var regionssf = pp.intRegionSSF.ToString();
                var regionsMilsdn = pp.intRegionMilitarSDN.ToString();

                if (await ExisteItinerarioConfiguradoEnRegionesZonaAsync(pp.intIdTipoPatrullaje, regionssf, regionsMilsdn, pp.intZonaMilitarSDN, pp.strItinerario))
                {
                    throw new Exception("itinerario existente en la region y zona");
                }

                int totalRutas = await CalculaTotalRutasAsync(pp.intIdTipoPatrullaje, regionsMilsdn);
                pp.strClave = await GeneraClaveRutaAsync(pp.intIdTipoPatrullaje, regionsMilsdn, pp.intZonaMilitarSDN, totalRutas);
                pp.intTotalRutasRegionMilitarSDN = totalRutas;

                var rp = ConvierteRutaDto(pp);
                var itinerarios = ConvierteRecorridosAItinearios(pp.objRecorridoRuta);

                await _repo.AgregaAsync(rp, itinerarios);
            }
        }

        /// <summary>
        /// Método <c>Update</c> Implementa la interfaz para el caso de uso de actualizar una ruta
        /// </summary>
        public async Task UpdateAsync(RutaDto pp, string usuario)
        {
            if (await EsUsuarioConfiguradorAsync(usuario))
            {
                if (await ExisteRutaConMismaClaveAsync(pp.intIdRuta, pp.strClave))
                {
                    throw new Exception("ruta existente con la misma clave");
                }

                var regionssf = pp.intRegionSSF.ToString();
                var regionsMilsdn = pp.intRegionMilitarSDN.ToString();

                if (await ExisteItinerarioConfiguradoEnOtraRutaAsync(pp.intIdTipoPatrullaje, regionssf, regionsMilsdn, pp.intZonaMilitarSDN, pp.intIdRuta, pp.strItinerario))
                {
                    throw new Exception("itinerario existente en otra ruta");
                }

                int totalRutas = await CalculaTotalRutasAsync(pp.intIdTipoPatrullaje, regionsMilsdn);
                var strClave = await AsignaClaveRutaAsync(pp, totalRutas);

                pp.strClave = strClave;

                await _repo.UpdateAsync(pp);
            }
        }

        /// <summary>
        /// Método <c>ReiniciaRegionMilitarAsync</c> Implementa la interfaz para el caso de uso de reiniciar regiones militares
        /// </summary>
        public async Task ReiniciaRegionMilitarAsync(string opcion, string regionMilitar, string tipoPatrullaje, string usuario)
        {
            if (await EsUsuarioConfiguradorAsync(usuario))
            {
                if (opcion == "ReiniciarClavesRegionMilitar")
                {
                    await _repo.ReiniciaRegionMilitarAsync(regionMilitar, tipoPatrullaje);
                }
            }
        }

        /// <summary>
        /// Método <c>Update</c> Implementa la interfaz para el caso de uso de eliminar una ruta
        /// </summary>
        public async Task DeleteAsync(int id, string usuario)
        {
            if (await EsUsuarioConfiguradorAsync(usuario))
            {
                if (await ExisteRutaEnPropuestaOrProgramaAsync(id))
                {
                    return ;
                }

                await _repo.DeleteAsync(id);
            }            
        }

        /// <summary>
        /// Método <c>ObtenerPorFiltro</c> Implementa la interfaz para el caso de uso de obterne rutas filtradas
        /// </summary>
        public async Task<List<RutaDto>> ObtenerPorFiltroAsync(string usuario, int opcion, string tipo, string criterio, string actividad)
        {
            List<RutaDto> retornadas = new List<RutaDto>();
            List<RutaVista> encontradas = new List<RutaVista>();

            if (!await EsUsuarioConfiguradorAsync(usuario)) 
            {
                return retornadas;
            }

            var opcionFiltro = (FiltroRutaOpcion)opcion;
            bool hayUsuarioConRegion = false;

            switch (opcionFiltro)
            {
                case FiltroRutaOpcion.RutaItinerarioObservacion:
                    switch (actividad)
                    {
                        case "Propuesta":
                            var regionSsf = await ObtenerRegionSsfPorUsuarioAsync(usuario);
                            hayUsuarioConRegion = regionSsf is null;

                            if (hayUsuarioConRegion)
                            {
                                encontradas = await _repo.ObtenerPropuestasPorCombinacionFiltrosConRegionSsfAsync(tipo, criterio, regionSsf.Value);
                            }
                            else
                            {
                                encontradas = await _repo.ObtenerRutasPorCombinacionFiltrosAsync(tipo, criterio);
                            }
                            break;

                        case "Programa":
                            encontradas = await _repo.ObtenerRutasPorCombinacionFiltrosAsync(tipo, criterio);
                            break;

                        case "Rutas":
                            encontradas = await _repo.ObtenerRutasTodasPorTipoAndCriterioAsync(tipo, criterio);
                            break;
                    }
                    break;

                case FiltroRutaOpcion.RegionMilitar:
                    switch (actividad)
                    {
                        case "Propuesta":
                            var regionSsf = await ObtenerRegionSsfPorUsuarioAsync(usuario);
                            hayUsuarioConRegion = regionSsf is null;

                            if (hayUsuarioConRegion)
                            {
                                encontradas = await _repo.ObtenerPropuestasPorRegionMilitarAndRegionSsfAsync(tipo, criterio, regionSsf.Value);
                            }
                            else
                            {
                                encontradas = await _repo.ObtenerRutasPorRegionMilitarAsync(tipo, criterio);
                            }
                            break;

                        case "Programa":
                            encontradas = await _repo.ObtenerRutasPorRegionMilitarAsync(tipo, criterio);
                            break;

                        case "Rutas":
                            encontradas = await _repo.ObtenerRutasTodasPorRegionMilitarAsync(tipo, criterio);
                            break;
                    }
                    break;

                case FiltroRutaOpcion.RegionSsf:

                    if (Int32.TryParse(criterio, out int j))
                    {
                        encontradas = await _repo.ObtenerRutasPorRegionSsfAsync(tipo, j);
                    }
                    break;
            }

            if (encontradas.Count > 0)
            {
                foreach (var item in encontradas)
                {
                    var itinPat = new List<RecorridoDto>();

                    var miruta = new RutaDto()
                    {
                       intIdRuta = item.id_ruta,
                       strClave = item.clave,  
                       intRegionMilitarSDN = Int32.Parse(item.regionMilitarSDN),
                       intRegionSSF = Int32.Parse(item.regionSSF),   
                       intZonaMilitarSDN= item.zonaMilitarSDN,
                       strObservaciones = item.observaciones,  
                       intConsecutivoRegionMilitarSDN= item.consecutivoRegionMilitarSDN,
                       intTotalRutasRegionMilitarSDN = item.totalRutasRegionMilitarSDN,
                       intBloqueado = item.bloqueado,
                       strItinerario = item.itinerarioruta,
                       intHabilitado= item.habilitado,
                       intIdTipoPatrullaje = item.id_tipopatrullaje
                    };

                    if(item.itinerariorutapatrullaje.Length > 0) 
                    {
                        var ptosItinerario = item.itinerariorutapatrullaje.Split("¦");

                        foreach (var pto in ptosItinerario)
                        {
                            var strPto = pto.Replace("[!:!]", "¦");
                            var datosPtos = strPto.Split("¦");
                            var reco = new RecorridoDto() 
                            {
                                intPosicion = Int32.Parse(datosPtos[0]),
                                strUbicacion = datosPtos[1],
                                strCoordenadas = datosPtos[2],
                                intIdItinerario = Int32.Parse(datosPtos[3]),
                                intIdPunto = Int32.Parse(datosPtos[4])
                            };

                            itinPat.Add(reco);
                        }
                         miruta.objRecorridoRuta = itinPat;
                    }


                    retornadas.Add(miruta);
                }              
            }

            return retornadas;
        }

        public async Task<List<RutaDisponibleDto>> ObtenerRutasDisponiblesParaCambioDeRutaAsync(string region, DateTime fecha)
        {
           var rutas =  await _repo.ObtenerRutasDisponiblesParaCambioDeRutaAsync(region, fecha);

            return ConvierteListaDeRutasToDisponibleDto(rutas);
        }

        /// <summary>
        /// Método <c>ExisteRutaConMismaClave</c> verifica si en el catálogo de rutas existe otra con la misma clave
        /// </summary>
        private async Task<bool> ExisteRutaConMismaClaveAsync(int idRuta, string clave)
        {
            int numRutas = await _repo.ObtenerNumeroRutasConMismaClaveAsync(clave, idRuta);
            
            if (numRutas > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Método <c>ExisteItinerarioConfiguradoEnRegionesZona</c> verifica si en el catálogo de itinerarios existe otra para la misma ruta, región, zona y tipo de patrullaje
        /// </summary>
        private async Task<bool> ExisteItinerarioConfiguradoEnRegionesZonaAsync(int idTipoPatrullaje, string regionSsf, string regionMilitar, int zonaMilitar, string ruta)
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
        private async Task<bool> ExisteItinerarioConfiguradoEnOtraRutaAsync(int idTipoPatrullaje, string regionSsf, string regionMilitar, int zonaMilitar, int ruta, string rutaItinerario)
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
        private async Task<bool> ExisteRutaEnPropuestaOrProgramaAsync(int idRuta)
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
        private async Task<bool> EsUsuarioConfiguradorAsync(string usuario)
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
        private async Task<int> CalculaTotalRutasAsync(int tipoPatrullaje, string regionMilitar) 
        {
            int numRutas = await _repo.ObtenerNumeroRutasPorTipoAndRegionMilitarAsync(tipoPatrullaje, regionMilitar);

            return numRutas + 1;
        }

        /// <summary>
        /// Método <c>GeneraClaveRuta</c> genera la clave de la ruta de acuerdo al tipo, región, zona y total de rutas
        /// </summary>
        private async Task<string> GeneraClaveRutaAsync(int tipoPatrullaje, string regionMilitar, int zonaMilitar, int totalRutas) 
        {
           string clave= await GeneraPrefijoClaveAsync(tipoPatrullaje, totalRutas);

            return clave + 
                   "-" +
                   ConvierteCadenaNumericaARomano(regionMilitar) +
                   "-" + zonaMilitar +
                   "-" + totalRutas +
                   "/" + totalRutas;
        }

        /// <summary>
        /// Método <c>AsignaClaveRuta</c> cambia la clave de la ruta en caso de requerirse para una actualización
        /// </summary>
        private async Task<string> AsignaClaveRutaAsync(RutaDto pp, int totalRutas)
        {
            var clave = await GeneraPrefijoClaveAsync(pp.intIdTipoPatrullaje, totalRutas);

            var strClave = clave + "-" + ConvierteCadenaNumericaARomano(pp.intRegionMilitarSDN.ToString()) + "-" + pp.intZonaMilitarSDN;
            if ((Strings.Left(pp.strClave, strClave.Length) ?? "") == (strClave ?? ""))
            {
                strClave = pp.strClave;
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
        private async Task<string> GeneraPrefijoClaveAsync(int tipoPatrullaje, int totalRutas)
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
        private async Task<int?> ObtenerRegionSsfPorUsuarioAsync(string usuario)
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
        string ConvierteCadenaNumericaARomano(string num) {

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
                    IdPunto = reco.intIdPunto,
                    Posicion = reco.intPosicion,
                    //Campos no nulos
                    UltimaActualizacion = DateTime.UtcNow
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
                Clave = r.strClave,
                RegionMilitarSdn = r.intRegionMilitarSDN.ToString(),
                RegionSsf = r.intRegionSSF.ToString(),
                IdTipoPatrullaje = r.intIdTipoPatrullaje,
                Bloqueado = r.intBloqueado,
                ZonaMilitarSdn = r.intZonaMilitarSDN,
                Observaciones = r.strObservaciones,
                Habilitado = r.intHabilitado,
                //Campos no nulos
                UltimaActualizacion = DateTime.UtcNow,
                ConsecutivoRegionMilitarSdn = 1,
                TotalRutasRegionMilitarSdn = 1
            };
        }


        /// <summary>
        /// Método <c>ConvierteRutaDto</c> Convierte un objeto ruta de visualización (DTO) a un objeto Ruta del domino
        /// </summary>
        private Ruta ConvierteRutaDtoForUpdate(RutaDto r)
        {
            return new Ruta()
            {
                IdRuta = r.intIdRuta,
                Clave = r.strClave,
                RegionMilitarSdn = r.intRegionMilitarSDN.ToString(),
                RegionSsf = r.intRegionSSF.ToString(),
                Bloqueado = r.intBloqueado,
                ZonaMilitarSdn = r.intZonaMilitarSDN,
                Observaciones = r.strObservaciones,
                Habilitado = r.intHabilitado,
                //Campos no nulos
                UltimaActualizacion = DateTime.UtcNow,
            };
        }

        /// <summary>
        /// Método <c>ConvierteRutaDto</c> Convierte un objeto ruta de visualización (DTO) a un objeto Ruta del domino
        /// </summary>
        private RutaDisponibleDto ConvierteRutaToDisponibleDto(RutaDisponibleVista r)
        {          
            return new RutaDisponibleDto()
            {
                intIdRuta = r.id_ruta,
                intRegionMilitarSDN = r.regionMilitarSDN,
                intRegionSSF = r.regionSSF,
                intZonaMilitarSDN = r.zonaMilitarSDN,
                strClave = r.clave,
                strItinerario = r.itinerario,
                strObservaciones = r.observaciones
            };
        }

        private List<RutaDisponibleDto> ConvierteListaDeRutasToDisponibleDto(List<RutaDisponibleVista> rutas)
        {
            var l = new List<RutaDisponibleDto>();

            foreach (var item in rutas)
            {
                var a = ConvierteRutaToDisponibleDto(item);
                l.Add(a);
            }

            return l;
        }
    }
}

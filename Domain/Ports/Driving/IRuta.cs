using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IRutaCommand
    {
        void Agrega(Ruta r, List<Itinerario> itin);
        void Update(Ruta pp);
        void Delete(int id);
    }

    public interface IRutaQuery
    {
        List<Ruta> ObtenerPorFiltro(int opcion, string tipo, string criterio, string actividad);
    }

    public interface IRutaQueries
    {        
        int ObtenerNumeroRutasPorFiltro(string clave, int idRuta);
        int ObtenerNumeroItinerariosConfiguradosPorZonasRuta(int tipoPatrullaje, string regionSsf, string regionMilitar, int zonaMilitar, string ruta );
        int ObtenerNumeroItinerariosConfiguradosEnOtraRuta(int tipoPatrullaje, string regionSsf, string regionMilitar, int zonaMilitar, int ruta, string rutaItinerario);
        int ObtenerNumeroRutasPorTipoAndRegionMilitar(int tipoPatrullaje, string regionMilitar);
        string ObtenerDescripcionTipoPatrullaje(int tipoPatrullaje);
        int ObtenerNumeroProgramasPorRuta(int idRuta);
        int ObtenerNumeroPropuestasPorRuta(int idRuta);
        Usuario? ObtenerUsuarioConfigurador(string usuario);
        List<RutaVista> ObtenerRutasPorRegionSsf(string tipo, int regionSsf);
        List<RutaVista> ObtenerRutasPorRegionMilitar(string tipo, string regionMilitar);
        List<RutaVista> ObtenerRutasPorCombinacionFiltros(string tipo, string criterio);
        List<RutaVista> ObtenerPropuestasPorRegionMilitarAndRegionSsf(string tipo, string regionMilitar, int regionSsf);
        List<RutaVista> ObtenerPropuestasPorCombinacionFiltrosConRegionSsf(string tipo, string criterio, int regionSsf);
    }
}

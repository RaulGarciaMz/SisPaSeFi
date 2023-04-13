using Domain.Entities;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven
{
    public interface IVehiculoPatrullajeCommand
    {
        Task AgregaAsync(string matricula, string numeroEconomico, int habilitado, int tipoPatrullaje, int tipoVehiculo, int comandancia);
        Task ActualizaAsync(int idVehiculo, string matricula, string numeroEconomico, int habilitado, int tipoPatrullaje, int tipoVehiculo);
        Task BorraPorOpcionAsync(int idPrograma, int idVehiculo);
    }

    public interface IVehiculoPatrullajeQuery
    {
        Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorRegionAsync(int region);
        Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorRegionCriterioAsync(int region, string criterio);
        Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorRegionParaPatrullajeAereoAsync(int region);
        Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorRegionCriterioParaPatrullajeAereoAsync(int region, string criterio);
        Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorRegionParaPatrullajeTerrestreAsync(int region);
        Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPorRegionCriterioParaPatrullajeTerrestreAsync(int region, string criterio);
        Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosPatrullajeExtraordinarioPorDescripcionAsync(int idPropuesta, string descripcion );
        Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosHabilitadosPorRegionAereoAsync(int region);
        Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosHabilitadosPorRegionCriterioAereoAsync(int region, string criterio);
        Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosHabilitadosPorRegionTerrestreAsync(int region);
        Task<List<VehiculoPatrullajeVista>> ObtenerVehiculosHabilitadosPorRegionCriterioTerrestreAsync(int region, string criterio);


        Task<int> ObtenerNumeroDeVehiculosPorMatriculaAndComandanciaAsync(string matricula, int comandancia);
    }
}

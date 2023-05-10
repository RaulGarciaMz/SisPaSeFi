using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class MonitoreoVista
    {
        public int id_programa { get; set; }
        public int id_ruta { get; set; }
        public DateTime? fechapatrullaje { get; set; }
        public TimeSpan? inicio { get; set; }
        public int riesgopatrullaje { get; set; }
        public int id_puntoresponsable { get; set; }
        public int id_ruta_original { get; set; }
        public string clave { get; set; }
        public string regionmilitarsdn { get; set; }
        public string regionssf { get; set; }
        public string? observaciones { get; set; }
        public string descripcionnivel { get; set; }
        public string descripcionestadopatrullaje { get; set; }
        public string descripcion { get; set; }

    }

    [Keyless]
    public class PuntoEnRutaVista
    {
        public string ubicacion { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
    }


    [Keyless]
    public class IncidenciaTarjetaVista
    {
        public string incidencia { get; set; }
        public string nombre { get; set; }
        public string clave { get; set; }
    }

    [Keyless]
    public class UsoVehiculoMonitoreoVista
    {
        public int id_vehiculo { get; set; }
        public int kminicio { get; set; }
        public int kmfin { get; set; }
        public int consumocombustible { get; set; }
        public string? estadovehiculo { get; set; }
        public string? numeroeconomico { get; set; }
        public string matricula { get; set; }
    }


}

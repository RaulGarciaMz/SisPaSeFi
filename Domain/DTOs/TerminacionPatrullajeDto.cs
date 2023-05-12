using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class TerminacionPatrullajeDto
    {
        public string FechaHora { get; set; }
        public string IdEquipo { get; set; }
        public string usuario { get; set; }
        public int IdRuta { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }
        public string HoraInicio { get; set; }
        public string HoraTermino { get; set; }
        public int ComandanteInstalacion { get; set; }
        public int ComandanteTurno { get; set; }
        public int OficialSSF { get; set; }
        public int OficialSDN { get; set; }
        public int TropaSDN { get; set; }
        public int Conductores { get; set; }
        public int RegionSSF { get; set; }
        public string FechaPatrullaje { get; set; }
        public string TiempoVuelo { get; set; }
        public string CalzoCalzo { get; set; }
        public string ObservacionesPatrullaje { get; set; }
        public string EstadoPatrullaje { get; set; }
        public int KmRecorrido { get; set; }
        public int OficialSEMAR { get; set; }
        public int TropaSEMAR { get; set; }
        public List<TerminacionPatrullaVehiculoDto> objTerminoPatrullajeVehiculo { get; set; }
    }

    public class TerminacionPatrullaVehiculoDto
    {
        public int IdVehiculo { get; set; }
        public int KmInicio { get; set; }
        public int KmFin { get; set; }
        public int Combustible { get; set; }
        public string EstadoVehiculo { get; set; }
    }
}

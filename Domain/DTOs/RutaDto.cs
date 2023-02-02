using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class RutaDto
    {
        public int IdRuta;
        public string Clave;
        public string RegionMilitarSDN;
        public string RegionSSF;
        public int IdTipoPatrullaje;
        public int Bloqueado;
        public int ZonaMilitarSDN;
        public string Observaciones;
        public int ConsecutivoRegionMilitarSDN;
        public int TotalRutasRegionMilitarSDN;
        public string UltimaActualizacion;
        public int Habilitado;

        public string Itinerario;
        public List<RecorridoDto> Recorridos;
    }

    public class RecorridoDto
    {
        public int IdItinerario;
        public int IdPunto;
        public int Posicion;
        public string Ubicacion;
        public string Coordenadas;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ProgramaPatrullajeMensualDto
    {
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string NombreResponsable { get; set; }
        public string ApellidoResponsable1 { get; set; }
        public string ApellidoResponsable2 { get; set; }
        public string RegionesMilitares { get; set; }
        public List<RutasProgramaPatrullajeMensualDto> RutasProgramaPatrullajeMensual { get; set; }
    }

    public class RutasProgramaPatrullajeMensualDto
    {
        public int IdRuta { get; set; }
        public string RegionMilitar { get; set; }
        public string RegionSSF { get; set; }
        public int ZonaMilitar { get; set; }
        public string Clave { get; set; }
        public string ItinerarioRuta { get; set; }
        public string Fechas { get; set; }
        public string ObservacionesRuta { get; set; }
        public List<RecorridoRutaDto> RecorridoRuta { get; set; }        
    }

    public class RecorridoRutaDto
    {
        public int IdItinerario { get; set; }
        public int IdPunto { get; set; }
        public int RegionSSF { get; set; }
        public int Posicion { get; set; }
        public string Ubicacion { get; set; }
        public string Coordenadas { get; set; }
    }
}

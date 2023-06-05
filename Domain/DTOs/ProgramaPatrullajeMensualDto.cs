namespace Domain.DTOs
{
    public class ProgramaPatrullajeMensualDto
    {
        public string strMunicipio { get; set; }
        public string strEstado { get; set; }
        public string strNombreResponsable { get; set; }
        public string strApellidoResponsable1 { get; set; }
        public string strApellidoResponsable2 { get; set; }
        public string strRegionesMilitares { get; set; }
        public List<RutasProgramaPatrullajeMensualDto> objRutasProgramaPatrullajeMensual { get; set; }
    }

    public class RutasProgramaPatrullajeMensualDto
    {
        public int intIdRuta { get; set; }
        public string intRegionMilitar { get; set; }
        public string intRegionSSF { get; set; }
        public int intZonaMilitar { get; set; }
        public string strClave { get; set; }
        public string strItinerarioRuta { get; set; }
        public string strFechas { get; set; }
        public string strObservacionesRuta { get; set; }
        public List<RecorridoRutaDto> objRecorridoRuta { get; set; }        
    }

    public class RecorridoRutaDto
    {
        public int intIdItinerario { get; set; }
        public int intIdPunto { get; set; }
        //public int RegionSSF { get; set; }
        public int intPosicion { get; set; }
        public string strUbicacion { get; set; }
        public string strCoordenadas { get; set; }
    }
}

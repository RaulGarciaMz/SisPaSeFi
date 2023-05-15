namespace Domain.DTOs
{
    public class RegistrarIncidenciaDto
    {
        public string FechaHora { get; set; }
        public string IdEquipo { get; set; }
        public string usuario { get; set; }
        public int IdRuta { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }
        public string FechaPatrullaje { get; set; }
        public string TipoIncidencia { get; set; }
        public int IdActivo { get; set; }
        public string DescripcionIncidencia { get; set; }
        public int IdPrioridad { get; set; }
        public int IdClasificacion { get; set; }
        public List<EvidenciaIncidenciaMovilDto> listaEvidencia { get; set; }
        public List<AfectacionIncidenciaMovilDto> listaAfectaciones { get; set; }
    }

    public class EvidenciaIncidenciaMovilDto
    {
        public string strNombreArchivo { get; set; }
        public string strDescripcion { get; set; }
    }

    public class AfectacionIncidenciaMovilDto
    {
        public int intIdAfectacion { get; set; }
        public int intTotalAfectaciones { get; set; }
    }
}
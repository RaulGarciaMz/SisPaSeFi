namespace Domain.DTOs
{
    public class LineaDtoForCreate
    {
        public string strClave { get; set; }
        public string strDescripcion { get; set; }
        public int intIdPuntoInicio { get; set; }
        public int intIdPuntoFin { get; set; }
        public string strUsuario { get; set; }

    }
    public class LineaDtoForUpdate
    {
        public int intIdLinea { get; set; }
        public string strClave { get; set; }
        public string strDescripcion { get; set; }
        public int intIdPuntoInicio { get; set; }
        public int intIdPuntoFin { get; set; }
        public int intBloqueado { get; set; }
        public string strUsuario { get; set; }       
    }

    public class LineaDto
    {
        public int intIdLinea { get; set; }
        public string strClave { get; set; }
        public string strDescripcion { get; set; }
        public int intIdPuntoInicio { get; set; }
        public string strUbicacionPuntoInicio { get; set; }
        public string strEstadoPuntoInicio { get; set; }
        public int intIdPuntoFin { get; set; }
        public string strUbicacionPuntoFin { get; set; }
        public string strEstadoPuntoFin { get; set; }
        public string strMunicipioPuntoInicio { get; set; }
        public string strMunicipioPuntoFin { get; set; }
    }
}


namespace Domain.DTOs
{
    public class DatosPropuestaExtraordinariaDto
    {
        public int intIdPropuesta { get; set; }
        public string strFechaPatrullaje { get; set; }
        public string strFechaTermino { get; set; }
        public string strUbicacion { get; set; }
        public string strMunicipioUbicacion { get; set; }
        public string strEstadoUbicacion { get; set; }
        public string strNombre { get; set; }
        public string strApellido1 { get; set; }
        public string strApellido2 { get; set; }
        public string strInstalacionResponsable { get; set; }
        public string strMunicipioPuntoResponsable { get; set; }
        public string strEstadoPuntoResponsable { get; set; }
        public List<DatosPropuestaExtraordinariaLineaDto> listaLineas { get; set; }
        public List<DatosPropuestaExtraordinariaVehiculoDto> listaVehiculos { get; set; }
    }

    public class DatosPropuestaExtraordinariaLineaDto
    {
        public string strClave { get; set; }
        public string strUbicacionPuntoInicio { get; set; }
        public string strUbicacionPuntoFin { get; set; }
    }

    public class DatosPropuestaExtraordinariaVehiculoDto
    {
        public string strMatricula { get; set; }
        public string strNumeroEconomico { get; set; }
        public string strDescripcionTipoVehiculo { get; set; }

    }
}
namespace Domain.DTOs
{
    public class DatosGeneralesMovilDto
    {
        public int intIdPrograma { get; set; }
        public int intIdRuta { get; set; }
        public string strFechaPatrullaje { get; set; }
        public string strInicio { get; set; }
        public string strClave { get; set; }
        public int intRegionMilitarSDN { get; set; }
        public int intRegionSSF { get; set; }
        public string strObservacionesRuta { get; set; }
        public int intIdEstadoPatrullaje { get; set; }
        public string strDescripcionEstadoPatrullaje { get; set; }
        public int intIdRiesgoPatrullaje { get; set; }
        public string strDescripcionRiesgoPatrullaje { get; set; }
        public int intIdPuntoResponsable { get; set; }
        public string strNombreInstalacionResponsable { get; set; }
        public int intIdApoyoPatrullaje { get; set; }
        public string strDescripcionApoyoPatrullaje { get; set; }
        public int intIdRutaOriginal { get; set; }
    }

    public class ItinerarioRutaMovilDto
    {
        public string strUbicacion { get; set; }
        public string strLatitud { get; set; }
        public string strLongitud { get; set; }
    }

    public class TarjetaInformativaTerrestreMovilDto
    {
        public int intIdTarjetaInformativa { get; set; }
        public string strUltimaActualizacion { get; set; }
        public int intIdUsuario { get; set; }
        public string strInicio { get; set; }
        public string strTermino { get; set; }
        public string strObservaciones { get; set; }
        public int intComandantesInstalacionSSF { get; set; }
        public int intComandantesTurnoSSF { get; set; }
        public int intOficialesSSF { get; set; }
        public int intConductoresSSF { get; set; }
        public int intPersonalMilitarOficialSDN { get; set; }
        public int intPersonalMilitarTropaSDN { get; set; }
        public int intIdEstadoTarjetaInformativa { get; set; }
        public int intPersonalNavalOficialSEMAR { get; set; }
        public int intPersonalNavalTropaSEMAR { get; set; }
    }

    public class TarjetaInformativaAereaMovilDto
    {
        public int intIdTarjetaInformativa { get; set; }
        public string strUltimaActualizacion { get; set; }
        public int intIdUsuario { get; set; }
        public string strInicio { get; set; }
        public string strTermino { get; set; }
        public string strObservaciones { get; set; }
        public int intComandantesInstalacionSSF { get; set; }
        public int intComandantesTurnoSSF { get; set; }
        public int intOficialesSSF { get; set; }
        public int intConductoresSSF { get; set; }
        public int intPersonalMilitarOficialSDN { get; set; }
        public int intPersonalMilitarTropaSDN { get; set; }
        public int intIdEstadoTarjetaInformativa { get; set; }
        public int intPersonalNavalOficialSEMAR { get; set; }
        public int intPersonalNavalTropaSEMAR { get; set; }
        public string strTiempoVuelo { get; set; }
        public string strCalzoCalzo { get; set; }
    }

    public class ReporteEstructuraMovilDto
    {
        public string strIncidencia { get; set; }
        public string strNombreEstructura { get; set; }
        public string strClaveLinea { get; set; }
    }

    public class ReporteInstalacionMovilDto
    {
        public string strIncidencia { get; set; }
        public string strUbicacion { get; set; }
    }

    public class VehiculoMovilDto
    {
        public int intIdVehiculo { get; set; }
        public int intKmInicio { get; set; }
        public int intKmFin { get; set; }
        public int intConsumoCombustible { get; set; }
        public string strEstadoVehiculo { get; set; }
        public string strNumeroEconomico { get; set; }
        public string strMatricula { get; set; }
    }

    public class PatrullajesAereosMovilDto
    {
        public DatosGeneralesMovilDto objMMDatosGenerales { get; set; }
        public List<ItinerarioRutaMovilDto> listaMMItinerarioRuta { get; set; }
        public TarjetaInformativaTerrestreMovilDto objMMTarjetaInformativa { get; set; }
        public List<ReporteEstructuraMovilDto> listaMMReporteEstructura { get; set; }
        public List<ReporteInstalacionMovilDto> listaMMReporteInstalacion { get; set; }
        public string strTiempoVuelo { get; set; }
        public string strCalzoCalzo { get; set; }
        public int intIdVehiculo { get; set; }
        public string strMatricula { get; set; }
    }

    public class PatrullajesTerrestresMovilDto
    {
        public DatosGeneralesMovilDto objMMDatosGenerales { get; set; }
        public List<ItinerarioRutaMovilDto> listaMMItinerarioRuta { get; set; }
        public TarjetaInformativaTerrestreMovilDto objMMTarjetaInformativa { get; set; }
        public List<ReporteEstructuraMovilDto> listaMMReporteEstructura { get; set; }
        public List<ReporteInstalacionMovilDto> listaMMReporteInstalacion { get; set; }
        public List<VehiculoMovilDto> listaMMVehiculos { get; set; }
        public int intKmRecorrido { get; set; }
    }

    public class MonitoreoMovilDto
    {
        public List<PatrullajesTerrestresMovilDto> listaMMPatrullajesTerrestresIniciados { get; set; }
        public List<PatrullajesTerrestresMovilDto> listaMMPatrullajesTerrestresTerminados { get; set; }
        public List<PatrullajesTerrestresMovilDto> listaMMPatrullajesTerrestresCancelados { get; set; }
        public List<PatrullajesAereosMovilDto> listaMMPatrullajesAereosIniciados { get; set; }
        public List<PatrullajesAereosMovilDto> listaMMPatrullajesAereosTerminados { get; set; }
        public List<PatrullajesAereosMovilDto> listaMMPatrullajesAereosCancelados { get; set; }
    }
}

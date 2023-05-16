namespace Domain.DTOs
{
    public class ReporteServicioMensualDto
    {
        public string strUsuario { get; set; }
        public int intTotalPatrullajes { get; set; }
        public string strTiempoVueloKmRecorrido { get; set; }
        public int intComandantesInstalacion { get; set; }
        public int intPersonalMilitarSEDENAoficial { get; set; }
        public int intPersonalMilitarSEDENAtropa { get; set; }
        public int intConductores { get; set; }
        public int intComandantesTurnoSSF { get; set; }
        public int intOficialesSSF { get; set; }
        public List<DetalleReporteServicioMensualDto> listaDetalles { get; set; }
    }

    public class DetalleReporteServicioMensualDto
    {
        public int intIdTarjeta { get; set; }
        public int intIdPrograma { get; set; }
        public int intIdUsuario { get; set; }
        public int intRegionSSF { get; set; }
        public string strMatriculas { get; set; }
        public string strFechaPatrullaje { get; set; }
        public string strUltimaActualizacion { get; set; }
        public string strItinerarioRuta { get; set; }
        public string strInicio { get; set; }
        public string strTermino { get; set; }
        public string strTiempoVuelo { get; set; }
        public string strCalzoCalzo { get; set; }
        public string strDescripcionEstadoPatrullaje { get; set; }
        public string strObservaciones { get; set; }
        public string strIncidenciaEnEstructura { get; set; }
        public string strIncidenciaEnInstalacion { get; set; }
        public int intIdRuta { get; set; }
        public string strKmRecorridos { get; set; }
        public string strOdometros { get; set; }
        public string strKmRecorridosVehiculos { get; set; }
        public int intComandantesInstalacionSSF { get; set; }
        public int intPersonalMilitarSEDENAoficial { get; set; }
        public int intIdEstadoTarjetaInformativa { get; set; }
        public int intIdTipoPatrullaje { get; set; }
        public int intPersonalMilitarSEDENAtropa { get; set; }
        public int intConductores { get; set; }
        public int intComandantesTurnoSSF { get; set; }
        public int intOficialesSSF { get; set; }
        public int intPersonalNavalSEMARoficial { get; set; }
        public int intPersonalNavalSEMARtropa { get; set; }
    }
}

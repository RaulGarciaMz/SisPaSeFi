using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class ReporteServicioMensualVista
    {
        public int id_nota { get; set; }
        public int id_programa { get; set; }
        public DateTime ultimaActualizacion { get; set; }
        public int id_usuario { get; set; }
        public TimeSpan inicio { get; set; }
        public TimeSpan termino { get; set; }
        public TimeSpan tiempoVuelo { get; set; }
        public TimeSpan calzoAcalzo { get; set; }
        public string observaciones { get; set; }
        public int kmRecorrido { get; set; }
        public int comandantesInstalacionSSF { get; set; }
        public int personalMilitarSEDENAOficial { get; set; }
        public int id_estadoTarjetaInformativa { get; set; }
        public int personalMilitarSEDENATropa { get; set; }
        public int linieros { get; set; }
        public int comandantesTurnoSSF { get; set; }
        public int oficialesSSF { get; set; }
        public int personalNavalSEMAROficial { get; set; }
        public int personalNavalSEMARTropa { get; set; }
        public DateTime? fechaPatrullaje { get; set; }
        public int id_ruta { get; set; }
        public string regionSSF { get; set; }
        public int id_tipoPatrullaje { get; set; }
        public string descripcionEstadoPatrullaje { get; set; }
    }

    [Keyless]
    public class DetalleReporteServicioMensualVista
    {
        public int total { get; set; }
        public int dato1 { get; set; }
        public int dato2 { get; set; }
        public int dato3 { get; set; }
        public int dato4 { get; set; }
        public int dato5 { get; set; }
        public int dato6 { get; set; }
        public int dato7 { get; set; }
    }
}

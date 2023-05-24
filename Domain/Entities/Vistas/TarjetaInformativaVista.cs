using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class TarjetaInformativaVista
    {
        //De tabla tarjetainformativa
        public int id_nota { get; set; }
        public int id_programa { get; set; }
        public DateTime ultimaactualizacion { get; set; }
        public int id_usuario { get; set; }
        public TimeSpan inicio { get; set; }
        public TimeSpan termino { get; set; }
        public TimeSpan tiempovuelo { get; set; }
        public TimeSpan calzoacalzo { get; set; }
        public string observaciones { get; set; }
        public int kmrecorrido { get; set; }
        public int comandantesinstalacionssf { get; set; }
        public int personalmilitarsedenaoficial { get; set; }
        public int id_estadotarjetainformativa { get; set; }
        public int personalmilitarsedenatropa { get; set; }
        public int linieros { get; set; }
        public int comandantesturnossf { get; set; }
        public int oficialesssf { get; set; }
        public int personalnavalsemaroficial { get; set; }
        public int personalnavalsemartropa { get; set; }
        public DateTime fechaTermino { get; set; }
        public int idresultadopatrullaje { get; set; }
        public string lineaestructurainstalacion { get; set; }
        public string responsablevuelo { get; set; }
        public int fuerzareaccion { get; set; }
        // De programaspatrullajes
        public DateTime? fechapatrullaje { get; set; }
        public int id_ruta { get; set; }
        public int id_puntoresponsable { get; set; }
        //De rutas
        public string regionssf { get; set; }
        public int id_tipopatrullaje { get; set; }
        //De estadopatrullaje
        public int id_estadopatrullaje { get; set; }
        public string descripcionestadopatrullaje { get; set; }
        //De resultadopatrullaje
        public string resultadopatrullaje { get; set; }

        public string itinerario { get; set; }
        public string incidenciaenestructura { get; set; }
        public string incidenciaeninstalacion { get; set; }
        public string matriculas { get; set; }
        public string odometros { get; set; }
        public string kmrecorridos { get; set; }
    }

    [Keyless]
    public class TarjetaInformativaIdVista
    {
        //De tabla tarjetainformativa
        public int id_nota { get; set; }
        public int id_programa { get; set; }
        public DateTime ultimaactualizacion { get; set; }
        public int id_usuario { get; set; }
        public TimeSpan inicio { get; set; }
        public TimeSpan termino { get; set; }
        public TimeSpan tiempovuelo { get; set; }
        public TimeSpan calzoacalzo { get; set; }
        public string observaciones { get; set; }
        public int kmrecorrido { get; set; }
        public int comandantesinstalacionssf { get; set; }
        public int personalmilitarsedenaoficial { get; set; }
        public int id_estadotarjetainformativa { get; set; }
        public int personalmilitarsedenatropa { get; set; }
        public int linieros { get; set; }
        public int comandantesturnossf { get; set; }
        public int oficialesssf { get; set; }
        public int personalnavalsemaroficial { get; set; }
        public int personalnavalsemartropa { get; set; }
        public DateTime fechaTermino { get; set; }
        public int idresultadopatrullaje { get; set; }
        public string lineaestructurainstalacion { get; set; }
        public string responsablevuelo { get; set; }
        public int fuerzareaccion { get; set; }
        // De programaspatrullajes
        public DateTime? fechapatrullaje { get; set; }
        public int id_ruta { get; set; }
        public int id_puntoresponsable { get; set; }
        //De rutas
        public string regionssf { get; set; }
        public int id_tipopatrullaje { get; set; }
        //De estadopatrullaje
        public int id_estadopatrullaje { get; set; }
        public string descripcionestadopatrullaje { get; set; }
        //De resultadopatrullaje
        public string resultadopatrullaje { get; set; }
        //De puntospatrullaje
        public string instalacion { get; set; }
        public string municipio { get; set; }
        public string estado { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }

        public string itinerario { get; set; }
        public string incidenciaenestructura { get; set; }
        public string incidenciaeninstalacion { get; set; }
        public string matriculas { get; set; }
        public string odometros { get; set; }
        public string kmrecorridos { get; set; }
    }
}
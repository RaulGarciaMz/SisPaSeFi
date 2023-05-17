using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class TarjetaInformativaVista
    {
        public int id_nota;
        public int id_programa;
        public DateTime? fechapatrullaje;
        public int id_ruta;
        public string regionssf;
        public int id_tipopatrullaje;
        public DateTime ultimaactualizacion;
        public int id_usuario;

        public TimeSpan inicio;
        public TimeSpan termino;
        public TimeSpan tiempovuelo;
        public TimeSpan calzoacalzo;
        public string observaciones;
        
        public int comandantesinstalacionssf;
        public int personalmilitarsedenaoficial;
        public int kmrecorridos;
        public int id_estadotarjetainformativa;
        public int personalmilitarsedenatropa;
        public int linieros;
        public int comandantesturnossf;
        public int oficialesssf;
        public int personalnavalsemaroficial;
        public int personalnavalsemartropa;
        public int id_estadopatrullaje;
        public string descripcionestadopatrullaje;
        public string matriculas;
        public string itinerario;
        public string incidenciaenestructura;  
        public string incidenciaeninstalacion; 
        public string odometros;
        public string KmVehiculos;
        public DateTime fechaTermino;
        public int idresultadopatrullaje;
        public string resultadopatrullaje;
        public string lineaestructurainstalacion;
        public string responsablevuelo;
        public int fuerzareaccion;
        public int id_puntoresponsable;
        public string instalacion;
        public string municipio;
        public string estado;
        public string nombre;
        public string apellido1;
        public string apellido2;
    }
}
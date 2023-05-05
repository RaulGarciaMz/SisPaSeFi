using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class InicioPatrullajeProgramaVista
    {
        public int id_programa { get; set; }
        public int riesgopatrullaje { get; set; }
        public string regionSSF { get; set; }
    }

    [Keyless]
    public class InicioPatrullajePuntosVista
    {
        public int id_punto { get; set; }
        public string ubicacion { get; set; }
        public string coordenadas { get; set; }
        public int id_municipio { get; set; }
        public int id_nivelriesgo { get; set; }
    }
}

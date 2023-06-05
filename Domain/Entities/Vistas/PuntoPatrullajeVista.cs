using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class PuntoPatrullajeVista
    {
        public int id_punto { get; set; }
        public string ubicacion { get; set; }
        public string coordenadas { get; set; }
        public int id_municipio { get; set; }
        public int esinstalacion { get; set; }
        public int? id_nivelriesgo { get; set; }
        public int? id_comandancia { get; set; }
        public int id_procesoresponsable { get; set; }
        public int id_gerenciadivision { get; set; }
        public int bloqueado { get; set; }
        public string municipio { get; set; }
        public int id_estado { get; set; }
        public string estado { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Vistas
{

    public class AfectacionIncidenciaVista
    {
        [Key]
        public int id_afectacionIncidencia { get; set; }
        public int id_incidencia { get; set; }
        public int id_conceptoAfectacion { get; set; }
        public int tipo_incidencia { get; set; }
        public int cantidad { get; set; }
        public float precioUnitario { get; set; }
        public string descripcion { get; set; } 
        public string unidades { get; set; } 
    }
}





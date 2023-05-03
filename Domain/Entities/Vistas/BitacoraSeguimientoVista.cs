using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class BitacoraSeguimientoVista
    {
        public int id_bitacora { get; set; }
        public int id_reporte { get; set; }
        public DateTime ultimaactualizacion { get; set; }
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string descripcion { get; set; }
        public string descripcionestado { get; set; }
        public int id_estadoincidencia { get; set; }
        public string tipoincidencia { get; set; }
    }
}
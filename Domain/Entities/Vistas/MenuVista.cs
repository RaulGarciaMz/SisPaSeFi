using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class MenuVista
    {
        public int IdMenu { get; set; }
        public string? Desplegado { get; set; }
        public string? Descripcion { get; set; }
        public string? Liga { get; set; }
        public int? Padre { get; set; }
        public int? Posicion { get; set; }
    }
}

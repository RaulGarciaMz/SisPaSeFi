using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class CatalogoVista
    {
        public int id { get; set; }
        public string nombre { get; set; }
    }
}

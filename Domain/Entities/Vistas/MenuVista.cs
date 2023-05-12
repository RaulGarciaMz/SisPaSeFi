using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class MenuVista
    {
        public int idmenu { get; set; }
        public string desplegado { get; set; }
        public string descripcion { get; set; }
        public string liga { get; set; }
        public int padre { get; set; }
        public int posicion { get; set; }
        public int navegar { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class EstadisticaSistemaVista
    {
        public string concepto { get; set; }
        public int total { get; set; }


    }
}

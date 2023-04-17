using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class UsuarioVista
    {
        public int id_usuario { get; set; }
        public string usuario_nom { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string? cel { get; set; }
        public int? configurador { get; set; }
        public string? correoelectronico { get; set; }
        public int regionSSF { get; set; }
        public int desbloquearregistros { get; set; }
        public int tiempoEspera { get; set; }
    }

    [Keyless]
    public class UsuarioRegistroVista
    {
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string? cel { get; set; }
        public int? configurador { get; set; }
        public int? bloqueado { get; set; }
        public int? AceptacionAvisoLegal { get; set; }
        public int? intentos { get; set; }
        public int? NotificarAcceso { get; set; }
        public string? correoelectronico { get; set; }
        public int regionSSF { get; set; }
        public int desbloquearregistros { get; set; }
        public int tiempoEspera { get; set; }
        public DateTime? EstampaTiempoUltimoAcceso { get; set; }
    }
}



using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class LineaEnPropuestaVista
    {
        public string clave { get; set; }
        public string inicio { get; set; }
        public string fin { get; set; }
    }

    [Keyless]
    public class VehiculoEnPropuestaVista
    {
        public string placas { get; set; }
        public string numero { get; set; }
        public string tipo { get; set; }
    }

    [Keyless]
    public class PropuestaConResponsableVista
    {
        public DateTime fechaPatrullaje { get; set; }
        public DateTime fechaTermino { get; set; }
        public string ubicacion { get; set; }
        public string municipio { get; set; }
        public string estado { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string InstalacionResponsable { get; set; }
        public string mun { get; set; }
        public string edo { get; set; }
    }
}


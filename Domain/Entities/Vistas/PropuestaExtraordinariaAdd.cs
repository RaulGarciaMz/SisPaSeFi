namespace Domain.Entities.Vistas
{
    public class PropuestaExtraordinariaAdd
    {
        public PropuestaPatrullaje Propuesta { get; set; }
        public List<PropuestaPatrullajeVehiculo> Vehiculos { get;set; }
        public List<PropuestaPatrullajeLinea> Lineas { get; set; }
    }
}

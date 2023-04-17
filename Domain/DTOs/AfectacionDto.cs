namespace Domain.DTOs
{
    public class AfectacionDtoForCreate
    {
        public int IdIncidencia { get; set; }
        public int IdConceptoAfectacion { get; set; }
        public int Cantidad { get; set; }
        public float PrecioUnitario { get; set; }
        public int IdTipoIncidencia { get; set; }
        public string TipoIncidencia { get; set; }
        public string Usuario { get; set; }
    }

    public class AfectacionDtoForUpdate
    {
        public int IdAfectacionIncidencia { get; set; }
        public int Cantidad { get; set; }
        public float PrecioUnitario { get; set; }
        public string Usuario { get; set; }
    }
}
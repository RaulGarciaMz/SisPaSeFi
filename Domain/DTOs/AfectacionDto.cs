namespace Domain.DTOs
{
    public class AfectacionDtoForCreate
    {
        public int intIdIncidencia { get; set; }
        public int intIdConceptoAfectacion { get; set; }
        public int intCantidad { get; set; }
        public float sngPrecioUnitario { get; set; }
        public int intIdTipoIncidencia { get; set; }
        public string strTipoIncidencia { get; set; }
        public string strUsuario { get; set; }
    }

    public class AfectacionDtoForUpdate
    {
        public int intIdAfectacionIncidencia { get; set; }
        public int intCantidad { get; set; }
        public float sngPrecioUnitario { get; set; }
        public string strUsuario { get; set; }
    }
}
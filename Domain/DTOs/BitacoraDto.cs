namespace Domain.DTOs
{
    public class BitacoraDtoForCreate
    {
        public int intIdReporte { get; set; }
        public string strUsuario { get; set; }
        public int intIdEstadoIncidencia { get; set; }
        public string strDescripcion { get; set; }
        public string strTipoIncidencia { get; set; }
    }

    public class BitacoraDto
    {
        public int intIdBitacoraSeguimientoIncidencia { get; set; }
        public int intIdReporte { get; set; }
        public string strUltimaActualizacion { get; set; }
        public int intIdUsuario { get; set; }
        public string strNombreCompletoUsuario { get; set; }
         public string strDescripcion { get; set; }
        public string strDescripcionEstado { get; set; }
        public int intIdEstadoIncidencia { get; set; }
        public string strTipoIncidencia { get; set; }
    }
}

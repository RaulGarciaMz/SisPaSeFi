namespace Domain.DTOs
{
    public  class RolDto
    {
        public int intIdRol { get; set; }
        public string strNombre { get; set; }
        public string strDescripcion { get; set; }
        public int intIdMenu { get; set; }
    }

    public class RolDtoForCreate
    {
        public string strNombre { get; set; }
        public string strDescripcion { get; set; }
        public int intIdMenu { get; set; }
    }
}

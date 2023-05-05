namespace Domain.DTOs
{
    public class MenuDto
    {
        public int intIdMenu { get; set; }
        public string? strDesplegado { get; set; }
        public string? strDescripcion { get; set; }
        public string? strLiga { get; set; }
        public int? intIdPadre { get; set; }
        public int? intPosicion { get; set; }
    }
}

namespace Domain.DTOs
{
    public class ItinerarioDtoForCreate
    {
        public int intIdRuta { get; set; }
        public int intIdPunto { get; set; }
        public int intPosicion { get; set; }
        public string strUsuario { get; set; }
    }

    public class ItinerarioDtoForUpdate
    {
        public int intIdRuta { get; set; }
        public int intIdPunto { get; set; }
        public int intPosicion { get; set; }
        public string strUsuario { get; set; }
        public int intIdItinerario { get; set; }
    }
}

namespace Domain.DTOs
{
    public class InicioPatrullajeDto
    {
        public string usuario { get; set; }
        public int IdRuta { get; set; }

        public string FechaPatrullaje { get; set; }
        public string HoraInicio { get; set; }

        public int ComandanteInstalacion { get; set; }
        public int ComandanteTurno { get; set; }
        public int OficialSSF { get; set; }
        public int OficialSDN { get; set; }
        public int TropaSDN { get; set; }
        public int Conductores { get; set; }

        public List<InicioPatrullajeVehiculoDto> objInicioPatrullajeVehiculo { get; set; }
    }

    public class InicioPatrullajeVehiculoDto
    {
        public int IdVehiculo { get; set; }
        public int KmInicio { get; set; }
        public string EstadoVehiculo { get; set; }
    }
}


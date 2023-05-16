namespace Domain.Ports.Driving
{
    public interface IReporteServicioMensualDtoCommand
    {
        Task ObtenerReporteDeServicioMensualPorOpcionAsync(int anio, int mes, string tipo, string usuario);
    }
}

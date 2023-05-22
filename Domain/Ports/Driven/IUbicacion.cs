namespace Domain.Ports.Driven
{
    public interface IUbicacionQuey
    {
        Task<int?> ObtenerIdProgramaPorRutaAndFechaAsync(int idRuta, DateTime fecha);
    }

    public interface IUbicacionPost
    {
        Task ActualizarUbicacionAsync(int idPrograma, int idUsuario, string latitud, string longitud);
    }
}

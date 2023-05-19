namespace Domain.Ports.Driving
{
    public interface IOficioAutorizacionDtoQuery
    {
        Task<List<string>> ObtenerInformacionParaOficioAutorizacion(string usuario, string password, int idPropuesta);
    }
}

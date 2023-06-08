using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IRegistroIncidenciaDtoCommand
    {
        Task AgregaIncidenciaTransaccionalAsync(RegistrarIncidenciaDto i, string imagenes, string repo);
    }
}

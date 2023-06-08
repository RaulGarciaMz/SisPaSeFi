using Domain.DTOs;

namespace Domain.Ports.Driven
{
    public interface IRegistroIncidenteCommand
    {
        Task AgregaIncidenciaTransaccionalAsync(RegistrarIncidenciaDto ri, int idUsuario, string imagenes, string repo);
    }


}

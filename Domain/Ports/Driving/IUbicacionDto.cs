using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IUbicacionDtoCommand
    {
        Task ActualizaUbicacionAsync(UbicacionForUpdateDto ubicacion);
    }
}

using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IInicioPatrullajeDtoCommand
    {
        Task AgregaInicioPatrullajeAsync(InicioPatrullajeDto a);
    }

}

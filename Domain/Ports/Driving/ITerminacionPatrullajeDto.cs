using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface ITerminacionPatrullajeDtoCommand
    {
        Task RegistraTerminacionAsync(TerminacionPatrullajeDto p);
    }
}

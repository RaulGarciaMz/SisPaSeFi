using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IPersonalParticipanteDtoCommand
    {
        Task AgregarAsync(PersonalParticipanteDtoForCreate usuario);
        Task BorrarAsync(PersonalParticipanteDtoForCreate usuario);
    }

    public interface IPersonalParticipanteDtoQuery
    {
        Task<List<PersonalParticipanteDto>> ObtenerPersonalParticipantePorOpcionAsync(string opcion, int idPrograma, int region, string usuario);
    }
}

using Domain.DTOs;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IPersonalParticipanteDtoCommand
    {
        Task Agregar(PersonalParticipanteDto usuario);
        Task Borrar(PersonalParticipanteDto usuario);
    }

    public interface IPersonalParticipanteDtoQuery
    {
        Task<List<PersonalParticipanteVista>> ObtenerPersonalParticipantePorOpcionAsync(string opcion, int idPrograma, int region, string usuario);
    }
}

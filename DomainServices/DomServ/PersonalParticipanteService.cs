using Domain.DTOs;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class PersonalParticipanteService : IPersonalParticipanteService
    {
        private readonly IPersonalParticipanteRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public PersonalParticipanteService(IPersonalParticipanteRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task<List<PersonalParticipanteVista>> ObtenerPersonalParticipantePorOpcionAsync(string opcion, int idPrograma, int region, string usuario)
        {
            var l = new List<PersonalParticipanteVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                switch (opcion)
                {
                    case "PersonalAsignado":
                        l = await _repo.ObtenerPersonalAsignadoEnProgramaAsync(idPrograma);

                        break;
                    case "PersonalNoAsignado":
                        l = await _repo.ObtenerPersonalNoAsignadoEnProgramaAsync(idPrograma, region);
                        break;
                }
            }

            return l;
        }


        public async Task AgregarAsync(PersonalParticipanteDto u)
        {
            var l = new List<PersonalParticipanteVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(u.strNombreDeUsuario);

            if (user != null)
            {

                var usAsignado = await _repo.ObtenerUsuarioPatrullajeAsignadoEnProgramaAsync(u.IdPrograma, u.intIdUsuario);

                if (usAsignado != null)
                {
                    if (usAsignado.Count() > 0)
                    {
                        await _repo.AgregaUsuarioPatrullajeAsync(u.IdPrograma, u.intIdUsuario);
                    }
                }
            }
        }

        public async Task BorrarAsync(PersonalParticipanteDto u)
        {
            var l = new List<PersonalParticipanteVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(u.strNombreDeUsuario);

            if (user != null)
            {
                await _repo.BorraUsuarioPatrullajeAsync(u.IdPrograma, u.intIdUsuario);
            }
        }
    }
}

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

        public async Task<List<PersonalParticipanteDto>> ObtenerPersonalParticipantePorOpcionAsync(string opcion, int idPrograma, int region, string usuario)
        { 
            var lp = new List<PersonalParticipanteDto>();
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

                lp = ConvierteListaPersonalToDto(l);
            }

            return lp;
        }


        private List<PersonalParticipanteDto> ConvierteListaPersonalToDto(List<PersonalParticipanteVista> l)
        {

            var lp = new List<PersonalParticipanteDto>();
            foreach ( var item in l ) 
            {
                var np = new PersonalParticipanteDto() 
                {
                    intIdUsuario = item.id_usuario,
                    strNombreDeUsuario = item.usuario_nom,
                    strNombre = item.nombre,
                    strApellido1 = item.apellido1,
                    strApellido2 = item.apellido2,
                    strCorreoElectronico = item.correoelectronico,
                    strCel = item.cel,
                    intConfigurador = item.configurador.Value
                
                };
            }

            return lp;
        }

        public async Task AgregarAsync(PersonalParticipanteDtoForCreate u)
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

        public async Task BorrarAsync(PersonalParticipanteDtoForCreate u)
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

using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Microsoft.Extensions.Options;
using Domain.Entities.Vistas;
using Domain.Entities;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class UsuariosPatrullajeService : IUsuariosPatrullajeService
    {
        private readonly IUsuarioPatrullajeRepo _repo;
        private readonly IUsuariosConfiguradorQuery _user;

        public UsuariosPatrullajeService(IUsuarioPatrullajeRepo repo, IUsuariosConfiguradorQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task<List<UsuarioPatrullajeVista>> ObtenerIncidenciasPorOpcionAsync(string opcion, int idPrograma, int region, string usuario)
        {
            var l = new List<UsuarioPatrullajeVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                switch (opcion)
                {
                    case "PersonalAsignado":
                        l= await _repo.ObtenerPersonalAsignadoEnProgramaAsync(idPrograma);
                        break;
                    case "PersonalNoAsignado":
                        l= await _repo.ObtenerPersonalNoAsignadoEnProgramaAsync(idPrograma, region);
                        break;
                }
            }

            return l;
        }


        public async Task Agregar(UsuarioPatrullajeDto u)
        {
            var l = new List<UsuarioPatrullajeVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(u.Usuario);

            if (user != null)
            {

                var usAsignado = await _repo.ObtenerUsuarioPatrullajeAsignadoEnProgramaAsync(u.IdPrograma, u.IdUsuario);

                if (usAsignado != null)
                {
                    if (usAsignado.Count() > 0)
                    {
                        await _repo.AgregaUsuarioPatrullajeAsync(u.IdPrograma, u.IdUsuario);
                    }
                }
            }
        }

        public async Task Borrar(UsuarioPatrullajeDto u)
        {
            var l = new List<UsuarioPatrullajeVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(u.Usuario);

            if (user != null)
            {
                await _repo.BorraUsuarioPatrullajeAsync(u.IdPrograma, u.IdUsuario);
            }
        }
    }
}

﻿using Domain.Ports.Driven.Repositories;
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
    public class PersonalParticipanteService : IPersonalParticipanteService
    {
        private readonly IPersonalParticipanteRepo _repo;
        private readonly IUsuariosConfiguradorQuery _user;

        public PersonalParticipanteService(IPersonalParticipanteRepo repo, IUsuariosConfiguradorQuery u)
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


        public async Task Agregar(PersonalParticipanteDto u)
        {
            var l = new List<PersonalParticipanteVista>();
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

        public async Task Borrar(PersonalParticipanteDto u)
        {
            var l = new List<PersonalParticipanteVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(u.Usuario);

            if (user != null)
            {
                await _repo.BorraUsuarioPatrullajeAsync(u.IdPrograma, u.IdUsuario);
            }
        }
    }
}
﻿using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.DomServ
{
    public class AfectacionesService : IAfectacionesService
    {
        private readonly IAfectacionesRepo _repo;
        private readonly IUsuariosConfiguradorQuery _user;

        public AfectacionesService(IAfectacionesRepo repo, IUsuariosConfiguradorQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task AgregaAsync(AfectacionDtoForCreate a) 
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(a.Usuario);

            if (user != null)
            {
                var existe = await ExisteAfectacionRegistrada(a.IdIncidencia, a.IdConceptoAfectacion, a.TipoIncidencia);

                if (!existe)
                {
                    await _repo.AgregaAsync(a.IdIncidencia, a.IdConceptoAfectacion, a.Cantidad, a.PrecioUnitario, a.IdTipoIncidencia);
                }                
            }
        }

        public async Task ActualizaAsync(AfectacionDtoForUpdate a)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(a.Usuario);

            if (user != null)
            {
                await _repo.ActualizaAsync(a.IdAfectacionIncidencia, a.Cantidad, a.PrecioUnitario);
            }
        }

        public async Task<List<AfectacionIncidenciaVista>> ObtenerAfectacionIncidenciaPorOpcionAsync(int idReporte, string tipo, string usuario)
        {
            var afect = new List<AfectacionIncidenciaVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                afect = await _repo.ObtenerAfectacionIncidenciaPorOpcionAsync(idReporte, tipo);
            }

            return afect;
        }

        private async Task<bool> ExisteAfectacionRegistrada(int idIncidencia, int idConcepto, string tipo)
        {
            var result = false;
            var afect = await _repo.ObtenerAfectacionPorIncidenciaAndTipoAndConceptoAsync(idIncidencia, idConcepto, tipo);

            if (afect != null) 
            {
                if (afect.Count > 0)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
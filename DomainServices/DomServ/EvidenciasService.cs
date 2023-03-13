using Domain.DTOs;
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
    public class EvidenciasService : IEvidenciasService
    {
        private readonly IEvidenciasRepo _repo;
        private readonly IUsuariosConfiguradorQuery _user;

        public EvidenciasService(IEvidenciasRepo repo, IUsuariosConfiguradorQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task AgregarEvidenciaPorTipo(EvidenciaDto evidencia)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(evidencia.Usuario);

            if (user != null) 
            {
                switch(evidencia.TipoIncidencia) 
                {
                    case "INSTALACION":
                        await _repo.AgregarEvidenciaDeInstalacionAsync(evidencia.IdReporte, evidencia.RutaArchivo, evidencia.NombreArchivo, evidencia.Descripcion);
                        break;
                    case "ESTRUCTURA":
                        await _repo.AgregarEvidenciaDeInstalacionAsync(evidencia.IdReporte, evidencia.RutaArchivo, evidencia.NombreArchivo, evidencia.Descripcion);
                        break;
                }
            }            
        }

        public async Task BorrarEvidenciaPorTipo(int idEvidencia, string tipo, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                switch (tipo)
                {
                    case "INSTALACION":
                        await _repo.BorrarEvidenciaDeInstalacionAsync(idEvidencia);
                        break;
                    case "ESTRUCTURA":
                        await _repo.BorrarEvidenciaDeEstructuraAsync(idEvidencia);
                        break;
                }
            }
        }

        public async Task<List<EvidenciaVista>> ObtenerEvidenciasPorTipo( int idReporte, string tipo, string usuario)
        {
            var l = new List<EvidenciaVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                switch (tipo)
                {
                    case "INSTALACION":
                        l = await _repo.ObtenerEvidenciaDeInstalacionAsync(idReporte);
                        break;
                    case "ESTRUCTURA":
                        l =await _repo.ObtenerEvidenciaDeEstructuraAsync(idReporte);
                        break;
                }
            }

            return l;
        }
    }
}

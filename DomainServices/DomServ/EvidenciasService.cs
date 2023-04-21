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
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(evidencia.strUsuario);

            if (user != null) 
            {
                switch(evidencia.strTipoIncidencia) 
                {
                    case "INSTALACION":
                        await _repo.AgregarEvidenciaDeInstalacionAsync(evidencia.intIdReporte, evidencia.strRutaArchivo, evidencia.strNombreArchivo, evidencia.strDescripcion);
                        break;
                    case "ESTRUCTURA":
                        await _repo.AgregarEvidenciaDeInstalacionAsync(evidencia.intIdReporte, evidencia.strRutaArchivo, evidencia.strNombreArchivo, evidencia.strDescripcion);
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
                    case "EvidenciaIncidenciaEnINSTALACION":
                        l = await _repo.ObtenerEvidenciaDeInstalacionAsync(idReporte);
                        break;
                    case "EvidenciaIncidenciaEnESTRUCTURA":
                        l =await _repo.ObtenerEvidenciaDeEstructuraAsync(idReporte);
                        break;
                }
            }

            return l;
        }
    }
}

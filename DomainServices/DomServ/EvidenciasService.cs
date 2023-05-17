using Domain.DTOs;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class EvidenciasService : IEvidenciasService
    {
        private readonly IEvidenciasRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public EvidenciasService(IEvidenciasRepo repo, IUsuariosParaValidacionQuery u)
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
                        await _repo.AgregarEvidenciaDeEstructuraAsync(evidencia.intIdReporte, evidencia.strRutaArchivo, evidencia.strNombreArchivo, evidencia.strDescripcion);
                        break;
                    case "SeguimientoINSTALACION":
                        await _repo.AgregarEvidenciaSeguimientoDeInstalacionAsync(evidencia.intIdReporte, evidencia.strRutaArchivo, evidencia.strNombreArchivo, evidencia.strDescripcion);
                        break;
                    case "SeguimientoESTRUCTURA":
                        await _repo.AgregarEvidenciaSeguimientoDeEstructuraAsync(evidencia.intIdReporte, evidencia.strRutaArchivo, evidencia.strNombreArchivo, evidencia.strDescripcion);
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
                    case "SeguimientoINSTALACION":
                        await _repo.BorrarEvidenciaSeguimientoDeInstalacionAsync(idEvidencia);
                        break;
                    case "SeguimientoESTRUCTURA":
                        await _repo.BorrarEvidenciaSeguimientoDeEstructuraAsync(idEvidencia);
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
                    case "EvidenciaSeguimientoIncidenciaEnINSTALACION":
                        l = await _repo.ObtenerEvidenciaSeguimientoDeInstalacionAsync(idReporte);
                        break;
                    case "EvidenciaSeguimientoIncidenciaEnESTRUCTURA":
                        l = await _repo.ObtenerEvidenciaSeguimientoDeEstructuraAsync(idReporte);
                        break;
                }
            }

            return l;
        }
    }
}

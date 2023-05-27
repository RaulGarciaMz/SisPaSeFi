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

        public async Task AgregarEvidenciaPorTipoAsync(EvidenciaDtoForCreate evidencia)
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

        public async Task BorrarEvidenciaPorTipoAsync(int idEvidencia, string tipo, string usuario)
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
        public async Task<List<EvidenciaDto>> ObtenerEvidenciasPorTipoAsync( int idReporte, string tipo, string usuario)
        {
            var ret = new List<EvidenciaDto>();
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

                ret = ConvierteListaEvidenciaToDto(l);
            }

            return ret;
        }

        private List<EvidenciaDto> ConvierteListaEvidenciaToDto(List<EvidenciaVista> evidencias)
        {
            var ret = new List<EvidenciaDto>();
            foreach (var v in evidencias)
            {
                var ev = new EvidenciaDto()
                {
                    intIdEvidenciaIncidencia = v.IdEvidencia,
                    intIdReporte = v.IdReporte,
                    strRutaArchivo = v.RutaArchivo,
                    strNombreArchivo = v.NombreArchivo,
                    strDescripcion = v.Descripcion,
                    strTipoIncidencia = v.TipoReporte

                };

                ret.Add(ev);
            }
            return ret;
        }
    }
}

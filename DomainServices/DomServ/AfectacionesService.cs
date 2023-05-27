using Domain.DTOs;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class AfectacionesService : IAfectacionesService
    {
        private readonly IAfectacionesRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public AfectacionesService(IAfectacionesRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task AgregaAsync(AfectacionDtoForCreate a) 
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(a.strUsuario);

            if (user != null)
            {
                var existe = await ExisteAfectacionRegistrada(a.intIdIncidencia, a.intIdConceptoAfectacion, a.strTipoIncidencia);

                if (!existe)
                {
                    await _repo.AgregaAsync(a.intIdIncidencia, a.intIdConceptoAfectacion, a.intCantidad, a.sngPrecioUnitario, a.intIdTipoIncidencia);
                }                
            }
        }

        public async Task ActualizaAsync(AfectacionDtoForUpdate a)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(a.strUsuario);

            if (user != null)
            {
                await _repo.ActualizaAsync(a.intIdAfectacionIncidencia, a.intCantidad, a.sngPrecioUnitario);
            }
        }

        public async Task<List<AfectacionDto>> ObtenerAfectacionIncidenciaPorOpcionAsync(int idReporte, string tipo, string usuario)
        {
            var retAfec = new List<AfectacionDto>();
            var afect = new List<AfectacionIncidenciaVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                afect = await _repo.ObtenerAfectacionIncidenciaPorOpcionAsync(idReporte, tipo);

                retAfec = ConvierteListaAfectacionVistaToDto(afect, tipo);
            }

            return retAfec;
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

        private List<AfectacionDto> ConvierteListaAfectacionVistaToDto(List<AfectacionIncidenciaVista> a, string opcion)
        {
            var l = new List<AfectacionDto>();

            foreach (var af in a) 
            {
                var nAf = new AfectacionDto()
                { 
                    intIdAfectacionIncidencia = af.id_afectacionIncidencia,
                    intIdIncidencia = af.id_incidencia,
                    intIdConceptoAfectacion = af.id_conceptoAfectacion,
                    intCantidad = af.cantidad,
                    sngPrecioUnitario = af.precioUnitario,
                    intIdTipoIncidencia = af.tipo_incidencia,
                    strDescripcion = af.descripcion,
                    strUnidades = af.unidades,
                    strTipoIncidencia = opcion
                };
            
                l.Add(nAf);
            }

            return l;
        }
    }
}

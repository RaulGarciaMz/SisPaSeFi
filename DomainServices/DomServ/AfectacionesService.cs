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

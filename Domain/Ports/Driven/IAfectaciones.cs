using Domain.Entities;
using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IAfectacionesComand
    {
        Task AgregaAsync(int idIncidencia, int idConcepto, int cantidad, float precio, string tipo);
        Task ActualizaAsync(int idIncidencia, int cantidad, float precio);
    }

    public interface IAfectacionesQuery
    {
        Task<List<AfectacionIncidenciaVista>> ObtenerAfectacionIncidenciaPorOpcionAsync(int idReporte, string tipo);
        Task<List<AfectacionIncidencia>> ObtenerAfectacionPorIncidenciaAndTipoAndConceptoAsync(int idIncidencia, int idConcepto, string tipo);
    }
}

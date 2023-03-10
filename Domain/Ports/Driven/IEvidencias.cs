using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven
{
    public interface IEvidenciasCommand
    {
        Task AgregarEvidenciaDeEstructuraAsync(int idReporte, string rutaArchivo, string nombreArchivo, string descripcion);
        Task AgregarEvidenciaDeInstalacionAsync(int idReporte, string rutaArchivo, string nombreArchivo, string descripcion);
        Task BorrarEvidenciaDeEstructuraAsync(int idEvidencia);
        Task BorrarEvidenciaDeInstalacionAsync(int idEvidencia);
    }

    public interface IEvidenciasQuery
    {
        Task <List<EvidenciaVista>> ObtenerEvidenciaDeInstalacionAsync(int idReporte);
        Task <List<EvidenciaVista>> ObtenerEvidenciaDeEstructuraAsync(int idReporte);
    }
}

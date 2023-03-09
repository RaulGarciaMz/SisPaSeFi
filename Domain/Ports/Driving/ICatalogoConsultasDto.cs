using Domain.DTOs.catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface ICatalogoConsultasDto
    {
        Task<List<CatalogoGenerico>> ObtenerComandanciaPorIdUsuarioAsync(int idUsuario);
        Task<List<CatalogoGenerico>> ObtenerTiposPatrullajeAsync();
        Task<List<CatalogoGenerico>> ObtenerTiposVehiculoAsync();
        Task<List<CatalogoGenerico>> ObtenerClasificacionesIncidenciaAsync();
        Task<List<CatalogoGenerico>> ObtenerNivelesAsync();
        Task<List<CatalogoGenerico>> ObtenerConceptosAfectacionAsync();
        Task<List<int>> ObtenerRegionesMilitaresEnRutasAsync();
    }
}

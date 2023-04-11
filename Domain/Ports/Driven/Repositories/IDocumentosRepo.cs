using Domain.Entities;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven.Repositories
{
    public interface IDocumentosRepo
    {
        Task<List<DocumentosVista>> ObtenerDocumentosPatrullajeAsync(int idComandancia, int anio, int mes);
        Task<List<DocumentosVista>> ObtenerDocumentosDeUnUsuarioTodosAsync(int idUsuario);
        Task<List<DocumentosVista>> ObtenerDocumentosDeUnUsuarioMesAsync(int idUsuario, int anio, int mes);
        Task<List<DocumentosVista>> ObtenerDocumentosParaUnUsuarioTodosAsync(int idUsuario);
        Task<List<DocumentosVista>> ObtenerDocumentosParaUnUsuarioMesAsync(int idUsuario, int anio, int mes);
    }
}

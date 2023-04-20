using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{

    public interface IDocumentoDtoCommand
    {
        Task AgregarAsync(DocumentoDtoForCreate d);
    }

    public interface IDocumentoDtoQuery
    {
        Task<List<DocumentoDto>> ObtenerDocumentosAsync(string opcion, string criterio, int anio, int mes, string usuario);
    }
}

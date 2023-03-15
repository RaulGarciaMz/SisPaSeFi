using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IDocumentoDtoQuery
    {
        Task<List<DocumentoDto>> ObtenerDocumentosAsync(int idComandancia, int anio, int mes, string usuario);
    }
}

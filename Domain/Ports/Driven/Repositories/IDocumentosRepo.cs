using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven.Repositories
{
    public interface IDocumentosRepo
    {
        Task<List<DocumentoPatrullaje>> ObtenerDocumentosAsync(int idComandancia, int anio, int mes);
    }
}

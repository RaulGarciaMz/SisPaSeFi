using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IEvidenciasDtoCommand
    {       
        Task AgregarEvidenciaPorTipoAsync(EvidenciaDtoForCreate evidencia);
        Task BorrarEvidenciaPorTipoAsync(int idEvidencia, string tipo, string usuario);
    }

    public interface IEvidenciasDtoQuery
    {
        Task<List<EvidenciaDto>> ObtenerEvidenciasPorTipoAsync(int idReporte, string tipo, string usuario);
    }
}

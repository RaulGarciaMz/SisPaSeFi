using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven
{
    public interface IProgramaMensualQuery
    {
        Task<List<ResponsableRegionesVista>> ObtenerRegionesMilitaresProgramaMensualAsync(int anio, int mes, string region, string tipo);
        Task<List<ProgramaItinerarioVista>> ObtenerProgramaMensualAsync(int anio, int mes, string region, string tipo);
    }

}

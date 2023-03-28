using Domain.DTOs;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface ILineasDtoCommand
    {
        Task AgregarAsync(LineaDtoForCreate linea);
        Task ActualizaAsync(LineaDtoForUpdate linea);
        Task BorraAsync(int idLinea, string usuario);
    }

    public interface ILineasDtoQuery
    {
        Task<List<LineaVista>> ObtenerLineasAsync(int opcion, string criterio, string usuario);
    }
}

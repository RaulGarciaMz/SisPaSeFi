using Domain.DTOs;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IEstructurasDtoCommand
    {
        Task ActualizaUbicacionAsync(int idEstructura, string nombre, int idMunicipio, string latitud, string longitud, string usuario);
        Task AgregaAsync(int idLinea, string nombre, int idMunicipio, string? latitud, string? longitud, string usuario);
    }

    public interface IEstructurasDtoQuery
    {
        Task<List<EstructuraDto>> ObtenerEstructuraPorOpcionAsync(int opcion, int idLinea, string criterio, string usuario);
        Task<EstructuraDto> ObtenerEstructuraPorIdAsync(int idEstructura, string usuario);
    }
}

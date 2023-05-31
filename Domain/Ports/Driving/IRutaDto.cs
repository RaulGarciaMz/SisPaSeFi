﻿using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IRutaDtoCommand
    {
        Task AgregaAsync(RutaDtoForCreate pp);
        Task UpdateAsync(RutaDtoForUpdate pp);
        Task ReiniciaRegionMilitarAsync(string opcion, string regionMilitar, string tipoPatrullaje, string usuario);
        Task DeleteAsync(int id, string usuario);
    }

    public interface IRutaDtoQuery
    {
        Task<List<RutaDto>> ObtenerPorFiltroAsync(string usuario, int opcion, string tipo, string criterio, string actividad);
        Task<List<RutaDisponibleDto>> ObtenerRutasDisponiblesParaCambioDeRutaAsync(string region, DateTime fecha);
    }
}

﻿using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface ITarjetaDtoCommand
    {
        Task AgregaTarjetaTransaccionalAsync(TarjetaDtoForCreate tarjeta);
        Task UpdateAsync(TarjetaDtoForUpdate tarjeta);
    }

    public interface ITarjetaDtoQuery
    {
        Task<List<TarjetaDto>> ObtenerPorOpcionAsync(int opcion, string tipo, string region, int anio, int mes, int dia, string usuario);
        Task<TarjetaDto?> ObtenerPorIdAndOpcionAsync(int idTarjeta, string usuario, string opcion);
    }
}

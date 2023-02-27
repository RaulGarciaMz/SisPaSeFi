using Domain.Entities;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface ITarjetaInformativaCommand
    {
        Task AgregaAsync(TarjetaInformativa tarjeta, int idEstadoPatrullaje, int usuarioId);
        Task UpdateAsync(TarjetaInformativa tarjeta, int idEstadoPatrullaje, int usuarioId);
    }

    public interface ITarjetaInformativaQuery
    {
        Task<List<TarjetaInformativaVista>> ObtenerPorAnioMesAsync(string tipo, string region, int anio, int mes);

        Task<int> ObtenerIdUsuarioRegistradoAsync(string usuario);
        Task<int> ObtenerIdUsuarioConfiguradorAsync(string usuario);
        Task<TarjetaInformativa?> ObtenerTarjetaPorIdNotaAsync(int idNota);
        Task<int> NumeroDeTarjetasPorProgamaAsync(int idPrograma);
    }
}

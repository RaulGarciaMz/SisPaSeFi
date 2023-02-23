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
        void Agrega(TarjetaInformativa tarjeta, int idEstadoPatrullaje, int usuarioId);
        void Update(TarjetaInformativa tarjeta, int idEstadoPatrullaje, int usuarioId);
    }

    public interface ITarjetaInformativaQuery
    {
        List<TarjetaInformativaVista> ObtenerPorAnioMes(string tipo, string region, int anio, int mes);

        int ObtenerIdUsuarioRegistrado(string usuario);
        int ObtenerIdUsuarioConfigurador(string usuario);
        TarjetaInformativa? ObtenerTarjetaPorIdNota(int idNota);
        int NumeroDeTarjetasPorProgama(int idPrograma);
    }
}
